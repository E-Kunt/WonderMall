<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdouctList.aspx.cs" Inherits="Admin_ProdouctList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>

    <script src="../js/ProdouctList.js"></script>
    <script src="../js/ajaxfileupload.js"></script>

    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />


</head>
<body>
    <table id="prodouct">
    </table>
    <!--导航栏-->
     <div id="prodouct_tool" style="padding: 5px;">
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="prodouct_tool.add();">添加</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="prodouct_tool.edit();">修改</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-delete-new" plain="true" onclick="prodouct_tool.delete1();">删除</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="prodouct_tool.redo();">取消选择</a>
            
        </div>
    </div>
   <!--添加产品模块-->
    <form id="prodouct_add" style="margin: 0; padding: 5px 0 0 25px;overflow:hidden; color: #333" runat="server">
        <p>产品标题:<input type="text" name="pTitle" class="textbox" style="width: 200px;" /></p>
        <p>产品描述:<textarea style="width:350px;height:50px" id="pContent" name="pContent"></textarea></p>
        <p>产品价格:<input type="number" name="pPrice" class="textbox" style="width: 205px" /></p>
        <p>产品分类:<select class="type"></select><input type="hidden" id="pType"/></p>
        <p>产品数量:<input type="number"  class="textbox" name="pCount" style="width: 200px;" /></p>
        <p><iframe src="Default.aspx"  scrolling='no' width='100%' height='250px' frameborder='0'></iframe></p>
    </form>
    
    <!--编辑用户模块-->
    <form id="prodouct_edit" style="margin: 0; padding: 5px 0 0 25px; color: #333">
        <p>产品标题:<input type="text" name="pTitle1" class="textbox" style="width: 200px;" /></p>
        <p>产品描述:<textarea style="width:350px;height:50px" id="pContent1" name="pContent1"></textarea></p>
        <p>产品价格:<input type="number" name="pPrice1" class="textbox" style="width: 205px" /></p>
        <p>产品分类:<select class="type"></select><input type="hidden" id="pType1"/></p>
        <p>产品数量:<input type="number" name="pCount1" class="textbox" style="width: 205px" /></p>
        <p><iframe src="Default.aspx?action=edit"  scrolling='no' width='100%' height='250px' frameborder='0'></iframe></p>
    </form>
</body>
</html>
