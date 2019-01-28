using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;

public partial class check : System.Web.UI.Page
{
    private static string u_ID = "", u_Shop = "", w_ListID = "", w_ID = "";
    private static string[] w_IDs;
    public static int num = 0, sum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getCart();
        }
    }

    private void getCart()
    {
        if (Session["userid"] != null)
        {
            StringBuilder listString = new StringBuilder();
            u_ID = Session["userid"].ToString();
            string struser = "select * from Db_User where ID=" + u_ID;
            DataTable dtuser = OleDbHelper.GetDataTable(struser);
            if (dtuser.Rows.Count > 0)
            {
                u_Shop = dtuser.Rows[0]["userShopCart"].ToString();
                string strbind = "";
                if (!u_Shop.Equals(""))
                {
                    string[] s_c = u_Shop.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    w_IDs = new string[s_c.Length / 2 + 1];
                    int j = 0;
                    //添加temp参数
                    for (int i = 0; i < s_c.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (s_c[i] != "")
                            {
                                w_IDs[j] = s_c[i];
                                string strsql = "update Db_Prodouct set temp='" + (Convert.ToInt32(s_c[i + 1]) / 1000).ToString() + "' where ID=" + w_IDs[j];
                                int flag = OleDbHelper.ExecuteSql(strsql);
                                if (flag < 1)
                                    Response.Write("<script>alert('发生错误1！')</script>");
                                j++;
                            }
                        }
                    }
                    //获取ID列
                    w_ListID = "";
                    foreach (string s in w_IDs)
                    {
                        if (s != "")
                        {
                            w_ListID += s + ",";
                        }
                    }

                    strbind = "select * from Db_Prodouct where ID in(" + w_ListID.Substring(0, w_ListID.Length - 1) + ")";
                }
                else
                {
                    strbind = "select * from Db_Prodouct where ID=0";
                }
                DataTable dtbind = OleDbHelper.GetDataTable(strbind);
                num = 0;
                sum = 0;
                for (int i = 0; i < dtbind.Rows.Count; i++)
                {
                    num += Convert.ToInt32(dtbind.Rows[i]["temp"].ToString());
                    sum += Convert.ToInt32(dtbind.Rows[i]["temp"].ToString()) * Convert.ToInt32(dtbind.Rows[i]["pPrice"].ToString());
                }
                rptbind.DataSource = dtbind;
                rptbind.DataBind();

                //清空temp
                string strdel = "update Db_Prodouct set temp=''";
                int f = -1;
                f = OleDbHelper.ExecuteSql(strdel);
                if (f == -1)
                    Response.Write("<script>alert('发生错误2！')</script>");
            }
            else
            {
                Response.Redirect("sClear.aspx");
            }
        }
        else
        {
            Response.Redirect("userLogin.aspx");
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        w_ID = btn.CommandArgument.ToString();
        string strsel = "select * from Db_user where ID=" + u_ID;
        DataTable dtsel = OleDbHelper.GetDataTable(strsel);
        if (dtsel.Rows.Count > 0)
        {
            u_Shop = dtsel.Rows[0]["userShopCart"].ToString();
        }
        if (u_Shop.Equals(""))
        {
            Response.Write("<script>alert('发生错误3！')</script>");
            return;
        }
        else
        {
            string[] stru_s = u_Shop.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int i;
            for (i = 0; i < stru_s.Length; i++)
            {
                if (w_ID == stru_s[i])
                {
                    int count = Convert.ToInt32(stru_s[i + 1]);
                    count -= 1000;
                    if (count != 0)
                    {
                        stru_s[i + 1] = count.ToString();
                        break;
                    }
                    else
                    {
                        for (int j = i; j < stru_s.Length - 2; j++)
                        {
                            stru_s[j] = stru_s[j + 2];
                        }
                        stru_s[stru_s.Length - 1] = "";
                        stru_s[stru_s.Length - 2] = "";
                        break;
                    }
                }
            }
            u_Shop = "";
            foreach (string s in stru_s)
            {
                if (s != "")
                {
                    u_Shop += s + ",";
                }
            }
            if (i == stru_s.Length)
            {
                Response.Write("<script>alert('发生错误4！')</script>");
                return;
            }
        }
        string strudp = "update Db_User set userShopCart=@shop where ID=@id";
        OleDbParameter[] paras = {
                                         new OleDbParameter("@shop",u_Shop),
                                         new OleDbParameter("@id",u_ID)
                                     };
        int flag = OleDbHelper.ExecuteSql(strudp, paras);
        if (flag > 0)
        {
            getCart();
        }
        else
        {
            Response.Write("<script>alert('删除出错，请重新加入购物车')</script>");
            return;
        }
    }

    protected void btnToOrder_Click(object sender, EventArgs e)
    {
        string oNum = "";           //订单号
        string oProject = "";       //订单内容
        string oState = "";         //订单状态
        string oUserID = "";        //订单所属用户
        string oPrice = "";         //订单总价

        //订单号绑定
        DateTime date = DateTime.Now;
        Random ra = new Random();
        oNum = ra.Next(10) + ra.Next(10) + date.ToFileTime().ToString();

        //订单内容绑定
        string strsel = "select * from Db_user where ID=" + u_ID;
        DataTable dtsel = OleDbHelper.GetDataTable(strsel);
        if (dtsel.Rows.Count > 0)
        {
            u_Shop = dtsel.Rows[0]["userShopCart"].ToString();
        }
        if (u_Shop.Equals(""))
        {
            Response.Write("<script>alert('购物车为空，无法提交订单！')</script>");
            return;
        }
        else
        {
            oProject = u_Shop;
        }

        //订单剩余内容绑定
        oUserID = u_ID;
        oState = "待发货";
        oPrice = sum.ToString();

        string strAdd = "insert into Db_Orders(oNum,oProject,oState,oUserID,oPrice,oDate) values(@oNum,@oProject,@oState,@oUserID,@oPrice,@oDate)";
        OleDbParameter[] parasadd = {
                                        new OleDbParameter("@oNum",oNum),
                                        new OleDbParameter("@oProject",oProject),
                                        new OleDbParameter("@oState",oState),
                                        new OleDbParameter("@oUserID",oUserID),
                                        new OleDbParameter("@oPrice",oPrice),
                                        new OleDbParameter("@oDate",date.ToShortDateString().ToString())
                                    };

        int f_add = OleDbHelper.ExecuteSql(strAdd, parasadd);
        if (f_add > 0)
        {
            string strudp = "update Db_User set userShopCart=@shop where ID=@id";
            OleDbParameter[] paras = {
                                         new OleDbParameter("@shop",""),
                                         new OleDbParameter("@id",u_ID)
                                     };
            int flag = OleDbHelper.ExecuteSql(strudp, paras);
            if (flag <= 0)
            {
                Response.Write("<script>alert('出错5，请重新下订单')</script>");
                return;
            }

            //成功后要减少商品数量
            if (!u_Shop.Equals(""))
            {
                string[] s_c = u_Shop.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                w_IDs = new string[s_c.Length / 2 + 1];
                //减少商品数量
                for (int i = 0; i < s_c.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (s_c[i] != "")
                        {
                            string strsqlsel = "select pCount from Db_Prodouct where ID=" + s_c[i];
                            DataTable dt1 = OleDbHelper.GetDataTable(strsqlsel);
                            int tempC = Convert.ToInt32(dt1.Rows[0]["pCount"].ToString());
                            tempC -= (Convert.ToInt32(s_c[i + 1]) / 1000);
                            string strsqlexc = "update Db_Prodouct set pCount='" + tempC.ToString() + "' where ID=" + s_c[i];
                            int fl1 = OleDbHelper.ExecuteSql(strsqlexc);
                            if (fl1 < 1)
                                Response.Write("<script>alert('发生错误6！')</script>");
                        }
                    }
                }

            }
            Response.Write("<script>window.location='orders.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('出错6，请重新下订单')</script>");
            return;
        }

        //Response.Write(ra.Next(10)+ra.Next(10)+date.ToFileTime().ToString());
    }
}