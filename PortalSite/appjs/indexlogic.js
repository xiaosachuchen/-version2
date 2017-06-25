//var a =getCookie("CIRSUser")
$(document).ready(function() {
    //jquery 扩展函数
    $.showvideos("comment", 4);
    $.showvideos("indenpend", 10);
    $.showvideos("localcity", 8);
    $.showvideos("school", 10);
    $.showvideos("hotactivity", 8);
    $.showvideos("online", 10);
    $.showvideos("talent", 5);
    $.showitemcontent("abc",3);
    $.showcourseDiclist("grade");
    $.showcourseDiclist("subject")
    $.showteacherlist();
    $(function () {
        $("#btn_search").click(function () {
            $(this).attr("href", "classlist.html?subject=key&keyword=" + $("#search_keywords").val());
        })
    })
});
jQuery.extend({
    //政策分类下拉框
    showvideos: function(type,pagenum) {
        $.ajax({
            url: '/Interface/Video/GetVideos',
            method: 'GET',
            data: { videotype: type, ispage: false, rows: pagenum, offset: 1 },
            success: function(result) {
                //需要替换
                if (result != null) {
                    if (type == "comment") {
                        $("#video_comment_list").tmpl({ data: result.courses }).appendTo("#zxpjid");
                    }
                    else if (type == "indenpend") {
                        $("#independ_video_list").tmpl({ data: result.courses }).appendTo("#independ_video");

                    } else if (type == "localcity") {
                        $("#localcity_video_list").tmpl({ data: result.courses }).appendTo("#localcity");

                    } else if (type == "school") {
                        $("#school_video_list").tmpl({ data: result.courses }).appendTo("#school");

                    } else if (type == "hotactivity") {
                        $("#hotactivity_video_list").tmpl({ data: result.courses }).appendTo("#hotactivity");

                    } else if (type == "online") {
                        $("#online_video_list").tmpl({ data: result.courses }).appendTo("#online");

                    } else {
                        $("#talent_video_list").tmpl({ data: result.courses }).appendTo("#talent");
                    }
                } 
                else {
                    console.log("");
                    // 错误提示？
                }
                $("#hotactivity a").hover(function () {
                    $(this).find("ul").css("display", "block");
                }, function () {
                    $("#hotactivity a ul").css("display", "none");
                })
                $("#zxpjid a").hover(function () {
                    $(this).find("ul").css("display", "block");
                }, function () {
                    $("#zxpjid a ul").css("display", "none");
                })
                $("#localcity a").hover(function () {
                    $(this).find("ul").css("display", "block");
                }, function () {
                    $("#localcity a ul").css("display", "none");
                })
                $("#online a").hover(function () {
                    $(this).find("i").css("display", "block");
                    $(this).find("ol").css("display", "none");
                }, function () {
                    $(this).find("i").css("display", "none");
                    $(this).find("ol").css("display", "block");
                })
                $("#talent a").hover(function () {
                    $(this).find("ol").css("display", "block");
                }, function () {
                    $("#talent a ol").css("display", "none");
                })
            }
        });
    },
    showcourseDiclist: function(type) {
        $.ajax({
            url: '/Interface/Course/GetCoursesDic',
            method: 'GET',
            data: { dictype: type, ispage: false, rows: 100, offset: 1 },
            success: function(result) {
                if (result != null) {
                    if (type == "subject") {
                        $("#subject_list").tmpl({ data: result.subjects }).appendTo(".subject_dic");
                    } else {
                        $("#grade_list").tmpl({ data: result.grades }).appendTo(".grade_dic");
                    }
                }
                else {
                    console.log("");
                    // 错误提示？
                }
            }
        });
    },
    showitemcontent: function () {
        $.ajax({
            url: '/Interface/Base/GetItemContents',
            method: 'GET',
            data: { itemmark: "study", ispage: false, rows: 5, offset: 1 },
            success: function (result) {
                if (result != null) {
                    //$("#lunbo_list").tmpl({ data: result.Rdata }).appendTo("#lunbo_span");
                    $("#lunbo_span img").each(function (index, obj) {
                        //console.log($(this).attr("src"));
                        $(this).attr("src", result.Rdata[index].thumbnail)
                    })
                }
                else {
                    console.log("");
                    // 错误提示？
                }
            }
        });
    },
    showcoursedetail: function() {

        $.ajax({
            url: '/Interface/Course/GetCourseDetail',
            method: 'GET',
            data: { courseid:1 },
            success: function(result) {
                if (result != null) {
                    //这里需要替换地方
                    $("#class_list").tmpl({ data: result }).appendTo(".coursedic");
                }
                else {
                    console.log("");
                    // 错误提示？
                }
            }
        });
    },
    
    showteacherlist: function() {

        $.ajax({
            url: '/Interface/Teacher/GetTeachers',
            method: 'GET',
            data: { teachertype: "一年级", ispage: false, rows: 100, offset: 1 },
            success: function(result) {
                //需要替换
                if (result != null) {
                    $("#teacher_list").tmpl({ data: result.data }).appendTo("#teach_p");
                }
                else {
                    console.log("");
                    // 错误提示？
                }
            }
        });
    },
    showvideodetail: function() {

          $.ajax({
              url: '/Interface/Video/GetVideoDetail',
              method: 'GET',
              data: { videoid: 1 },
              success: function(result) {
                  if (result != null) {
                      //这里需要替换地方
                      $("#class_list").tmpl({ data: result }).appendTo(".coursedic");
                  }
                  else {
                      console.log("");
                      // 错误提示？
                  }
              }
          });
      }
});