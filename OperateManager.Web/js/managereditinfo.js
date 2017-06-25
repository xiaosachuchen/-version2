$(document).ready(function () {
    $.ajax({
        url: '/RIPSP/Base/Account/GetManagerInfo',
        method: 'Get',
        success: function (result) {
            if (result.Rcode == 1) {
                $("form").find("input[name=realname]").val(result.Rdata.realname);
                $("form").find("input[name=phone]").val(result.Rdata.phone);
                $("form").find("input[name=email]").val(result.Rdata.email);
            } else {
                $.ShowMessage(result.Rcode, result.Rmsg);
                return;
            }
        }
    });
    $("#formsubmit").on('click', function () {
        $.ajax({
            url: '/RIPSP/Base/Account/PostManagerInfo',
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
