$(function () {
    $("#manager").datagrid({
        url: "AdminList.aspx?action=bindAdmin",
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
            field: 'username',
            title: '用户名',
            width: 100,
        },
        {
            field: 'createDate',
            title: '创建时间',
            width: 100,
            sortable: true,
        },
        {
            field: 'userpower',
            title: '管理员权限',
            width: 100,
           
        }
        ]],
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 15, 20],
        pageNumber: 1,
        sortName: 'createDate',
        sortOrder: "desc",
        toolbar: "#manager_tool",
    });

    manager_tool = {
        row: undefined,
        add: function () {
            $('#manager_add').dialog('open'); $('input[name="username"]').focus();
        },
        edit: function () {
            var rows = $("#manager").datagrid("getSelections");
            if (rows.length > 1 || rows.length <= 0) {
                $.messager.alert("警告操作", "编辑有且只有一条记录进行编辑", 'warning');
            } else {
                $("#manager_edit").dialog('open');
                $('input[name="username1"]').focus();
                this.row = rows[0];
                var ID = this.row.ID;
                /*跳转到后台绑定到编辑页面*/
                $.ajax({
                    url: 'AdminList.aspx?action=edit',
                    data: { ID: ID },
                    type: 'post',
                    success: function (data) {
                        var datas = data.split(",");

                        $("input[name=username1]").val(datas[0]);
                        $('.power').combobox('select', datas[1]);
                    },
                    error: function () {
                        $.messager.alert("不存在该用户");
                        return;
                    }
                });
            }
        },
        delete1: function () {
            var rows = $("#manager").datagrid("getSelections");
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
                            url: "AdminList.aspx?action=deleteAdmin",
                            type: "post",
                            data: { ids: idss },
                            success: function(data) {
                                if (data == "success") {

                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除' + ids.length + '个管理员成功',
                                    });
                                    $("#manager_edit").dialog('close').form("reset");
                                    $("#manager").datagrid("reload");
                                } else {
                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除失败',
                                    });
                                }
                            },
                            error: function() {
                                $.messager.alert("警告", "服务器出现异常，请稍后重试", "warning");
                            }
                        });
                    }
                });

            }
            
        },
        redo: function () {
            $("#manager").datagrid("rejectChanges");
        },
        search: function () {
            $("#manager").datagrid('load', {
                title: $.trim($("input[name=search_manager]").val()),
            });
        }

    };
    /*以下是编辑窗口的设计*/
    $("#manager_edit").dialog({
        width: 350,
        title: "编辑管理员",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-edit',
            handler: function () {
                if (($("#manager_edit").form("validate"))) {
                    $.ajax({
                        url: 'AdminList.aspx?action=editAdmin',
                        type: 'post',
                        data: {
                            ID: manager_tool.row.ID,
                            password: $("input[name=password1]").val(),
                            userpower: $("#userpower").val(),
                        },
                        success: function (data) {
                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '编辑管理员成功',
                                });
                                $("#manager_edit").dialog('close').form("reset");
                                $("#manager").datagrid("reload");
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
                $('#manager_edit').dialog('close').form('reset');

            }
        }]
    });
    /*以下是对添加管理员对话框的设置 以及事件*/
    $("#manager_add").dialog({
        width: 350,
        title: "添加管理员",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-new',
            handler: function () {
                //alert($("#userpower").val());
                //return;
                if ($("#manager_add").form("validate")) {
                    $.ajax({
                        url: 'AdminList.aspx?action=addAdmin',
                        type: 'post',
                        data: {
                            username: $("input[name=username]").val(),
                            password: $("input[name=password]").val(),
                            userpower: $("#userpower").val(),
                        },
                        success: function (data) {
                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '新增管理员成功',
                                });
                                $("#manager_add").dialog('close').form("reset");
                                $("#manager").datagrid("reload");
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
                    $('#manager_add').dialog('close').form('reset');
                }
            }]
    });
    //管理员用户名
    $("input[name=username]").validatebox({
        required: true,
        vaildType: 'length[2,20]',
        missingMessage: '管理员用户名不能为空',
        invalidMessage: '管理员用户名长度在2到20位之间',
    });
    //管理员密码
    $("input[name=password],input[name=password1]").validatebox({
        required: true,
        validType: 'length[4,15]',
        missingMessage: '请输入管理员密码',
        invalidMessage: '管理员密码在4-15位',
    });
    //分配权限
    $(".power").combobox({
        width: 200,
        valueField: 'id',
        textField: 'text',
        data: [{
            id: '超级管理员',
            text: '超级管理员',
        },
            {
                id: '普通管理员',
                text: '普通管理员',
            }
        ],
        onSelect: function (record) {
            //alert('选择一项时触发' + record.id + '|' + record.text);
            $("#userpower").val(record.text); $("#userpower1").val(record.text);
        }
    });

});
