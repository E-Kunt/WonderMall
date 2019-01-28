<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>login</title>
    <link rel="stylesheet" type="text/css" href="css/backstage.css" />
    <script language="javascript" type="text/javascript">
        function btnclick(obtn) {
            var name = document.getElementById("txtName");
            var pwd = document.getElementById("txtPwd");
            var ver = document.getElementById("txtVer");
            if (name.value == "") {
                alert('账号不能为空！');
                return false;
            }
            else if (pwd.value == "") {
                alert('密码不能为空！');
                return false;
            }
            else if (ver.value == "") {
                alert("验证码不能为空！");
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="login">
        <div class="write">
            <div class="user">
                <input class="field" runat="server" type="text" maxlength="20" name="textfield" id="txtName" />
            </div>
            <div class="password">
                <input class="field1" type="password" runat="server" name="textfield2" maxlength="12"
                    id="txtPwd" />
            </div>
            <div class="yzpassword">
                <input class="yz" type="text" maxlength="5" name="textfield3" runat="server" value=" 验证码"
                    id="txtVer" onblur="" />
                <div class="number">
                    <div class="img">
                        <img src="Verification.aspx" alt='看不清楚，双击图片换一张。' ondblclick="this.src= 'Verification.aspx?flag=' + Math.random() "
                            border="0" height="38" />
                    </div>
                </div>
            </div>
            <div class="bt">

                 <asp:Button ID="btnlogin" runat="server" Text=""  OnClick="btnlogin_Click"/>  
            </div>
        </div>
    </div>
    </form>
</body>
</html>