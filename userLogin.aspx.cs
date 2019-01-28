using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class userLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Text))
        {
            Response.Write("<script>alert('用户名或密码不能为空！')</script>");
            return;
        }

        string checksql = "select * from Db_User where userName=@username and userpassWord=@password";
        OleDbParameter[] paras = {
                                    new OleDbParameter("@username", txtName.Text),
                                    new OleDbParameter("@password",CommonHelper.GetMD5(txtPassword.Text+CommonHelper.GetPawSalt()))
                                 };
        DataTable dt = OleDbHelper.GetDataTable(checksql, paras);
        if (dt.Rows.Count > 0)
        {
            Session["usname"] = dt.Rows[0]["userName"].ToString();
            Session["userid"] = dt.Rows[0]["ID"].ToString();
            Response.Write("<script>alert('登录成功')</script>");
            Response.Redirect("index.aspx");
        }
        else
        {
            Response.Write("<script>alert('账户或密码错误')</script>");
            return;
        }
    }
}