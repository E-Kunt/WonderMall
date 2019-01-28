<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminList.aspx.cs" Inherits="Admin_AdminList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>

    <script src="../js/AdminList.js"></script>

    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
</head>
<body>
    <table id="manager">
    </table>

    <!-- 管理员模块的工具栏 -->
    <div id="manager_tool" style="padding: 5px;">
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="manager_tool.add();">添加</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="manager_tool.edit();">修改</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-delete-new" plain="true" onclick="manager_tool.delete1();">删除</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="manager_tool.redo();">取消选择</a>
             查询管理员：<input type="text" class="textbox" name="search_manager" style="width: 110px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="manager_tool.search();">查询</a>
        </div>
    </div>

    <!--管理员添加模块-->
    <form id="manager_add" style="margin: 0; padding: 5px 0 0 25px; color: #333">
        <p>管理员账号:<input type="text" name="username" class="textbox" style="width: 200px;" /></p>
        <p>管理员密码:<input type="password" name="password" class="textbox" style="width: 205px" /></p>
        <p>
            分配权限:<select class="power"></select><input type="hidden" id="userpower"/>
        </p>
    </form>
    <form id="manager_edit" style="margin: 0; padding: 5px 0 0 25px; color: #333">
        <p>管理员账号:<input type="text" name="username1" disabled="true" class="textbox" style="width: 200px;" /></p>
        <p>管理员密码:<input type="password" name="password1" class="textbox" style="width: 205px" /></p>
        <p>
            分配权限:<select class="power" runat="server"></select><input type="hidden" id="userpower1"/>
        </p>
    </form>
    
</body>
</html>
