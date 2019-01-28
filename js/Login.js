$(function () {
    //登陆框设置
    $("#login").dialog({            
        title: '登陆后台',
        width: 300,
        height: 180,
        modal: true,
        iconCls: 'icon-login',
        buttons: "#btn",
        closable: false,
    });
    //用户名文本框设置
    $("#manager").validatebox({        
        required: true,
        missingMessage: '请输入管理员账号',
        invalidMessage:'管理员账号不能为空',
    });
    //密码框设置
    $("#password").validatebox({
        required: true,
        validType: 'length[4,15]',
        missingMessage: '请输入管理员密码',
        invalidMessage: '管理员密码在4到15位之间',
    });
    //登陆按钮
    $("#btn a").bind('click', function() {
        if (!$("#manager").validatebox('isValid')) {
            $("#manager").focus();
        } else if (!$("#password").validatebox('isValid')) {
            $("#password").focus();
        } else {
            //此处是进行异步提交给后台进行判断
            $.ajax({
                type: 'post',
                url: 'Login.aspx?action=login',
                data: {
                    username: $("#manager").val(),
                    password: $("#password").val(),
                },
                success: function (data) {
                    if (data == "success") {
                        
                        window.location.href = "Admin/index.aspx";
                    } else {
                        alert("登陆失败");
                    }
                },
                error:function() {
                    alert("登陆失败2");
                }
            });
        }
    });
})