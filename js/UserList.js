$(function () {
    $("#user").datagrid({
        url: "UserList.aspx?action=bindUser",
        fit: true,
        fitColumns: true,//自适应
        rownumbers: true,
        // border: false,
        striped: true,//显示斑马线
        loadMsg: '玩命加载中',
        columns: [[
        {
            field: 'ID',
            title: '自动编号',
            width: 100,
            checkbox: true,
        },
        {
            field: 'userName',
            title: '用户名',
            width: 100,
        },
        {
            field: 'userSex',
            title: '性别',
            width: 100,
            sortable: true,
        },
         {
             field: 'userCreateDate',
             title: '创建时间',
             width: 100,
             sortable: true,
         },
          {
              field: 'userTouch',
              title: '联系方式',
              width: 100,
              sortable: true,
          },
        {
            field: 'userEmail',
            title: '用户邮箱',
            width: 100,
        },
       
       {
         field: 'userAddress',
         title: '送货地址',
         width: 100,
         sortable: true,
        },
        ]],
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 15, 20],
        pageNumber: 1,
        sortName: 'userCreateDate',
        sortOrder: "desc",
        toolbar: "#user_tool",
    });
    user_tool = {
        row: undefined,
        add: function () {
            $('#user_add').dialog('open'); $('input[name="userName"]').focus();
        },
        edit: function () {
            var rows = $("#user").datagrid("getSelections");
            if (rows.length > 1 || rows.length <= 0) {
                $.messager.alert("警告操作", "编辑有且只有一条记录进行编辑", 'warning');
            } else {
                $("#user_edit").dialog('open');
                $('input[name="userName"]').focus();
                this.row = rows[0];
                var ID = this.row.ID;
                /*跳转到后台绑定到编辑页面*/
                $.ajax({
                    url: 'UserList.aspx?action=edit',
                    data: { ID: ID },
                    type: 'post',
                    success: function (data) {
                        var datas = data.split(",");

                        $("input[name=userName1]").val(datas[0]);
                        $("input[name=passWord1]").val(datas[1]);
                        $("input[name=userEmail1]").val(datas[4]);
                        $("input[name=userSex1]").val(datas[2]);
                        $("input[name=userTouch1]").val(datas[3]);
                        $("input[name=userAddress1]").val(datas[5]);


                    },
                    error: function () {
                        $.messager.alert("不存在该用户");
                        return;
                    }
                });
            }
        },
        delete1: function () {
            var rows = $("#user").datagrid("getSelections");
            if (rows.length <= 0) {
                $.messager.alert("警告操作", "至少选择一行进行删除操作", 'warning');

            } else {
                $.messager.confirm('确定操作', '您真的要删除所选的记录吗?', function (flag) {
                    if (flag) {
                        var ids = [];
                        for (i = 0; i < rows.length; i++) {
                            ids[i] = rows[i].ID;
                        }
                        idss = ids.join(","); //将数组变成字符串（用逗号隔开）
                        //alert(idss);
                        //return;
                        $.ajax({
                            url: "UserList.aspx?action=deleteUser",
                            type: "post",
                            data: { ids: idss },
                            success: function (data) {
                                if (data == "success") {

                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除' + ids.length + '个用户成功',
                                    });
                                    $("#user_edit").dialog('close').form("reset");
                                    $("#user").datagrid("reload");
                                } else {
                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除失败',
                                    });
                                }
                            },
                            error: function () {
                                $.messager.alert("警告", "服务器出现异常，请稍后重试", "warning");
                            }
                        });
                    }
                });

            }

        },
        redo: function () {
            $("#user").datagrid("rejectChanges");
        },
        search: function () {
            $("#user").datagrid('load', {
                title: $.trim($("input[name=search_user]").val()),
            });
        }
    };
    /*以下是编辑窗口的设计*/
    $("#user_edit").dialog({
        width: 350,
        title: "编辑用户",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-edit',
            handler: function () {
                if (($("#user_edit").form("validate"))) {
                    $.ajax({
                        url: 'UserList.aspx?action=editUser',
                        type: 'post',
                        data: {
                            ID: user_tool.row.ID,
                            passWord: $("input[name=passWord1]").val(),
                            userEmail: $("input[name=userEmail1]").val(),
                            userTouch: $("input[name=userTouch1]").val(),
                            userAddress: $("input[name=userAddress1]").val(),
                        },
                        success: function (data) {
                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '编辑用户成功',
                                });
                                $("#user_edit").dialog('close').form("reset");
                                $("#user").datagrid("reload");
                            } else {
                                $.messager.alert('警告操作', '用户已存在（或存在其他未知错误）！', 'warning');
                            }
                        },
                        error: function () {
                            $.messager.alert('警告操作', '服务器数据出错！,请稍后重试', 'warning');
                        }
                    });
                }
                //alert($("#userpower").val());
                //return;
            }
        }, {
            text: '取消',
            iconCls: 'icon-redo',
            handler: function () {
                $('#user_edit').dialog('close').form('reset');

            }
        }]
    });
    /*以下是对添加管理员对话框的设置 以及事件*/
    $("#user_add").dialog({
        width: 350,
        title: "添加新用户",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-new',
            handler: function () {
                if ($("input[name='check']").val() == "请输入正确的email地址") {
                    alert("请输入正确的email地址");
                    return;
                }
                //alert($("#userpower").val());
                //return;
                if ($("#user_add").form("validate")) {
                    $.ajax({
                        url: 'UserList.aspx?action=addUser',
                        type: 'post',
                        data: {
                            userName: $("input[name=userName]").val(),
                            passWord: $("input[name=passWord]").val(),
                            userEmail: $("input[name=userEmail]").val(),
                            userTouch: $("input[name=userTouch]").val(),
                            userAddress: $("input[name=userAddress]").val(),
                            userSex: $("input[name=userSex]:checked").val()
                        },
                        success: function (data) {
                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '新增管理员成功',
                                });
                                $("#user_add").dialog('close').form("reset");
                                $("#user").datagrid("reload");
                            } else {
                                $.messager.alert('警告操作', '用户已存在（或存在其他未知错误）！', 'warning');
                            }
                        },
                        error: function () {
                            $.messager.alert('警告操作', '服务器数据出错！,请稍后重试', 'warning');
                        }
                    });
                }
            }
        },
            {
                text: '取消',
                iconCls: 'icon-redo',
                handler: function () {
                    $('#user_add').dialog('close').form('reset');
                }
            }]
    });
    //用户名
    $("input[name=userName]").validatebox({
        required: true,
        validType: 'length[4,20]',
        missingMessage: '用户名不能为空',
        invalidMessage: '用户名长度在2到20位之间',
    });
    //用户密码
    $("input[name=passWord],input[name=passWord1]").validatebox({
        required: true,
        validType: 'length[4,15]',
        missingMessage: '请输入密码',
        invalidMessage: '密码在4-15位',
    });
    $("input[name=userTouch],input[name=userAddress],input[name=userTouch1],input[name=userAddress1]").validatebox({
        required: true,
        validType: 'length[4,80]',
        missingMessage: '请完善信息',
        invalidMessage: '输入的字符长度有误',
    });
    $(function () {
        $("input[name='userEmail'],input[name='userEmail1']").blur(function () {
            var email = $(this).val();
            var reg = /\w+[@]{1}\w+[.]\w+/;
            if (reg.test(email)) {
                $("input[name='check']").val("email合法");
            } else {
                $("input[name='check']").val("请输入正确的email地址");
                return;
            }
        });
    });

});

