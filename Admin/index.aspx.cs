using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_index : cAdmin
{
    protected string username = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            username = Session["username"].ToString();
        }
        #region 退出登录
        if (Request.QueryString["action"] == "loginout")
        {
            loginout();
        }
        #endregion
    }

    protected void loginout()
    {

        Session.Abandon();
        Response.Write("success");
        Response.End();
    }
}