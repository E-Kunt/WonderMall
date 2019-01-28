<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="orders.aspx.cs" Inherits="orders" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>订单</title>
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <!--jQuery(necessary for Bootstrap's JavaScript plugins)-->
    <script src="js/jquery-1.11.0.min.js"></script>
    <!--Custom-Theme-files-->
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
    <!--start-ckeckout-->
    <div class="ckeckout">
        <div class="container">
            <div class="ckeck-top heading">
                <h2>订单</h2>
            </div>
            <div class="ckeckout-top">
                <div class="cart-items">
                    <h3>我的订单</h3>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h3></h3>

                    <div class="in-check">
                        <ul class="unit">
                            <li id="numorders"><span>订单号</span></li>
                            <li id="itemorders"><span>商品名称</span></li>
                            <li id="odate"><span>日期</span></li>
                            <li id="priceorders"><span>总价</span></li>
                            <li id="stateorders"><span>状态</span></li>
                            <li></li>
                            <div class="clearfix"></div>
                        </ul>
                        <asp:Repeater ID="rptbind" runat="server">
                            <ItemTemplate>
                                <ul class="cart-header">
                                    <div class="close1">
                                        <asp:Button ID="btnMake" runat="server" OnClick="btnMake_Click" CommandArgument='<%#Eval("ID") %>' Text='<%#Eval("btnText") %>' />
                                    </div>
                                    <li class="nameo"><span class="name"><%#Eval("oNum").ToString() %></span></li>
                                    <li class="projecto"><span class="project"><%#Eval("oProject") %></span></li>
                                    <li class="odate"><span class="cost"><%#Eval("oDate","{0:yyyy年MM月dd日}").ToString() %></span></li>
                                    <li class="costo"><span class="cost"><%#Eval("oPrice") %>元</span></li>
                                    <li><span class="ostate"><%#Eval("oState") %></span>
                                        <p></p>
                                    </li>
                                    <div class="clearfix"></div>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" CssClass="pages"
                    PageSize="8" CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager1_PageChanged"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowNavigationToolTip="True">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </div>
    <!--end-ckeckout-->
</asp:Content>

