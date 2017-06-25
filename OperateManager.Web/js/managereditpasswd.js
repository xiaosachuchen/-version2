$(document).ready(function () {
  
    $("#formsubmit").on('click', function () {
        var passwd = $("form").find("input[name=passwd]").val();
        var newpasswd = $("form").find("input[name=newpasswd]").val();
        var confirmpasswd = $("form").find("input[name=confirmpasswd]").val();
        if (passwd.length < 1) {
            $.webalert({
                text: '请输入原密码',
                timer: 5000,
                type: 'info'
            });
            return;
        }
        if (newpasswd.length < 6) {
            $.webalert({
                text: '请输入新密码,新密码长度至少要6位',
                timer: 5000,
                type: 'info'
            });
            return;
        }
        if (newpasswd != confirmpasswd) {
            $.webalert({
                text: '两次密码输入不一致，请核对',
                timer: 5000,
                type: 'info'
            });
            return;
        }
        $.ajax({
            url: '/RIPSP/Base/Account/PostManagerPsswd',
            data: $("form").serializeArray(),
            beforeSend: function () {
                var isValidate = true;
                return isValidate;
            },
            method: 'Post',
            success: function (result) {
                if (result.Rcode == 1) {
                    $.webalert({
                        title: "",
                        text: "操作成功",
                        timer: 2000,
                        type: 'success'
                    });                   
                }
                else {
                    $.webalert({
                        text: result.Rmsg,
                        timer: 5000,
                        type: 'error'
                    });
                }
            }
        });
    })
});
