using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// CommonHelper 的摘要说明
/// </summary>
public class CommonHelper
{
    public static string GetMD5(string sDataIn)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] bytValue, bytHash;
        bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
        bytHash = md5.ComputeHash(bytValue);
        md5.Clear();
        string sTemp = "";
        for (int i = 0; i < bytHash.Length; i++)
        {
            sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
        }
        return sTemp.ToLower();
    }

    public static string GetPawSalt()
    {
        string salt = ConfigurationManager.AppSettings["pawsalt"].ToString();
        return salt;
    }
    /// <summary>
    /// 将DataTable转换成JSON格式的数据
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DataTable2Json(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("\"rows");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
               if (dt.Columns[j].ColumnName == "pImagePath")
               {
                   jsonBuilder.Append("\":\"");
                   jsonBuilder.Append(" <a class='smallimage' rel='" + dt.Rows[i][j].ToString() + "'><img src='" + dt.Rows[i][j].ToString() + "' style='width:150px;height:150px'/></a>");
                   jsonBuilder.Append("\",");
               }
               else
               {
                 jsonBuilder.Append("\":\"");
                 jsonBuilder.Append(dt.Rows[i][j].ToString());
                 jsonBuilder.Append("\",");
               }
                
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }

    public static string DataTable2Json2Orders(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("\"rows");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                if (dt.Columns[j].ColumnName == "oDate")
                {
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd"));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
}