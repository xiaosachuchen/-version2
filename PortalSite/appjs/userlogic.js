var service = null;
var regtype = 0;
var wait = 60;
var t = 0;
var countdown = 60;
//放到外面
function settime(obj) {
    if (countdown == 0)
    {
        obj.removeAttribute("disabled");
        obj.value = "获取验证码";
        countdown = 60;
        return;
    } else {
        obj.setAttribute("disabled", true);
        obj.value = "重新发送(" + countdown + ")";
        countdown--;
    }
    setTimeout(function () {
        settime(obj)
    },1000)
}
//注册逻辑
//先填写基本信息，手机号，获取验证码，密码。 如果调用注册接口的话，下一步就怎么处理。
//
$(document).ready(function () {
    var type = $.getparamfromurl("type");
    if (type == "logout")
    {
        $.userlogout();
    }
    //jquery 扩展函数
    $("#register").click(function () {
        $.regUser();
    })
    $("#send_code").click(function () {
        $.SendYzm();
    })
    $(".login-loginbtn").click(function () {
        $.loginuser($(".login-username input").eq(0).val(), $(".login-save input").eq(0).val(), "");
    })
    $("#submit_info").click(function () {
        if ($.getparamfromurl("userid") != "")
        {
            $.submituserinfo($.getparamfromurl("userid"), $.getparamfromurl("phone"));
        }
        
    })
});
jQuery.extend({
    //政策分类下拉框
    regUser: function () {
        $.ajax({
            url: '/Interface/UserAccount/UserReg',
            method: 'Post',
            data: { phone: $("#phone").val(), yzmcode: $("#yzmcode").val(), passwd: $("#passwd").val() },
            success: function (data) {
                if (data.Rcode == 1) {
                    window.location.href = "/register2.html?userid=" + data.Rdata +"&phone=" + $("#phone").val();
                }
                else {
                    window.location.href = "/register2.html?userid=" + 1;
                    alert(data.Rmsg);
                }
            },
            error: function () {
                console.log('通讯错误');
                return;
            },
        });
    },
    SendYzm: function () {
        if (regtype == 0) {
            var phone = $("#phone").val();
            if (/^0?(13|14|15|18)[0-9]{9}$/.test(phone) == false) {
                $.webalert({
                    title: "",
                    text: "请正确填写手机号码",
                    timer: 2000,
                    type: 'error'
                });
                return;
            }
            $.ajax({
                url: '/Interface/UserValidate/SendYzmSms',
                method: 'Get',
                data: { 'mobile': phone },
                success: function (data) {
                    if (data.Rcode == 1) {
                        $.webalert({
                            title: "",
                            text: '短信验证码已下发，请查看手机并填写验证码',
                            timer: 2000,
                            type: 'success'
                        });
                    }
                    else {
                        $.webalert({
                            title: "",
                            text: data.Rmsg,
                            timer: 3000,
                            type: 'error'
                        });
                    }
                },
                error: function () {
                    console.log('通讯错误');
                    return;
                },
            });
        } else {
            var email = $("#email").val();
            if (/^\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}$/.test(email) == false) {
                $.webalert({
                    title: "",
                    text: "请正确填写邮箱地址",
                    timer: 2000,
                    type: 'error'
                });
                return;
            }
            $.ajax({
                url: '/Interface/UserValidate/SendYzmEmail',
                method: 'Get',
                data: { 'email': email },
                success: function (data) {
                    if (data.Rcode == 1) {
                        $.webalert({
                            title: "",
                            text: '验证码邮件已发送，请查收邮箱并填写验证码',
                            timer: 2000,
                            type: 'success'
                        });
                    }
                    else {
                        $.webalert({
                            title: "",
                            text: data.Rmsg,
                            timer: 3000,
                            type: 'error'
                        });
                    }
                },
                error: function () {
                    console.log('通讯错误');
                    return;
                },
            });
        }
    },
    loginuser:function(username,password,code){
        $.ajax({
            url: '/Interface/UserAccount/UserLogin',
            method: 'Post',
            data: { UserName: username, Password: password, VerificationCode:code},
            success: function (data) {
                if (data.Rcode == 1) {
                    window.location.href = "/index.html?userid=" + data.Rdata;
                }
                else {
                    alert(data.Rmsg);
                }
            },
            error: function () {
                console.log('通讯错误');
                return;
            },
        });
    },
    submituserinfo: function (userid,phone) {
        $.ajax({
            url: '/Interface/UserAccount/PutUserInforschoolinfo',
            method: 'Get',
            data: { updateuserid: userid, name: $("#username").val(), realname: $("#realname").val(), email: "", phone: phone, opinion: 1, period: 1, grade: 1, classname: $("#classname").val(), school: $("#school").val() },
            success: function (data) {
                if (data.Rcode == 1) {
                    window.location.href = "/index.html?userid=" + data.Rdata;
                }
                else {
                    alert(data.Rmsg);
                }
            },
            error: function () {
                console.log('通讯错误');
                return;
            },
        });
    },
    userlogout: function () {
        $.ajax({
            url: '/Interface/UserAccount/SetUserLogout',
            method: 'Get',
            success: function (data) {
                if (data.Rcode == 1) {
                    console.log("退出成功!!!");
                }
            },
            error: function () {
                console.log('通讯错误!!!');
                return;
            },
        });
    },
    getparamfromurl: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
});