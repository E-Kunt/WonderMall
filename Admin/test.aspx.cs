using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["action"] == "jishi")
            {
                Response.Write(getCountRows("select count(*) from Db_Admin"));
                Response.End();
            }
        }
       
    }
    public static string getDBCON()
    {
        return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["dbpath"].ToString());
    }
    public static int getCountRows(string sql, params OleDbParameter[] parameters)
    {
        string connectionString = getDBCON();
        using (OleDbConnection con = new OleDbConnection(connectionString))
        {
            con.Open();
            using (OleDbCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}