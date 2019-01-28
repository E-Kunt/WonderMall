<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Admin_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>后台登陆</title>
    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>

    <script src="../js/index.js"></script>
    <link href="../css/index.css" rel="stylesheet" />

    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
       
</head>
<body>
    <body class="easyui-layout">
        <div data-options="region:'north',
            noheader:true,
             border:false,
             split:true,
             " style="height:60px;background-color: #666">
            <div class="logo">后台管理</div> 
            <div class="logout"> 您好， <%:username %>| <a href="#" onclick="obj.loginout();"> 退出</a> </div>
        </div>
        <div data-options="region:'south',title:'footer',noheader:true,split:true" style="height: 35px;line-height: 30px;text-align: center">
           &Oslash; 2015 Power By Software Center.
        </div>
        <div data-options="region:'west',
            title:'导航栏',
             border:false,
             split:true,
             iconCls:'icon-world',
             collapsible:true,
             "
            style="width: 180px; padding: 10px">
            <ul class="easyui-tree" data-options="lines:true">
                <li><span>管理员管理</span>
                    <ul>
                        <li>
                            <span><a href="javascript:void(0);"  icon="icon-manager" class="link" url="AdminList.aspx" style="color:#000000">管理员列表</a></span>
                        </li>
                    </ul>
                </li>
                <li><span>用户管理</span>
                    <ul>
                        <li>
                            <span><a href="javascript:void(0);"  icon="icon-user" class="link" url="UserList.aspx" style="color: #000000">用户列表</a></span>
                        </li>
                    </ul>
                </li>
                  <li><span>产品管理</span>
                    <ul>
                        <li>
                            <span><a href="javascript:void(0);"  icon="icon-user" class="link" url="ProdouctList.aspx" style="color: #000000">产品列表</a></span>
                        </li>
                    </ul>
                </li>
                <li><span>订单管理</span>
                    <ul>
                        <li>
                            <span><a href="javascript:void(0);"  icon="icon-user" class="link" url="OrdersList.aspx" style="color: #000000">未完成订单</a></span>
                        </li>
                        <li>
                            <span><a href="javascript:void(0);"  icon="icon-user" class="link" url="OrdersList_Fin.aspx" style="color: #000000">已完成订单</a></span>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
        <div data-options="region:'center'" style="overflow:hidden" id="center">
            <div id="tabs">
                <div title="起始页" iconCls="icon-house" style="padding :0 10px;display: block;">
                    <p>欢迎登陆后台管理系统</p>
                </div>
            </div>
        </div>
    </body>
</body>
</html>
