<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="userLogin.aspx.cs" Inherits="userLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Login</title>
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
    <script>$(document).ready(function () { $(".memenu").memenu(); });</script>
    <!--dropdown-->
    <script src="js/jquery.easydropdown.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--register-starts-->
    <div class="register">
        <div class="container">
            <div class="register-top heading">
                <h2>登录</h2>
            </div>
            <div class="register-main">
                <div class="col-md-3 account-left">
                </div>
                <div class="col-md-6 account-left">
                    <asp:TextBox ID="txtName" runat="server" placeholder="用户名" TabIndex="1" required></asp:TextBox>
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="密码" TabIndex="2" required></asp:TextBox>
                    <div class="address submit">
                        <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />
                    </div>
                </div>
                <div class="col-md-3 account-left">
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!--register-end-->
</asp:Content>

