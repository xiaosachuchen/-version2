
$(document).ready(function () {
    //jquery 扩展函数
    var id = $.getparamfromurl("cid");
    $.showvideodetail(id);
    $.showrelatecourse("", "", "", "", 5, 0);
    var vids = "";
    $("a.submit").click(function () {
        $("li.play-detail i").each(function (index, obj) {
            if ($(this).hasClass("active")) {
                //vids = vids +","+ $(this).attr("vid");
                //提交按钮触发，提交订单信息，视频订单
                //数据类型要传
                $.addoneorder(11,$(this).attr("vtitle"));
            }
        })
        //alert(vids);
        //window.location.href = "user-shopcar.html"
    })
});
jQuery.extend({
    //政策分类下拉框
    showvideodetail: function (id) {
        $.ajax({
            url: '/Interface/Course/GetCourseDetail',
            method: 'GET',
            data: { courseid: id },
            success: function (result) {
                if (result != null) {
                    //这里需要替换地方
                    $("#course_basic").tmpl({ data: result.course }).appendTo("#course_basic_hold");
                    $("#course_abstract_info").tmpl({ data: result.course }).appendTo("#course_abstract");
                    $("#video_container_list").tmpl({ data: result.videos }).appendTo("#video_container");
                    $("#teacher_detail_info").tmpl({ data: result.teacher }).appendTo("#teacher_detail");
                }
                else {
                    console.log(id);
                    // 错误提示？
                };
                $(".details-left ul li.title").click(function () {
                    if ($(this).hasClass("active")) {
                        $(this).removeClass("active")
                        $(this).siblings("li.play-detail").hide()
                    } else {
                        $(this).addClass("active")
                        $(this).siblings("li.play-detail").show()
                    }
                });
                $("li.play-detail i").click(function () {
                    if ($(this).hasClass("active")) {
                        $(this).removeClass("active")
                    } else
                    {
                        $(this).addClass("active")
                    }
                    //alert($(this).attr("vid"));
                    //window.location.href = "play.html?vid=" + $(this).attr("vid");
                });
                $("li.play-detail img").click(function () {
                    window.location.href = "play.html?vid=" + $(this).attr("vid");
                });
                $("a.jrsc").click(function () {
                    $.addtofavor(105,id,"eee",11);
                })
            }
        });
    },
    showrelatecourse:function(coursetype,subject,grade,press, pagenum, offset){
        $.ajax({
            url: '/Interface/Course/GetCourses',
            method: 'GET',
            data: { coursetype:coursetype,rows:pagenum,offset:offset, subject: subject,grade:grade,press:press },
            success: function (result)
            {
                if (result != null) {
                    //这里需要替换地方
                    $("#relate_course_list").tmpl({ data: result.courses }).appendTo("#relate_course");
                }
                else {
                    console.log(id);
                    // 错误提示？
                };
            }
        });
    },
    addoneorder:function(m_price,title)
    {
        $.ajax({
            url: '/Interface/UserAccount/InsertOrdersInfo',
            method: 'GET',
            data: { m_price: m_price, title: title },
            success: function (result)
            {
                    if (result.Rcode > 0) {
                        //这里需要替换地方
                        alert("订单添加成功!!!");
                    }
                else {
                    alert("添加失败!!!");
                    // 错误提示？
                };
            }
        });
    },
    addtofavor:function(restype,seqid,resremark,classid)
    {
        $.ajax({
            url: '/Interface/UserAccount/ResourcesFavorites',
            method: 'GET',
            data: { restype: restype, seqid: seqid, resremark: resremark, classid: classid },
            success: function (result)
            {
                if (result.Rcode > 0) {
                    //这里需要替换地方
                    alert("课程收藏成功!!!");
                }
                else {
                    alert("收藏失败!!!");
                    // 错误提示？
                };
            }
        });
    },
    getparamfromurl:function(name){
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
});