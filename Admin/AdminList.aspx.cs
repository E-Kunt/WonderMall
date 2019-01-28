using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

public partial class Admin_AdminList : cAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] == "bindAdmin")
        {
            bindAdmin();
        }
        else if (Request.QueryString["action"] == "addAdmin")
        {
            addAdmin();
        }
        else if (Request.QueryString["action"] == "edit")
        {
            edit();
        }
        else if (Request.QueryString["action"] == "editAdmin")
        {
            editAdmin();
        }
        else if (Request.QueryString["action"] == "deleteAdmin")
        {
            deleteAdmin();
        }
    }

    protected void bindAdmin()
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
            total = OleDbHelper.getCountRows("select Count(*) from Db_Admin");
           
            if (page == 1)
            {
                sql = "select top " + rows + " ID,username,createDate,userpower from Db_Admin  order by " + sort + " " +
                      order;
            }

            else
            {
                sql = "select top " + rows +
                      " ID,username,createDate,userpower from Db_Admin where ID not in (select top " + count +
                      " ID from Db_Admin order by " + sort + " " + order + ") order by " + sort + " " + order;
            }
        }
        #endregion
        #region 是查询的时候绑定数据

        else
        {
            string title = Request.Form["title"];
            total = OleDbHelper.getCountRows("select Count(*) from Db_Admin where username like '%"+title+"%'");
            if (page == 1)
            {
                sql = "select top " + rows + " ID,username,createDate,userpower from Db_Admin where  username like '%" + title + "%' order by " + sort + " " +
                      order;
            }

            else
            {
                sql = "select top " + rows +
                      " ID,username,createDate,userpower from Db_Admin where ID not in (select top " + count +
                      " ID from Db_Admin where username like '%" + title + "%' order by " + sort + " " + order + ") and username like '%" + title + "%' order by " + sort + " " + order;
            }
        }
        #endregion
        DataTable dt = OleDbHelper.GetDataTable(sql);
        string data = CommonHelper.DataTable2Json(dt);
        Response.Write("{\"total\":" + total + "," + data + "}");
        Response.End();
    }
    protected void addAdmin()
    {
        string username = Request.Form["username"];
        string password = CommonHelper.GetMD5(Request.Form["password"].ToString().Trim() + CommonHelper.GetPawSalt());
        string userpower = Request.Form["userpower"];
        DateTime createDate = DateTime.Now;
        string checksql = "select count(*) from Db_Admin where username=@username";
        int count = OleDbHelper.getCountRows(checksql, new OleDbParameter("@username", username));
        if (count >= 1)
        {
            Response.Write("error");
            Response.End();
        }
        else
        {
            string sql = "insert into Db_Admin(username,userpassword,userpower,createDate)values(@username,@password,@userpower,@createDate)";
            int i = OleDbHelper.ExecuteSql(sql, new OleDbParameter("@username", username),
                new OleDbParameter("@password", password),
                new OleDbParameter("@userpower", userpower),
                new OleDbParameter("@createDate", createDate.ToString()));
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
        string sql = "select * from Db_Admin where ID=" + id;
        DataTable dt = OleDbHelper.GetDataTable(sql);
        string []data=new string[2];
        data[0] = dt.Rows[0]["username"].ToString();
        data[1] = dt.Rows[0]["userpower"].ToString();
        Response.Write(data[0]+","+data[1]);
        Response.End();
    }

    protected void editAdmin()
    {
        int id = Convert.ToInt32(Request.Form["ID"]);
        string password = CommonHelper.GetMD5(Request.Form["password"].ToString().Trim() + CommonHelper.GetPawSalt());
        string userpower = Request.Form["userpower"];
        string sql = "update Db_Admin set userpassword=@password,userpower=@userpower where ID=@id";
        int i = OleDbHelper.ExecuteSql(sql, new OleDbParameter("@password1", password),
            new OleDbParameter("@userpower", userpower),
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

    protected void deleteAdmin()
    {
        string ids = Request.Form["ids"];
        string sql = "delete from Db_Admin where id in (" + ids + ") ";
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
}