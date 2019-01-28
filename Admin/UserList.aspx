<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Admin_UsersList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>

    <script src="../js/UserList.js"></script>

    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
     <script>
         var count = "";

         function test() {
             $.ajax({
                 type: "post",
                 url: "UserList.aspx?action=jishi",
                 success: function (data) {
                     if (count == "") {
                         count = data;
                     } else {
                         if (data > count) {

                             count = data;
                             play_click('../mp3/appleline.mp3');
                         } else if (data < count) {

                             count = data;
                         } else {
                             count = data;
                         }
                     }
                     //if (data >hh) {
                     //    alert(data);
                     //}
                 },
                 error: function () {
                     //asyncRequest(); //服务器抛出错误后继续发送一个请求
                 }
             });
         }
         function play_click(url) {
             var div = document.getElementById('div1');
             div.innerHTML = '<embed src="' + url + '" loop="0" autostart="true" hidden="true"></embed>';
         }

         setInterval("test()", 2000); //每2秒钟执行一次test()
</script>
</head>
<body>
     <%-- 以下这个div只是用来播放音乐用的 --%>
      <div id="div1"></div>
    <!-- 用户列表-->
  <table id="user">
    </table>
    <!--导航栏-->
     <div id="user_tool" style="padding: 5px;">
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="user_tool.add();">添加</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="user_tool.edit();">修改</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-delete-new" plain="true" onclick="user_tool.delete1();">删除</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="user_tool.redo();">取消选择</a>
             查询用户：<input type="text" class="textbox" name="search_user" style="width: 110px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="user_tool.search();">查询</a>
        </div>
    </div>
   <!--添加用户模块-->
    <form id="user_add" style="margin: 0; padding: 5px 0 0 25px; color: #333">
        <p>用户账号:<input type="text" name="userName" class="textbox" style="width: 200px;" /></p>
        <p>用户密码:<input type="password" name="passWord" class="textbox" style="width: 205px" /></p>
        <p>邮箱地址:<input type="text" name="userEmail" class="textbox" style="width: 205px" />
            <input type="text" name="check" style="border-width:0px;color:red" />

        </p>
        <p>性别:<input type="radio" name="userSex" value="男"  />男 
                <input type="radio" name="userSex" value="女"  />女 </p>
        <p>联系方式:<input type="text" name="userTouch" class="textbox" style="width: 200px;" /></p>
        <p>送货地址:<input type="text" name="userAddress" class="textbox" style="width: 200px;" /></p>
    </form>
    <!--编辑用户模块-->
    <form id="user_edit" style="margin: 0; padding: 5px 0 0 25px; color: #333">
        <p>用户账号:<input type="text" name="userName1" class="textbox" disabled="true" style="width: 200px;" /></p>
        <p>用户密码:<input type="password" name="passWord1" class="textbox" style="width: 205px" /></p>
        <p>邮箱地址:<input type="text" name="userEmail1" class="textbox" style="width: 205px" />
            <input type="text" name="check" style="border-width:0px;color:red" />
        </p>
        <p>性别:<input type="text" name="userSex1" class="textbox" disabled="true" style="width: 205px" /></p>
        <p>联系方式:<input type="text" name="userTouch1" class="textbox"  style="width: 200px;" /></p>
        <p>送货地址:<input type="text" name="userAddress1" class="textbox"  style="width: 200px;" /></p>
    </form>
</body>
</html>
