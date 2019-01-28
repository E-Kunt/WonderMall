using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///数据库操作类，所有方法为静态
/// </summary>
public class OleDbHelper
{

    //数据库连接
    public static string getDBCON()
    {
        return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["dbpath"].ToString());
    }





    /// <summary>
    /// 执行一条sql语句 指的是增 删 改  返回的是影响的行数
    /// </summary>
    /// <param name="strSQL"></param>
    /// <param name="cmdParms"></param>
    /// <returns></returns>
    public static int ExecuteSql(string strSQL, params OleDbParameter[] cmdParms)
    {
        using (OleDbConnection connn = new OleDbConnection(getDBCON()))
        {
            connn.Open();
            using (OleDbCommand cmd = connn.CreateCommand())
            {
                cmd.CommandText = strSQL;
                cmd.Parameters.AddRange(cmdParms);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
    }


    /// <summary>
    /// 执行带参数查询语句，返回DataTable
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <param name="db">数据库</param>
    /// <param name="cmdParms">参数列表</param>
    /// <returns>DataTable</returns>
    public static DataTable GetDataTable(string strSQL, params OleDbParameter[] cmdParms)
    {
        using (OleDbConnection connn = new OleDbConnection(getDBCON()))
        {
            connn.Open();
            using (OleDbCommand cmd = connn.CreateCommand())
            {
                cmd.CommandText = strSQL;
                cmd.Parameters.AddRange(cmdParms);
                DataTable dt = new DataTable();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
                dataAdapter.Fill(dt);
                cmd.Parameters.Clear();
                return dt;
            }
        }
    }



    /// <summary>
    /// 返回总记录条数
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 返回最后一条数据的ID
    /// </summary>
    /// <param name="tableName">数据表名</param>
    /// <returns></returns>
    public static int getlastID(string tableName)
    {
        string connectionString = getDBCON();
        using (OleDbConnection con = new OleDbConnection(connectionString))
        {
            con.Open();
            using (OleDbCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select max(id) from "+tableName;
                int lastID = Convert.ToInt32(cmd.ExecuteScalar());
                return lastID;
            }
        }
    }
}
