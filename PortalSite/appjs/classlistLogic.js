
$(document).ready(function () {
    var type = $.getparamfromurl("type");
    var grade = "一年级";
    var subject = "语文";
    var press = "人教版";
    var keywords = "";
    var total = 100;
    var index = 0;
    var pageindex=0;
    var pagesize = 12;
    //页面间传递数据
    if (type == "subject") {
        subject = $.getparamfromurl("keyword");
    } else if (type == "grade") {
        grade = $.getparamfromurl("keyword");
    }
    else
    {
        keywords = $.getparamfromurl("keyword");
        $(".keyword").val(keywords);
    }
    //jquery 扩展函数
    $.showvideos("newest", 4,1,"","","");
    $.showvideos("main", pagesize,1,subject,grade,press);
    //$.showvideos("mainbottom", 6);
    $(function () {
        //设置样式
        $("p.title a").click(function () {
            $(this).addClass("active");
            //根据最新或者点击量 查询课程信息
            if ($(this).html() == "最新") {
                $.showvideos("", 4, 1, "", "", "", "createdtime");
            } else
            {
                $.showvideos("", 4, 1, "", "", "hits");
            }
            $(this).siblings("a").removeClass("active")
        })
        $(".nav-xxb li:not('.jcbb') a").click(function () {
            $(this).addClass("hover")
            $(this).siblings("a").removeClass("hover")
        })
        $(".nav-xxb li.jcbb a:not('.more')").click(function () {
            $(this).addClass("hover")
            $(".nav-xxb li.jcbb a:not('.more')").not($(this)).removeClass("hover")
        })
        $("li.grade a").click(function () {
            $(this).addClass("hover")
            $(this).siblings("a").removeClass("hover")
            grade = $(this).html();
        })
        $("li.subject a").click(function () {
            $(this).addClass("hover")
            $(this).siblings("a").removeClass("hover")
            subject = $(this).html();
        })
        $("li.jcbb a").click(function () {
            $(this).addClass("hover")
            $(this).siblings("a").removeClass("hover")
            press = $(this).html();
        })
        $(".search_btn").click(function () {
            keywords = $(".keyword").val();
            //点击查询按钮 查询数据，传入年级，课程，出版社等信息。
            //alert(grade + "." + subject + "." + press + "." + keywords);
            //先清空列表
            $("#video_main_list").empty();
            $.showvideos("main", pagesize, 1, subject, grade, press);
        })
        $(".dpages").click(function () {
            pageindex = $(".dpages").eq(index).html(); //获取指向数据库的页码
            $("#video_main_list").empty();
            $(this).addClass("active");
            index = $(this).attr("tt");
            $(this).siblings("a").removeClass("active");
            //$.showvideos("main", pagesize, $(this).html());
            $("#video_main_list").empty();
            $.showvideos("main", pagesize, pageindex, subject, grade, press);
        })
        $(".firstpage").click(function () {
            //alert($(".dpages").eq(0).html());
            var start = 1;
            $(".dpages").eq(0).addClass("active");
            index = 0;
            $(".dpages").eq(0).siblings("a").removeClass("active");
            $(".dpages").each(function (index, obj) {
                  $(this).html(start++);
            })
            $("#video_main_list").empty();
            $.showvideos("", pagesize, 0, subject, grade, press);
        })
        $(".next").click(function () {
            pageindex = $(".dpages").eq(index).html(); //获取指向数据库的页码
            if ($(".dpages").eq(7).hasClass("active")) {
                var startindex = pageindex;
                var total = $("#pages").attr("totalnum");
                index = $("#pages").attr("pageindex");
                var pagecount = parseInt(total / pagesize);
                if (pagecount > $(".dpages").eq(7).html()) {
                    $(".dpages").each(function (index, obj) {
                        $(this).html(startindex++);
                    })
                    index = 0;
                    $(".dpages").eq(0).addClass("active");
                    $(".dpages").eq(0).siblings("a").removeClass("active");
                }
            } else
            {
                var next = ++index;
                $(".dpages").eq(next).addClass("active");
                $(".dpages").eq(next).siblings("a").removeClass("active");
            }
            $("#video_main_list").empty();
            $.showvideos("", pagesize, pageindex, subject, grade, press);
        })
        $(".pre").click(function () {
            pageindex = $(".dpages").eq(index).html(); //获取指向数据库的页码
            if ($(".dpages").eq(0).hasClass("active")) {
                if (pageindex > 7) {
                    var startindex = pageindex - 7;
                    $(".dpages").each(function (index, obj) {
                        $(this).html(startindex++);
                    })
                }
            } else
            {
                var pre = --index  //指针向前
                $(".dpages").eq(pre).addClass("active");
                $(".dpages").eq(pre).siblings("a").removeClass("active");
            }
            $("#video_main_list").empty();
            $.showvideos("", pagesize, pageindex, subject, grade, press);
        })
        $(".endpage").click(function () {
            //alert($(".dpages").eq(7).html());
            $(".dpages").eq(7).addClass("active");
            $(".dpages").eq(7).siblings("a").removeClass("active");
            var total = $("#pages").attr("totalnum");
            var index = $("#pages").attr("pageindex");
            var pagecount = parseInt(total/pagesize);
            var start = pagecount - 7;
            $(".dpages").each(function (index, obj) {
                $(this).html(start++);
            })
            $("#video_main_list").empty();
            $.showvideos("",pagesize,pagecount-1,subject,grade,press);
        })
        //$(".pre").click(function () {
        //    //index = 7;
        //    alert(index)
        //    $(".dpages").eq(index--).addClass("active");
        //    $(".dpages").eq(index--).siblings("a").removeClass("active");
        //})
    });
});
jQuery.extend({
    //政策分类下拉框
        getparamfromurl:function(name){
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        },
       showvideos: function(type,size,index,subject,grade,press,orderbyname) {
        $.ajax({
            url: '/Interface/Course/GetCourses',
            method: 'GET',
            data: { coursetype: type, ispage: false, rows: size, offset: index, orderby: orderbyname ,subject:subject,grade:grade,press:press},
            success: function(result) {
                //需要替换
                var temp = index;
                if (result != null) {
                    if (type == "newest") {
                        $("#orderby_video_list").tmpl({ data: result.courses }).appendTo("#orderby_video");
                    }
                    else if (type = "main")
                    {
                        $("#video_main_data_list").tmpl({ data: result.courses }).appendTo("#video_main_list");
                        $("#pages").attr("totalnum", result.coursetotal);
                        $("#pages").attr("pageindex", result.pageindex);
                    }
                    else if (type == "mainbottom")
                    {
                        $("#video_mainbottom_data_list").tmpl({ data: result.courses }).appendTo("#mainbottom");
                    }
                }
                else {
                    console.log("");
                    // 错误提示？
                }
                $(".clwrap-topar ol li dl").hover(function () {
                    if ($(this).hasClass("active")) {
                        $(this).removeClass("active")
                        $(this).find("span").css("display","none");
                    } else {
                        $(this).addClass("active")
                        $(this).find("span").css("display","block");
                    }
                });
                $(".clwrap-topar ol li dl").click(function () {
                    window.location.href = "classdetail.html?cid=" + $(this).attr("cid");
                });
                $(".clwrap-topal dl").hover(function () {
                    $(this).addClass("active")
                    $(this).siblings("dl").removeClass("active")
                });
                $(".clwrap-topal dl").click(function () {
                    window.location.href = "classdetail.html?cid=" + $(this).attr("cid");
                });

            }
        });

    }
});