<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="single.aspx.cs" Inherits="single" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Single</title>
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
    <script type="text/javascript">
        $(function () {

            var menu_ul = $('.menu_drop > li > ul'),
                   menu_a = $('.menu_drop > li > a');

            menu_ul.hide();

            menu_a.click(function (e) {
                e.preventDefault();
                if (!$(this).hasClass('active')) {
                    menu_a.removeClass('active');
                    menu_ul.filter(':visible').slideUp('normal');
                    $(this).addClass('active').next().stop(true, true).slideDown('normal');
                } else {
                    $(this).removeClass('active');
                    $(this).next().stop(true, true).slideUp('normal');
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--start-logo-->
    <div class="logo">
        <a href="index.aspx">
            <h1>淘奇网</h1>
        </a>
    </div>
    <!--start-logo-->

    <!--bottom-header-->
    <div class="header-bottom">
        <div class="container">
            <div class="header">
                <div class="col-md-9 header-left">
                    <div class="top-nav">
                        <ul class="memenu skyblue">
                            <li class="active"><a href="index.aspx">首页</a></li>
                            <li class="grid"><a href="products.aspx?item=装饰摆饰">装饰摆饰</a>

                            </li>
                            <li class="grid"><a href="products.aspx?item=厨房餐饮">厨房餐饮</a>

                            </li>
                            <li class="grid"><a href="products.aspx?item=办公文具">办公文具</a>

                            </li>
                            <li class="grid"><a href="products.aspx?item=玩具娱乐">玩具娱乐</a>

                            </li>
                            <li class="grid"><a href="products.aspx?item=智能科技">智能科技</a>

                            </li>
                            <li class="grid"><a href="products.aspx?item=全部">全部</a>
                            </li>
                        </ul>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-3 header-right">
                    <div class="search-bar">
                        <asp:TextBox ID="txtQuery" runat="server" Text="搜索玩意儿"  onfocus="this.value = '';" onblur="if (this.value == '') {this.value = '搜索玩意儿';}"></asp:TextBox>
                        <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" />
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!--bottom-header-->
    <!--start-breadcrumbs-->
    <div class="breadcrumbs">
        <div class="container">
            <div class="breadcrumbs-main">
                <ol class="breadcrumb">
                    <li><a href="index.aspx">首页</a></li>
                    <li class="active"><%=w_Title %></li>
                </ol>
            </div>
        </div>
    </div>
    <!--end-breadcrumbs-->
    <!--start-single-->
    <div class="single contact">
        <div class="container">
            <div class="single-main">
                <div class="col-md-9 single-main-left">
                    <div class="sngl-top">
                        <div class="col-md-5 single-top-left">
                            <div class="flexslider">
                                <ul class="slides">
                                    <li data-thumb="images/s-1.jpg">
                                        <div class="thumb-image">
                                            <img src='<%=w_Path %>' data-imagezoom="true" class="img-responsive" alt="" />
                                        </div>
                                    </li>

                                </ul>
                            </div>
                            <!-- FlexSlider -->
                            <script src="js/imagezoom.js"></script>
                            <script defer src="js/jquery.flexslider.js"></script>
                            <link rel="stylesheet" href="css/flexslider.css" type="text/css" media="screen" />

                            <script>
                                // Can also be used with $(document).ready()
                                $(window).load(function () {
                                    $('.flexslider').flexslider({
                                        animation: "slide",
                                        controlNav: "thumbnails"
                                    });
                                });
                            </script>
                        </div>
                        <div class="col-md-7 single-top-right">
                            <div class="single-para simpleCart_shelfItem">
                                <h2><%=w_Title %></h2>
                                <div class="star-on">
                                    <ul class="star-footer">
                                        <li><a href="#"><i></i></a></li>
                                        <li><a href="#"><i></i></a></li>
                                        <li><a href="#"><i></i></a></li>
                                        <li><a href="#"><i></i></a></li>
                                        <li><a href="#"><i></i></a></li>
                                    </ul>
                                    <div class="clearfix"></div>
                                </div>

                                <h5 class="item_price">￥ <%=w_Price %></h5>
                                <p><%=w_Info %></p>
                                <div class="available">
                                    <!--<ul>
                                    <li class="size-in"></li>
                                        <li class="size-in">尺寸<select>
                                            <option>特大</option>
                                            <option>大</option>
                                            <option selected="selected">中</option>
                                            <option>小</option>
                                        </select></li>
                                        <div class="clearfix"></div>
                                    </ul>-->
                                </div>
                                <ul class="tag-men">
                                    <li><span>分类</span>
                                        <span class="women1">: <%=w_Type %></span></li>
                                    <li><span>库存</span>
                                        <span class="women1">: <%=w_Count %></span>个</li>
                                </ul>
                                <asp:LinkButton ID="btnAddShop" runat="server" CssClass="add-cart item_add" OnClick="btnAddShop_Click">加入购物车</asp:LinkButton>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <div class="latestproducts">
                        <div class="product-one">
                            <asp:Repeater ID="rptAbout" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4 product-left p-left">
                                        <div class="product-main simpleCart_shelfItem">
                                            <a href='single.aspx?id=<%#Eval("ID") %>' class="mask">
                                                <img class="img-responsive zoom-img" src="<%#Eval("pImagePath").ToString().Substring(3) %>" alt="" /></a>
                                            <div class="product-bottom">
                                                <h3><%#Eval("pTitle").ToString().Length>=15?Eval("pTitle").ToString().Substring(0,15):Eval("pTitle").ToString() %></h3>
                                                <p><%#Eval("pContent").ToString().Length>=20?Eval("pContent").ToString().Substring(0,20):Eval("pContent").ToString() %></p>
                                                <h4><a class="item_add" href="single.aspx?id=<%#Eval("ID") %>"><i></i></a><span class=" item_price">￥ 329</span></h4>
                                            </div>
                                            <div class="srch">
                                                <span>-50%</span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 single-right">
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    </div>
	<!--end-single-->
</asp:Content>

