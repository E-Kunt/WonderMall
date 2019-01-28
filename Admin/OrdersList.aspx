<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrdersList.aspx.cs" Inherits="Admin_OrdersList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="../js/OrdersList.js"></script>
    <script src="../js/ajaxfileupload.js"></script>
    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
    <script>
        var count = "";
        function test() {
            $.ajax({
                type: "post",
                url: "OrdersList.aspx?action=jishi",
                success: function (data) {
                    if (count == "") {
                        count = data;
                    } else {
                        if (data > count) {
                            count = data;
                            play_click('../mp3/dingdong.mp3');
                            
                    $.messager.show({
                                    title: '提示',
                                    msg: '有新的订单录入',
                                });
                                $("#orders_edit").dialog('close').form("reset");
                                $("#orders").datagrid("reload")
                        } else if (data < count) {

                            count = data;
                        } else {
                            count = data;
                        }
                    }
//                    if (data >hh) {
//                        alert(data);
//                    }
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
    <table id="orders">
    </table>
    <!--导航栏-->
    <div id="orders_tool" style="padding: 5px;">
        <h1>
            未完成订单</h1>
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="orders_tool.edit();">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="orders_tool.redo();">
                    取消选择</a>
        </div>
    </div>
    <!--编辑模块-->
    <form id="orders_edit" style="margin: 0; padding: 5px 0 0 25px; color: #333">
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
    
    <div id="div1" hidden="true">
    </div>
</body>
</html>
