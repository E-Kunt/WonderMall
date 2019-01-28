using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class ChangePsw : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["usname"] != null)
            {
                txtName.Text = Session["usname"].ToString();
            }
            else
            {
                Response.Redirect("userLogin.aspx");
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {

        if (txtPassword.Text != txtRePassword.Text)
        {
            Response.Write("<script>alert('两次密码不一致！请重新输入')</script>");
            return;
        }

        if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtAdress.Text))
        {
            Response.Write("<script>alert('用户名，密码或地址不能为空。')</script>");
            return;
        }
        
        string checksql = "select count(*) from Db_User where userName=@username";
        int count = OleDbHelper.getCountRows(checksql, new OleDbParameter("@username", txtName.Text));
        if (count >= 1)
        {
            DateTime createDate = DateTime.Now;
            string txtSex = "";
            if (rbtnMan.Checked == true)
                txtSex = "男性";
            else
                txtSex = "女性";
            string sql = "update Db_User set userpassWord=@password,userSex=@usersex,userCreateDate=@usercreatedate,userEmail=@useremail,userAddress=@useradress,userMobile=@usermobile where userName=@username";
            OleDbParameter[] paras = {
                                            
                                            new OleDbParameter("@password", CommonHelper.GetMD5(txtPassword.Text+CommonHelper.GetPawSalt())),
                                            new OleDbParameter("@usersex", txtSex),
                                            new OleDbParameter("@usercreatedate", createDate.ToShortDateString()),
                                            new OleDbParameter("@useremail",txtEmail.Text),
                                            new OleDbParameter("@useradress",txtAdress.Text),
                                            new OleDbParameter("@usermobile",txtMobile.Text),
                                            new OleDbParameter("@username", txtName.Text)
                                     };
            int i = OleDbHelper.ExecuteSql(sql, paras);
            if (i == 1)
            {
                string str = "select * from Db_User where userName=@username";
                DataTable dt = OleDbHelper.GetDataTable(str, new OleDbParameter("@username", txtName.Text));
                if (dt.Rows.Count > 0)
                {
                    Session["usname"] = dt.Rows[0]["userName"].ToString();
                    Session["userid"] = dt.Rows[0]["ID"].ToString();
                }
                Response.Write("<script>alert('修改成功！');window.location='index.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('修改失败')</script>");
                return;
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPassword.Text = "";
        txtRePassword.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        txtAdress.Text = "";
    }
}