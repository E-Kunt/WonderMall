using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.OleDb;

public partial class orders : System.Web.UI.Page
{
    private static string u_ID = "", u_Shop = "", w_ListID = "", w_ID = "";
    private static string[] w_IDs;
    public int num = 0, sum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getOrders();
        }
    }

    private void getOrders()
    {
        if (Session["userid"] != null)
        {
            StringBuilder listString = new StringBuilder();
            u_ID = Session["userid"].ToString();
            string struser = "select * from Db_Orders where oUserID='" + u_ID + "' and oState<>'已评价' order by ID desc";
            DataTable dtorders = OleDbHelper.GetDataTable(struser);
            dtorders.Columns.Add("btnText", Type.GetType("System.String"));
            if (dtorders.Rows.Count > 0)
            {
                for (int oi = 0; oi < dtorders.Rows.Count; oi++)
                {
                    u_Shop = dtorders.Rows[oi]["oProject"].ToString();
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
                                        Response.Write("<script>alert('1发生错误！')</script>");
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
                    listString.Clear();
                    listString.Append("");
                    for (int i = 0; i < dtbind.Rows.Count; i++)
                    {
                        listString.Append(dtbind.Rows[i]["pTitle"].ToString());
                        listString.Append("*");
                        listString.Append(dtbind.Rows[i]["temp"].ToString());
                        listString.Append("<br />");
                        num += Convert.ToInt32(dtbind.Rows[i]["temp"].ToString());
                        sum += Convert.ToInt32(dtbind.Rows[i]["temp"].ToString()) * Convert.ToInt32(dtbind.Rows[i]["pPrice"].ToString());
                    }

                    if (!dtorders.Rows[oi]["oState"].ToString().Equals("已收货") && !dtorders.Rows[oi]["oState"].ToString().Equals("已评价"))
                    {
                        dtorders.Rows[oi]["btnText"] = "确认收货";
                    }
                    else if (dtorders.Rows[oi]["oState"].ToString().Equals("已收货"))
                    {
                        dtorders.Rows[oi]["btnText"] = "等待评价";
                    }
                    else
                    {
                        dtorders.Rows[oi]["btnText"] = "订单完成";
                    }

                    dtorders.Rows[oi]["oPrice"] = sum.ToString();
                    dtorders.Rows[oi]["oProject"] = listString;

                    //清空temp
                    string strdel = "update Db_Prodouct set temp=''";
                    int f = -1;
                    f = OleDbHelper.ExecuteSql(strdel);
                    if (f == -1)
                        Response.Write("<script>alert('2发生错误！')</script>");
                }
                PagedDataSource pds = new PagedDataSource();
                pds.PageSize = AspNetPager1.PageSize;
                pds.AllowPaging = true;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                AspNetPager1.RecordCount = dtorders.Rows.Count;
                int pagecount = AspNetPager1.PageCount;
                DataView dv = dtorders.DefaultView;
                pds.DataSource = dv;

                rptbind.DataSource = pds;
                rptbind.DataBind();
            }
            else
            {
                Response.Write("<script>alert('你没有未完成订单！')</script>");
            }
        }
        else
        {
            Response.Redirect("userLogin.aspx");
        }
    }
    protected void btnMake_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (btn.Text == "确认收货")
        {
            string strsel = "update Db_Orders set oState='已收货' where ID=" + btn.CommandArgument.ToString();
            int flag = OleDbHelper.ExecuteSql(strsel);
            if (flag > 0)
            {
                getOrders();
            }
            else
            {
                Response.Write("<script>alert('功能失效，请重新点击！')</script>");
                return;
            }
        }
        else if (btn.Text == "等待评价")
        {
            string strsel = "update Db_Orders set oState='已评价' where ID=" + btn.CommandArgument.ToString();
            int flag = OleDbHelper.ExecuteSql(strsel);
            if (flag > 0)
            {
                getOrders();
            }
            else
            {
                Response.Write("<script>alert('功能失效，请重新点击！')</script>");
                return;
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        getOrders();
    }
}