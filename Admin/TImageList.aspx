<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TImageList.aspx.cs" Inherits="Admin_TImageList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="../js/timagesList.js"></script>
    <script src="../js/ajaxfileupload.js"></script>
    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
</head>
<body>
    <table id="timages">
    </table>
    <!--导航栏-->
    <div id="timages_tool" style="padding: 5px;">
        <h1>
            未完成订单</h1>
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="orders_tool.edit();">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="timages_tool.redo();">
                    取消选择</a>
        </div>
    </div>
    <!--编辑模块-->
    <form id="timages_edit" style="margin: 0; padding: 5px 0 0 25px; color: #333">
    <p>
        订单号码:<input type="text" readonly="readonly" name="oNum1" class="textbox" style="width: 200px;" /></p>
    <p>
        订单商品:<textarea style="width: 350px; height: 50px" readonly="readonly" id="oProject1"
            name="oProject1"></textarea></p>
    <p>
        订单用户:<input type="text" readonly="readonly" name="oUserID1" id="oUserID1" class="textbox"
            style="width: 205px" /></p>
    <p>
        订单状态:<select class="state"></select><input type="hidden" id="oState1" />(可修改)</p>
    <p>
        订单日期:<input type="text" readonly="readonly" name="oDate1" class="textbox" style="width: 205px" /></p>
    </form>
</body>
</html>
