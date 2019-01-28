$(function () {
    $("#timages").datagrid({
        url: "timagesList.aspx?action=bindOrders",
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
            field: 'oNum',
            title: '订单号',
            width: 100,
        },
        {
            field: 'oProject',
            title: '订单商品',
            width: 100,
            sortable: true,
        },

        {
            field: 'oState',
            title: '订单状态',
            width: 100,
            sortable: true,
        },
        {
            field: 'oUserID',
            title: '订单所属用户',
            width: 100,
            sortable: true,
        },
         {
             field: 'oPrice',
             title: '总价',
             width: 100,
             sortable: true,
         },
          {
              field: 'oDate',
              title: '订单日期',
              width: 100,
              sortable: false,
          },
        ]],
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 15, 20],
        pageNumber: 1,
        sortName: 'ID',
        sortOrder: "desc",
        toolbar: "#timages_tool",
    });
    timages_tool = {
        row: undefined,
        edit: function () {
            var rows = $("#timages").datagrid("getSelections");
            if (rows.length > 1 || rows.length <= 0) {
                $.messager.alert("警告操作", "编辑有且只有一条记录进行编辑", 'warning');
            } else {
                $("#timages_edit").dialog('open');
                //$('input[name="pTitle1"]').focus();
                this.row = rows[0];
                var ID = this.row.ID;
                /*跳转到后台绑定到编辑页面*/
                $.ajax({
                    url: 'timagesList.aspx?action=edit',
                    data: { ID: ID },
                    type: 'post',
                    success: function (data) {
                        var datas = data.split(";");

                        $("input[name=oNum1]").val(datas[0]);
                        $("input[name=oUserID1]").val(datas[4]);
                        $("input[name=oDate1]").val(datas[2]);

                        $("#oState1").val(datas[3]);
                        $("#oProject1").html(datas[1]);
                        $('.state').combobox('select', datas[3]);
                    },
                    error: function () {
                        $.messager.alert("不存在该产品");
                        return;
                    }
                });
            }
        },
        redo: function () {
            $("#timages").datagrid("rejectChanges");
        }
    };

    $("#timages_edit").dialog({
        top: 0,
        width: 500,
        title: "编辑订单",
        iconCls: 'icon-user-add',
        modal: true,
        closed: true,
        buttons: [{
            text: "提交",
            iconCls: 'icon-add-edit',
            handler: function () {
                if (($("#timages_edit").form("validate"))) {
                    $.ajax({
                        url: 'timagesList.aspx?action=edittimages',
                        type: 'post',
                        data: {
                            ID: timages_tool.row.ID,
                            oState: $("#oState1").val(),
                        },
                        success: function (data) {

                            if (data == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: '编辑产品成功',
                                });
                                $("#timages_edit").dialog('close').form("reset");
                                $("#timages").datagrid("reload");
                            } else {
                                $.messager.alert('警告操作', '产品已存在（或存在其他未知错误）！', 'warning');
                            }
                        },
                        error: function () {
                            $.messager.alert('警告操作', '服务器数据出错！,请稍后重试', 'warning');
                        }
                    });
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-redo',
            handler: function () {
                $('#timages_edit').dialog('close').form('reset');

            }
        }]
    });

    $(".state").combobox({
        width: 200,
        valueField: 'id',
        textField: 'text',
        data: [{
            id: '1',
            text: '未发货',
        },
            {
                id: '2',
                text: '已发货',
            },
            {
                id: '3',
                text: '已收货',
            },
            {
                id: '4',
                text: '已评价',
            }
        ],
        onSelect: function (record) {
            $("#oState").val(record.text); $("#oState1").val(record.text);
        }
    });
});