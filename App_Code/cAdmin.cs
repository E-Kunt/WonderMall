using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// cAdmin 的摘要说明
/// </summary>
public class cAdmin:cMain
{
    protected cAdmin()
    {
        this.Load += new EventHandler(cAdmin_Load);
    }
    //加载时判断session是否为空，不为空时是否有权限访问对应的页面
    private void cAdmin_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Write("<script>alert('您尚未登录！请登录后重试');window.top.location='../Login.aspx'</script>");
            Response.End();
        }
    }
}