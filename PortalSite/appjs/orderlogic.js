
//注册逻辑
//先填写基本信息，手机号，获取验证码，密码。 如果调用注册接口的话，下一步就怎么处理。
var total = 0;
var userid = "";
$(document).ready(function () {
    var type = $.getparamfromurl("type");
    var realpay = $.getparamfromurl("realpay");
    $("#realpay").html("实付：" + realpay);
    if ($(".user-msg").attr("otype") == "shopcart") {
        //$.getcurrentuserorders();
        $.getcurrentusershopcart();

    } else if ($(".user-msg").attr("otype") == "orderlist") {
        $.getcurrentuserorders();

    } else if ($(".user-msg").attr("otype") == "myfavor")
    {
        $.getfavorcourselist();

    } else if ($(".user-msg").attr("otype") == "mycourse")
    {
        $.getMycourselist();
    }
    $("#pay_to").click(function () {
        window.location.href = "user-mypay.html?realpay=" + total;
    })

    
});
jQuery.extend({
    //政策分类下拉框
    getcurrentuser:function(){
        $.ajax({
            url: '/Interface/UserAccount/GetCurrentUser',
            method: 'Get',
            success: function (data) {
                if (data.Rcode == 1) {
                    userid = data.Data.UserID;
                }
                else {
                    userid = "";
                }
            }
        })
    },
    getMycourselist: function () {
        $.ajax({
            url: '/Interface/Course/GetCourses',
            method: 'Get',
            data: { coursetype: "", ispage: false, rows: 10 },
            success: function (result) {
                if (result != null) {
                    //处理时间
                    for (var i = 0; i < result.courses.length; i++) {
                        result.courses[i].createdtime = result.courses[i].createdtime.substring(0, 7);
                    }
                    $("#my_course").empty();
                    $("#my_course_list").tmpl({ data: result.courses }).appendTo("#my_course");
                }
                else {
                    console.log("收藏获取失败!!!");
                }
            },
            error: function () {
                console.log('通讯错误');
                return;
            },
        });
    },
    //政策分类下拉框
    getfavorcourselist: function () {
        $.ajax({
            url: '/Interface/UserAccount/GetUserFavor',
            method: 'Get',
            data: { restype:105, rows:10, page:1},
            success: function (result) {
                if (result != null) {
                    for (var i = 0; i < result.Rdata.length; i++) {
                        result.Rdata[i].createdtime = result.Rdata[i].createdtime.substring(0, 7);
                    }
                    $("#my_course").empty();
                    $("#my_course_list").tmpl({ data: result.Rdata }).appendTo("#my_course");
                }
                else {
                    console.log("收藏获取失败!!!");
                }
            },
            error: function () {
                console.log('通讯错误');
                return;
            },
        });
    },
    getcurrentuserorders: function () {
        $.ajax({
            url: '/Interface/UserAccount/GetUserOrdersList',
            method: 'Get',
            data:{timetype:1,page:1,rows:10},
            success: function (data) {
                if (data.Rcode == 1)
                {
                    $("#order_detail_list").tmpl({ data: data.Rdata }).appendTo("#order_list");
                }
                else
                {
                    userid = "";
                }
                //成功获取列表
                $(".oid_delete").click(function () {
                    //删除订单数据啊
                    $.deleteorder($(this).attr("oid"));
                })
            }
        })
    },
    getcurrentusershopcart: function () {
        $.ajax({
            url: '/Interface/UserAccount/GetUserOrdersList',
            method: 'Get',
            data: { timetype: 1, page: 1, rows: 10 },
            success: function (data) {
                
                for (var i = 0; i < data.Rdata.length; i++) {
                    total += data.Rdata[i].s_price;
                }
                if (data.Rcode == 1) {
                    $("#shop_cart_list").tmpl({ data: data.Rdata }).appendTo("#shop_cart");
                }
                else
                {
                    console.log("fail to get list!!!");
                }
                $("#totalmoney").html(total);
                $("#shop_cart li span i").click(function () {
                    if ($(this).hasClass("on")) {
                        //vids = vids +","+ $(this).attr("vid");
                        //提交按钮触发，提交订单信息，视频订单
                        //数据类型要传
                        $(this).removeClass("on");
                        total -= $(this).attr("s_price");
                        alert($(this).attr("oid") + userid);
                    }
                    else {
                        $(this).addClass("on");
                        total += $(this).attr("s_price");
                        $("#totalmoney").html(total);
                    }
                })
            }
        })
    },
    deleteorder: function (oid) {
        $.ajax({
            url: '/Interface/UserAccount/UpdateUserOrdersList',
            method: 'Get',
            data: { orderno:oid },
            success: function (data) {
                if (data.Rcode == 1) {
                    alert("删除成功!!!");
                    window.location.href = "user-payfirst.html";
                }
                else {
                    alert("删除失败!!!");
                }
            }
        })
    },
    getparamfromurl: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
});