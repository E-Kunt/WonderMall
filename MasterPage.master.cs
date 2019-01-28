using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public static string tip = "您尚未登录，请登录！";
    public static string btn = "登录", btnurl = "userLogin.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["usname"] != null)
            {
                tip = "欢迎用户:" + Session["usname"].ToString() + "使用";
                btn = "注销";
                btnurl = "sClear.aspx";
            }
            else
            {
                tip = "您尚未登录，请登录！";
                btn = "登录";
                btnurl = "userLogin.aspx";
            }
        }
    }
}
