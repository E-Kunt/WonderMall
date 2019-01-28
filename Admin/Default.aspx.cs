using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    public static string imgpath = "";
    public static string imgpath2 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            if (FileUpload1.PostedFile.ContentLength > 2097152)
            {
                AjaxSystem.ShowAjaxDialog(this, "图片太大超过2M");
                return;
            }
            string name = DateTime.Now.ToOADate() + FileUpload1.FileName;//获取文件名
            string type2 = name.Substring(name.LastIndexOf(".") + 1);//上传文件后缀名
            string ipath = Server.MapPath("../Imges/" + name); //上传到服务器上后的路径(实际路径),"\\"必须为两个斜杠,在C#中一个斜杠表示转义符.
            string ipath1 = Server.MapPath("../Imges");//创建文件夹时用
            imgpath = "../Imges/" + name;
            if (type2.ToLower() == "jpg" || type2.ToLower() == "gif" || type2.ToLower() == "bmp" || type2.ToLower() == "png" || type2.ToLower() == "jpeg") //根据后缀名来限制上传类型
            {

                if (!System.IO.Directory.Exists(ipath1)) //判断文件夹是否已经存在
                {
                    System.IO.Directory.CreateDirectory(ipath1); //创建文件夹
                }
                FileUpload1.SaveAs(ipath); //上传文件到ipath这个路径里
                
                img1.Src = imgpath;
                Label1.Text = "上传成功！";
                Session["imagepath"] = imgpath;
            }
            else
            {
                AjaxSystem.ShowAjaxDialog(this, "请选择图片进行上传(bmp,gif,jpeg,png格式的");
                return;
            }
        }
        else
        {
            Label1.Text = "上传失败！";
        }
    }

}