using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;

public partial class Admin_OrdersList_Fin : cAdmin
{
    public string url = "";
    private static string u_ID = "", u_Shop = "", w_ListID = "";
    private static string[] w_IDs;
    private static int num = 0, sum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] == "bindOrders")
        {
            bindOrders();
        }
        else if (Request.QueryString["action"] == "del")
        {
            delOrders();
        }
    }

    protected void bindOrders()
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
            total = OleDbHelper.getCountRows("select Count(*) from Db_Orders where oState = '已评价'");

            if (page == 1)
            {
                sql = "select top " + rows + " ID,oNum,oProject,oState,oUserID,oPrice,oDate from Db_Orders where oState = '已评价' order by " + sort + " " +
                      order;
            }

            else
            {
                sql = "select top " + rows +
                      " ID,oNum,oProject,oState,oUserID,oPrice,oDate from Db_Orders where oState = '已评价' and ID not in (select top " + count +
                      " ID from Db_Orders where oState = '已评价'  order by " + sort + " " + order + ") order by " + sort + " " + order;
            }
        }
        #endregion
        DataTable dtorders = OleDbHelper.GetDataTable(sql);
        StringBuilder listString = new StringBuilder();
        if (dtorders.Rows.Count > 0)
        {
            for (int oi = 0; oi < dtorders.Rows.Count; oi++)
            {
                u_Shop = dtorders.Rows[oi]["oProject"].ToString();
                u_ID = dtorders.Rows[oi]["oUserID"].ToString();
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
                                    Response.Write("<script>alert('发生错误！')</script>");
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

                string strname = "select userName from Db_User where ID=" + u_ID;
                DataTable dtname = OleDbHelper.GetDataTable(strname);
                if (dtname.Rows.Count > 0)
                {
                    dtorders.Rows[oi]["oUserID"] = dtname.Rows[0]["userName"].ToString();
                }
                else
                {
                    //Response.Write("<script>alert('缺少用户，请修改用户表！')</script>");
                }

                dtorders.Rows[oi]["oPrice"] = sum.ToString();
                dtorders.Rows[oi]["oProject"] = listString;

                //清空temp
                string strdel = "update Db_Prodouct set temp=''";
                int f = -1;
                f = OleDbHelper.ExecuteSql(strdel);
                if (f == -1)
                    Response.Write("<script>alert('发生错误！')</script>");
            }

        }
        else
        {
            Response.Write("<script>alert('没有订单！')</script>");
        }

        string data = CommonHelper.DataTable2Json2Orders(dtorders);
        Response.Write("{\"total\":" + total + "," + data + "}");
        Response.End();
    }

    protected void delOrders()
    {
        string ids = Request.Form["ids"];
        string sql = "delete from Db_Orders where ID in (" + ids + ") ";
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