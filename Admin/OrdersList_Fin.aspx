<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrdersList_Fin.aspx.cs" Inherits="Admin_OrdersList_Fin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>

    <script src="../js/OrdersList_Fin.js"></script>
    <script src="../js/ajaxfileupload.js"></script>

    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
</head>
<body>
     <table id="orders_Fin">
    </table>
    <!--导航栏-->
     <div id="orders_tool" style="padding: 5px;">
         
    <h1>已完成订单</h1>
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="orders_tool_Fin.del();">删除</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="orders_tool_Fin.redo();">取消选择</a>
        </div>
    </div>
</body>
</html>
