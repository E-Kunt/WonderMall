using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UsersList : cAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] == "jishi")
        {
            getDataRows();
        }
        else if (Request.QueryString["action"] == "bindUser")
        {
            bindUser();
        }
        else if (Request.QueryString["action"] == "addUser")
        {
            addUser();
        }
        else if (Request.QueryString["action"] == "edit")
        {
            edit();
        }
        else if (Request.QueryString["action"] == "editUser")
        {
            editUser();
        }
        else if (Request.QueryString["action"] == "deleteUser")
        {
            deleteUser();
        }
    }

    protected void bindUser()
    {
        int page = Convert.ToInt32(Request.Form["page"]);//显示的是第几页
        int rows = Convert.ToInt32(Request.Form["rows"]);//显示的是该页面的大小
        string order = Request.Form["order"];//排列规则
        string sort = Request.Form["sort"];//排列字段
        int count = rows * (page - 1);
        int total = 0;
        string sql = "";
        #region 不是查询的时候绑定的数据
        //title 是传递过来的参数 即查询的内容
        if (Request.Form["title"] == null || Request.Form["title"] == "")
        {
            total = OleDbHelper.getCountRows("select Count(*) from Db_User");

            if (page == 1)
            {
                sql = "select top " + rows + " ID,userName,userSex,userCreateDate,userTouch,userEmail,userAddress from Db_User  order by " + sort + " " +
                      order;
            }

            else
            {
                sql = "select top " + rows +
                      " ID,userName,userSex,userCreateDate,userTouch,userEmail,userAddress from Db_User where ID not in (select top " + count +
                      " ID from Db_User order by " + sort + " " + order + ") order by " + sort + " " + order;
            }
        }
        #endregion
        #region 是查询的时候绑定数据

        else
        {
            string title = Request.Form["title"];
            total = OleDbHelper.getCountRows("select Count(*) from Db_User where userName like '%" + title + "%'");
            if (page == 1)
            {
                sql = "select top " + rows + " ID,userName,userSex,userCreateDate,userTouch,userEmail,userAddress from Db_User where  userName like '%" + title + "%' order by " + sort + " " +
                      order;
            }

            else
            {
                sql = "select top " + rows +
                      " ID,userName,userSex,userCreateDate,userTouch,userEmail,userAddress from Db_User where ID not in (select top " + count +
                      " ID from Db_User where userName like '%" + title + "%' order by " + sort + " " + order + ") and userName like '%" + title + "%' order by " + sort + " " + order;
            }
        }
        #endregion
        DataTable dt = OleDbHelper.GetDataTable(sql);
        string data = CommonHelper.DataTable2Json(dt);
        Response.Write("{\"total\":" + total + "," + data + "}");
        Response.End();
    }
    protected void addUser()
    {
        string username = Request.Form["userName"];
        string userpassword = CommonHelper.GetMD5(Request.Form["passWord"] + CommonHelper.GetPawSalt());
        string userEmail = Request.Form["userEmail"];
        DateTime createDate = DateTime.Now;
        string userTouch = Request.Form["userTouch"];
        string userAddress = Request.Form["userAddress"];
        string userSex = Request.Form["userSex"];
        string checksql = "select count(*) from Db_User where userName=@userName";
        int count = OleDbHelper.getCountRows(checksql, new OleDbParameter("@userName", username));
        if (count >= 1)
        {
            Response.Write("error");
            Response.End();
        }
        else
        {
            string sql = "insert into Db_User(userName,userpassWord,usersex,userCreateDate,userTouch,userEmail,userAddress)values(@userName,@userpassWord,@usersex,@userCreateDate,@userTouch,@userEmail,@userAddress)";
            int i = OleDbHelper.ExecuteSql(sql, new OleDbParameter("@userName", username),
                new OleDbParameter("@userpassWord", userpassword),
                new OleDbParameter("@usersex", userSex),
                new OleDbParameter("@createDate", createDate.ToString()),
                new OleDbParameter("@userTouch", userTouch),
                new OleDbParameter("@userEmail", userEmail),
                new OleDbParameter("@userAddress", userAddress));
            if (i == 1)
            {
                Response.Write("success");
                Response.End();
            }
            else
            {
                Response.Write("error");
                Response.End();
            }
        }
    }
    protected void edit()
    {
        int id = Convert.ToInt32(Request.Form["ID"]);
        string sql = "select * from Db_User where ID=" + id;
        DataTable dt = OleDbHelper.GetDataTable(sql);
        string[] data = new string[6];
        data[0] = dt.Rows[0]["userName"].ToString();
        data[1] = dt.Rows[0]["userpassWord"].ToString();
        data[2] = dt.Rows[0]["userSex"].ToString();
        data[3] = dt.Rows[0]["userTouch"].ToString();
        data[4] = dt.Rows[0]["userEmail"].ToString();
        data[5] = dt.Rows[0]["userAddress"].ToString();
        Response.Write(data[0] + "," + data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + data[5]);
        Response.End();
    }

    protected void editUser()
    {
        int id = Convert.ToInt32(Request.Form["ID"]);
        string userpassWord = Request.Form["passWord"];
        string userEmail = Request.Form["userEmail"];
        string userTouch = Request.Form["userTouch"];
        string userAddress = Request.Form["userAddress"];
        string sql = "update Db_User set userpassWord=@userpassWord,userTouch=@userTouch,userEmail=@userEmail,userAddress=@userAddress where ID=@id";
        int i = OleDbHelper.ExecuteSql(sql, 
            new OleDbParameter("@userpassWord", userpassWord),
            new OleDbParameter("@userTouch", userTouch),
            new OleDbParameter("@userEmail", userEmail),
            new OleDbParameter("@userAddress", userAddress),
            new OleDbParameter("@id", id));
        if (i == 1)
        {
            Response.Write("success");
            Response.End();
        }
        else
        {
            Response.Write("error");
            Response.End();
        }
    }

    protected void deleteUser()
    {
        string ids = Request.Form["ids"];
        string sql = "delete from Db_User where id in (" + ids + ") ";
        int count = OleDbHelper.ExecuteSql(sql);
        if (count > 0)
        {
            Response.Write("success");
            Response.End();
        }
        else
        {
            Response.Write("error");
            Response.End();
        }
    }
    protected void getDataRows()
    {
        String sql = "select count(*) from Db_User";
        int count = OleDbHelper.getCountRows(sql);
        Response.Write(count.ToString());
        Response.End();
    }
}