<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change.aspx.cs" Inherits="ChangePsw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>修改账户资料</title>
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <!--jQuery(necessary for Bootstrap's JavaScript plugins)-->
    <script src="js/jquery-1.11.0.min.js"></script>
    <!--Custom Theme files-->
    <!--theme-style-->
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <!--//theme-style-->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Luxury Watches Responsive web template, Bootstrap Web Templates, Flat Web Templates, Andriod Compatible web template, 
Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyErricsson, Motorola web design" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!--start-menu-->
    <script src="js/simpleCart.min.js"> </script>
    <link href="css/memenu.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/memenu.js"></script>
    <script>        $(document).ready(function () { $(".memenu").memenu(); });</script>
    <!--dropdown-->
    <script src="js/jquery.easydropdown.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Change-starts-->
    <div class="register">
        <div class="container">
            <div class="register-top heading">
                <h2>修改账户资料</h2>
            </div>
            <div class="register-main">
                <div class="col-md-6 account-left">
                    <asp:TextBox ID="txtName" Text="用户名" runat="server" TabIndex="1" Enabled="False" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtMobile" placeholder="手机" runat="server" TabIndex="4"></asp:TextBox>
                    <asp:TextBox ID="txtEmail" placeholder="邮箱（选填）" runat="server" TabIndex="5" ></asp:TextBox>
                    <ul>
                        <li>
                            <label class="radio left">
                                
                                <asp:RadioButton ID="rbtnMan" GroupName="Sex" runat="server" Checked="true"/><i></i>男士</label></li>
                        <li>
                            <label class="radio">
                                <asp:RadioButton ID="rbtnWoman" GroupName="Sex" runat="server"/><i></i>女士</label></li>
                        <div class="clearfix"></div>
                    </ul>
                </div>
                <div class="col-md-6 account-left">
                    <asp:TextBox ID="txtPassword" placeholder="密码" runat="server" TextMode="Password" TabIndex="2" required></asp:TextBox>
                    <asp:TextBox ID="txtRePassword" placeholder="确认密码" runat="server" TextMode="Password" TabIndex="3" required></asp:TextBox>
                    <asp:TextBox ID="txtAdress" placeholder="收货地址" runat="server" TabIndex="6" required></asp:TextBox>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="address submit">
                <asp:Button ID="btnOK" Text="确认修改" runat="server" OnClick="btnOK_Click" />
                <asp:Button ID="btnClear" Text="重置" runat="server" onclick="btnClear_Click" />
            </div>
        </div>
    </div>
    <!--Change-end-->
</asp:Content>

