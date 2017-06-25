$(document).ready(function () {
    $(".verificationcode img").on('click', function () {
        $(this).attr('src', '/RIPSP/Base/Account/GetVerificationImg?w=150&h=32&t=' + new Date());
    }).click();
    $("form button").on('click', function () {
        $.ajax({
            url: '/RIPSP/Base/Account/Login',
            data: $("form").serializeArray(),
            beforeSend: function () {
                var isValidate = true;
                return isValidate;
            },
            method: 'Post',
            success: function (result) {
                if (result.Rcode == 1) {
                    //$.webalert({
                    //    title: "",
                    //    text: "登录成功",
                    //    timer: 2000,
                    //    type: 'success',
                    //    fun: function () {
                           document.location.href = 'main.shtml';
                    //    }
                    //});                   
                }
                else {
                    $.webalert({
                        text: result.Rmsg,
                        timer: 5000,
                        type: 'error'
                    });
                    $("form input[name!=loginname]").val("");
                    $(".verificationcode img").click();
                }
            }
        });
    })
});
