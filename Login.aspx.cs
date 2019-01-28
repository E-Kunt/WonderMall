using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["username"] = "";
            Session["password"] = "";
        }
    }

    protected void login()
    {

        //string username = Request.Form["username"];
        //string password = CommonHelper.GetMD5(Request.Form["password"]+CommonHelper.GetPawSalt());
        //string sql = "select * from Db_Admin where username=@username and password=@password";
        //DataTable dt = OleDbHelper.GetDataTable(sql, new OleDbParameter("@username", username),
        //    new OleDbParameter("@password", password));
        
        //if (dt.Rows.Count == 1)
        //{
        //    Session["username"] = username;
        //    Session["password"] = password;
        //    Response.Write("success");
        //    Response.End();
        //}
        //else
        //{
        //     Response.Write("error");
        //    Response.End();
        //}
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (txtName.Value.Trim().ToString() == "" || txtPwd.Value.Trim().ToString() == "" || txtVer.Value.Trim().ToString() == "")
        {
            Response.Write("<script>alert('请填写必要内容！')</script>");
            return;
        }
        string selcmdstr = "select * from Db_Admin where username=@name";
        OleDbParameter para = new OleDbParameter("@name", txtName.Value.Trim().ToString());
        DataTable dt = OleDbHelper.GetDataTable(selcmdstr, para);
        if (dt.Rows.Count > 0)
        {
            string value = txtPwd.Value.ToString().Trim();
            string password = CommonHelper.GetMD5(value + CommonHelper.GetPawSalt());
            if (password == dt.Rows[0]["userpassword"].ToString())
            {
                if (Session["check"] == null)
                    return;
                if (txtVer.Value.Trim().ToString() == Session["check"].ToString())
                {
                    Session["username"] = txtName.Value.Trim().ToString();
                    Session["password"] = CommonHelper.GetMD5(txtPwd.Value.Trim().ToString() + CommonHelper.GetPawSalt());
                    Response.Redirect("~/Admin/index.aspx");
                }
                else
                {
                    Response.Write("<script>alert('验证码错误')</script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('密码错误！')</script>");
                return;
            }
        }
        else
        {
            Response.Write("<script>alert('不存在此用户')</script>");
            return;
        }
    }
}