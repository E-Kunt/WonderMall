$(function () {
    $("#prodouct").datagrid({
        url: "ProdouctList.aspx?action=bindProdouct",
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
            field: 'pImagePath',
            title: '产品图片展示',
            width: 100,
        },
        {
            field: 'pTitle',
            title: '产品标题',
            width: 100,
            sortable: true,
        },
         
        {
            field: 'pPrice',
            title: '产品价格',
            width: 100,
            sortable: true,
        },
        {
            field: 'pType',
            title: '产品分类',
            width: 100,
            sortable: true,
        },
         {
             field: 'pCount',
             title: '产品数量',
             width: 100,
             sortable: true,
         },
          {
              field: 'pDate',
              title: '产品上传时间',
              width: 100,
              sortable: true,
          },
        ]],
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 15, 20],
        pageNumber: 1,
        sortName: 'pDate',
        sortOrder: "desc",
        toolbar: "#prodouct_tool",
    });
    prodouct_tool = {
        row: undefined,
        add: function () {
            $('#prodouct_add').dialog('open'); $('input[name="pTitle"]').focus();
        },
        edit: function () {
            var rows = $("#prodouct").datagrid("getSelections");
            if (rows.length > 1 || rows.length <= 0) {
                $.messager.alert("警告操作", "编辑有且只有一条记录进行编辑", 'warning');
            } else {
                $("#prodouct_edit").dialog('open');
                $('input[name="pTitle1"]').focus();
                this.row = rows[0];
                var ID = this.row.ID;
                /*跳转到后台绑定到编辑页面*/
                $.ajax({
                    url: 'ProdouctList.aspx?action=edit',
                    data: { ID: ID },
                    type: 'post',
                    success: function (data) {
                        var datas = data.split(",");

                        $("input[name=pTitle1]").val(datas[0]);
                        $("input[name=pPrice1]").val(datas[1]);
                        $("input[name=pCount1]").val(datas[3]);

                        $("#pType1").val(datas[2]);
                        $("#pContent1").html(datas[4]);
                        $('.type').combobox('select', datas[2]);

                    },
                    error: function () {
                        $.messager.alert("不存在该产品");
                        return;
                    }
                });
            }
        },
        delete1: function () {
            var rows = $("#prodouct").datagrid("getSelections");
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
                            url: "ProdouctList.aspx?action=deleteProdouct",
                            type: "post",
                            data: { ids: idss },
                            success: function (data) {
                                if (data == "success") {

                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除' + ids.length + '个用户成功',
                                    });
                                    $("#prodouct_edit").dialog('close').form("reset");
                                    $("#prodouct").datagrid("reload");
                                } else {
                                    $.messager.show({
                                        title: '提示',
                                        msg: '商品还有库存，不能随便删除！',
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
            $("#prodouct").datagrid("rejectChanges");
        }
    };
    /*以下是编辑窗口的设计*/
    $("#prodouct_edit").dialog({
        top:0,
        width: 500,
        title: "编辑产品",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-edit',
            handler: function () {
                if (($("#prodouct_edit").form("validate"))) {
                    $.ajax({
                        url: 'ProdouctList.aspx?action=editProdouct',
                        type: 'post',
                        data: {
                            ID: prodouct_tool.row.ID,
                            pTitle: $("input[name=pTitle1]").val(),
                            pPrice: $("input[name=pPrice1]").val(),
                            pType: $("#pType1").val(),
                            pCount: $("input[name=pCount1]").val(),
                            pContent: $("#pContent1").val(),
                        },
                        success: function (data) {
                          
                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '编辑产品成功',
                                });
                                $("#prodouct_edit").dialog('close').form("reset");
                                $("#prodouct").datagrid("reload");
                            } else {
                                $.messager.alert('警告操作', '产品已存在（或存在其他未知错误）！', 'warning');
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
                $('#prodouct_edit').dialog('close').form('reset');

            }
        }]
    });
    /*以下是对添加管理员对话框的设置 以及事件*/
    $("#prodouct_add").dialog({
        top: 0,
        width: 500,
        title: "添加新产品",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-new',
            handler: function () {
                if ($("#prodouct_add").form("validate")) {
                    $.ajax({
                        url: 'ProdouctList.aspx?action=addProdouct',
                        type: 'post',
                        data: {
                            pTitle: $("input[name=pTitle]").val(),
                            pPrice: $("input[name=pPrice]").val(),
                            pType: $("#pType").val(),
                            pCount: $("input[name=pCount]").val(),
                            pContent: $("#pContent").val(),
                        },
                        success: function (data) {
                            
                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '新增产品成功',
                                });
                                $("#prodouct_add").dialog('close').form("reset");
                                $("#prodouct").datagrid("reload");
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
                    $('#prodouct_add').dialog('close').form('reset');
                }
            }]
    });
    $("input[name=pTitle],input[name=pPrice],input[name=pCount],input[name=pContent],input[name=pTitle1],input[name=pPrice1],input[name=pCount1],input[name=pContent1]").validatebox({
        required: true,
        validType: 'length[1,50]',
        missingMessage: '请完善信息',
        invalidMessage: '输入的字符长度有误(1,50)',
    });
    $(".type").combobox({
        width: 200,
        valueField: 'id',
        textField: 'text',
        data: [{
            id: '1',
            text: '装饰摆饰',
        },
            {
                id: '2',
                text: '厨房餐饮',
            },
            {
                id: '3',
                text: '办公文具',
            },
            {
                id: '4',
                text: '玩具娱乐',
            },
            {
                id: '5',
                text: '智能科技',
            }
        ],
        onSelect: function (record) {
            //alert('选择一项时触发' + record.id + '|' + record.text);
            
            //alert('选择一项时触发' + record.id + '|' + record.text);
            $("#pType").val(record.text); $("#pType1").val(record.text);
        }
    });
});

