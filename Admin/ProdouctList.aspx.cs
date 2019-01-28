using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProdouctList : cAdmin
{
    public string url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] == "bindProdouct")
        {
            bindProdouct();
        }
        else if (Request.QueryString["action"] == "addProdouct")
        {
            addProdouct();
        }
        else if (Request.QueryString["action"] == "edit")
        {
            edit();
        }
        else if (Request.QueryString["action"] == "editProdouct")
        {
            editProdouct();
        }
        else if (Request.QueryString["action"] == "deleteProdouct")
        {
            deleteProdouct();
        }
      
    }

    protected void bindProdouct()
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
            total = OleDbHelper.getCountRows("select Count(*) from Db_Prodouct");

            if (page == 1)
            {
                sql = "select top " + rows + " ID,pImagePath,pTitle,pPrice,pType,pCount,pDate from Db_Prodouct  order by " + sort + " " +
                      order;
            }

            else
            {
                sql = "select top " + rows +
                      " ID,pImagePath,pTitle,pPrice,pType,pCount,pDate from Db_Prodouct where ID not in (select top " + count +
                      " ID from Db_Prodouct order by " + sort + " " + order + ") order by " + sort + " " + order;
            }
        }
        #endregion
        DataTable dt = OleDbHelper.GetDataTable(sql);
        string data = CommonHelper.DataTable2Json(dt);
        Response.Write("{\"total\":" + total + "," + data + "}");
        Response.End();
    }
    protected void addProdouct()
    {
     
       string pImagePath = Session["imagepath"].ToString();
       string pTitle = Request.Form["pTitle"];
       string pPrice = Request.Form["pPrice"];
       string pType = Request.Form["pType"];
       DateTime pDate = DateTime.Now;
       string pCount = Request.Form["pCount"];
       string pContent = Request.Form["pContent"];
       string sql = "insert into Db_Prodouct(pImagePath,pTitle,pPrice,pType,pDate,pCount,pContent)values(@pImagePath,@pTitle,@pPrice,@pType,@pDate,@pCount,@pContent)";
       int i = OleDbHelper.ExecuteSql(sql, new OleDbParameter("@pImagePath", pImagePath),
               new OleDbParameter("@pTitle", pTitle),
               new OleDbParameter("@pPrice", pPrice),
               new OleDbParameter("@pType", pType),
               new OleDbParameter("@pDate", pDate.ToString()),
               new OleDbParameter("@pCount", pCount),
               new OleDbParameter("@pContent", pContent));
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
    protected void edit()
    {
        int id = Convert.ToInt32(Request.Form["ID"]);
        string sql = "select * from Db_Prodouct where ID=" + id;
        DataTable dt = OleDbHelper.GetDataTable(sql);
        string[] data = new string[6];
        data[0] = dt.Rows[0]["pImagePath"].ToString();
        //Image.imagepath = data[0].ToString();
       // Session["image2path"] = data[0].ToString();
        url = data[0].ToString();
        data[1] = dt.Rows[0]["pTitle"].ToString();
        data[2] = dt.Rows[0]["pPrice"].ToString();
        data[3] = dt.Rows[0]["pType"].ToString();
        data[4] = dt.Rows[0]["pCount"].ToString();
        data[5] = dt.Rows[0]["pContent"].ToString();
        Response.Write(data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + data[5]);
        Response.End();
    }

    protected void editProdouct()
    {
        int id = Convert.ToInt32(Request.Form["ID"]);
        string pImagePath = Session["imagepath"].ToString();
        string pTitle = Request.Form["pTitle"];
        string pPrice = Request.Form["pPrice"];
        string pType = Request.Form["pType"];
        DateTime pDate = DateTime.Now;
        string pCount = Request.Form["pCount"];
        string pContent = Request.Form["pContent"];
        string sql = "update Db_Prodouct set pImagePath=@pImagePath,pTitle=@pTitle,pPrice=@pPrice,pType=@pType,pDate=@pDate,pCount=@pCount,pContent=@pContent where ID=@id";
        int i = OleDbHelper.ExecuteSql(sql,
            new OleDbParameter("@pImagePath", pImagePath),
            new OleDbParameter("@pTitle", pTitle),
            new OleDbParameter("@pPrice", pPrice),
            new OleDbParameter("@pType", pType),
            new OleDbParameter("@pDate", pDate.ToShortDateString()),
            new OleDbParameter("@pCount", pCount),
            new OleDbParameter("@pContent", pContent),

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

    protected void deleteProdouct()
    {
        string ids = Request.Form["ids"];
        string sqlsel = "select pCount from Db_Prodouct where id in (" + ids + ") ";
        DataTable dt = OleDbHelper.GetDataTable(sqlsel);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["pCount"].ToString()) > 0)
            {
                Response.Write("error");
                Response.End();
                return;
            }
        }
        string sql = "delete from Db_Prodouct where id in (" + ids + ") ";
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
