using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
/// AjaxSystem 的摘要说明
/// </summary>
public class AjaxSystem
{
    public AjaxSystem()
    {

    }
    /// <summary>
    /// 在ASP.NET环境中弹出一个对话框
    /// </summary>
    /// <param name="response"></param>
    /// <param name="message"></param>
    public static void ShowDialog(HttpResponse response, string message)
  {
   response.Write("<script>alert(" + message + ");</script>");
  }
    /// <summary>
    /// 在ajax环境中弹出一个对话框
    /// </summary>
    /// <param name="button"></param>
    /// <param name="message"></param>
    public static void ShowAjaxDialog(Button button, string message)
    {
        ScriptManager.RegisterClientScriptBlock(button, typeof(Button), button.ClientID, message, true);
    }
    ///
    public static void ShowAjaxDialog(Page page, string message)
    {
        ScriptManager.RegisterClientScriptBlock(page,typeof(Page),DateTime.Now.ToString().Replace(":"," "),"alert('"+message+"')",true);
    }
    /// <summary>
    ///  根据Value属性把列表控件的选择项设置为指定项
    /// </summary>
    /// <param name="list"></param>
    /// <param name="value"></param>
    public static void ListSelectedItemByValue(ListControl list, string value)
    {
        if (list == null) return;
        ///选择项为空
        if (list.Items.Count <= 0)
        {
            list.SelectedIndex = -1;
            return;
        }
        ///逐项进行比较，设置选择项
        for (int i = 0; i < list.Items.Count; i++)
        {
            if (list.Items[i].Value == value)
            {
                list.SelectedIndex = i;
                return;
            }
        }
        ///没有符合条件的选择项
        list.SelectedIndex = -1;
    }
    /// <summary>
    /// 根据Text属性把列表控件的选择项设置为指定项
    /// </summary>
    /// <param name="list"></param>
    /// <param name="text"></param>
    public static void ListSelectedItemByText(ListControl list, string text)
    {
        if (list == null) return;
        ///选择项为空
        if (list.Items.Count <= 0)
        {
            list.SelectedIndex = -1;
            return;
        }
        ///逐项进行比较，设置选择项
        for (int i = 0; i < list.Items.Count; i++)
        {
            if (list.Items[i].Text == text)
            {
                list.SelectedIndex = i;
                return;
            }
        }
        ///没有符合条件的选择项
        list.SelectedIndex = -1;
    }
    /// <summary>
    /// 根据时间创建字符串
    /// </summary>
    /// <returns></returns>
    public static string CreateDateTimeString()
    {
        DateTime now = DateTime.Now;
        string dtString = now.Year.ToString()
         + now.Month.ToString().PadLeft(2, '0')
         + now.Day.ToString().PadLeft(2, '0')
         + now.Hour.ToString().PadLeft(2, '0')
         + now.Minute.ToString().PadLeft(2, '0')
         + now.Second.ToString().PadLeft(2, '0')
         + now.Millisecond.ToString().PadLeft(3, '0');
        return (dtString);
    }
}