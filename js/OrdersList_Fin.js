$(function () {
    $("#orders_Fin").datagrid({
        url: "OrdersList_Fin.aspx?action=bindOrders",
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
        toolbar: "#orders_tool",
    });
    orders_tool_Fin = {
        row: undefined,
        del: function () {
            var rows = $("#orders_Fin").datagrid("getSelections");
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
                            url: "OrdersList_Fin.aspx?action=del",
                            type: "post",
                            data: { ids: idss },
                            success: function (data) {
                                if (data == "success") {

                                    $.messager.show({
                                        title: '提示',
                                        msg: '删除' + ids.length + '个订单成功',
                                    });
                                    $("#orders_Fin").datagrid("reload");
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
            $("#orders_Fin").datagrid("rejectChanges");
        }
    };

});