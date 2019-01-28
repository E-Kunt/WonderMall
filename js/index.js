$(function () {
    $("#tabs").tabs({
        fit: true,
        border: false,
    });
    obj = {
        loginout: function() {
            $.messager.confirm("提示", '您确定要退出吗', function(flag) {
                if (flag) {
                    $.ajax({
                        type: 'post',
                        url: 'index.aspx?action=loginout',
                        success: function(data) {
                            if (data == "success") {
                                window.location.href = "../Login.aspx";
                            }
                        },
                        eror: function() {
                            alert("退出失败,请重试");
                        }
                    });
                }
            });
        }
    };
    /*以下是栏目链接*/
    $(".link").bind("click", function() {
        var url = $(this).attr("url");
        var icon = $(this).attr("icon");
        var title = $(this).text();
        if ($("#tabs").tabs("exists", title)) {
            $('#tabs').tabs('select', title);
            return;
        } else {
            $('#tabs').tabs('add', {
                iconCls:icon,
                title: title,
                content: showContent(url),
                closable: true,
            });
        }
    });
    function showContent(url) {
        var show = "<iframe src='" + url + "'  scrolling='no' width='100%' height='100%' frameborder='0'></iframe>";
        return show;
    }
});