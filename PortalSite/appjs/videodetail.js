
$(document).ready(function () {
    //jquery 扩展函数
    var id = $.getparamfromurl("vid");
    $.showvideodetail(id);
    //$.showrelatecourse("", "", "", "", 5, 0);
});
jQuery.extend({
    //政策分类下拉框
    showvideodetail: function (id) {
        $.ajax({
            url: '/Interface/Video/GetVideoDetail',
            method: 'GET',
            data: { videoid: id },
            success: function (result) {
                if (result != null) {
                    //这里需要替换地方
                    $("#video_detail_html").tmpl({ data: result.video }).appendTo("#video_detail");
                    $("#video_abstract_html").tmpl({ data: result.video }).appendTo("#video_abstract");
                    $("#video_teacher_html").tmpl({ data: result.teacher }).appendTo("#video_teacher");
                }
                else {
                    console.log(id);
                    // 错误提示？             
                };
                $("a.bf").click(function () {
                    $("dl.main").hide()

                    var DEFAULT_VERSION = "8.0";
                    var ua = navigator.userAgent.toLowerCase();
                    var isIE = ua.indexOf("msie") > -1;
                    var safariVersion;
                    if (isIE) {
                        safariVersion = ua.match(/msie ([\d.]+)/)[1];
                        if (safariVersion <= DEFAULT_VERSION) {
                            $("embed").show()
                            //alert(1)
                        } else {
                            $("video").show()
                            //alert(0)
                        }
                    } else {
                        $("video").show()
                    }
                })
                $("ul li p a").click(function () {
                    $(this).siblings("span").show()
                })
            }
        });
    },
    showrelatecourse: function (coursetype, subject, grade, press, pagenum, offset) {
        $.ajax({
            url: '/Interface/Course/GetCourses',
            method: 'GET',
            data: { coursetype: coursetype, rows: pagenum, offset: offset, subject: subject, grade: grade, press: press },
            success: function (result) {
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
    getparamfromurl: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
});