$(document).ready(function () {
    $.Getrestype();
    $("#classname").comboxtree({
        url: "/RIPSP/Base/Options/GetClassTreeOptionsByParent",
        data: { 'parentID': -1, 'WithNone': true }
    });
    $.Datepicker_time();
    $.search();
    var starTime = "";
    var endTime = "";
    var classID = 18;
    if (starTime == null || starTime == '') {
        var dateStar = new Date();
        endTime += dateStar.getFullYear() + '-';
        starTime += (dateStar.getFullYear() - 1) + '-';
        starTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
        endTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
        starTime += dateStar.getDate() >= 10 ? dateStar.getDate() : ('0' + dateStar.getDate());
        endTime += (dateStar.getDate()+1) >= 10 ? (dateStar.getDate()+1) : ('0' + dateStar.getDate()+1);

    }
    DrowEchart(classID,90, starTime, endTime);
})

jQuery.extend({
    Datepicker_time: function () {
        $('.input-group.date').datepicker({ autoclose: true });
    },
    search: function () {
        var search = $('#search_btn');
        var starTime;
        var endTime;
        var classID;
        var restype;
        search.click(function () {
            classID = $('#classname').val();
         
            starTime = $('input[name=startTime]').val();
            if (starTime == null || starTime == '' || classID == "" || classID==null) {
                var dateStar = new Date();
                endTime += dateStar.getFullYear() + '-';
                starTime += (dateStar.getFullYear() - 1) + '-';
                starTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
                endTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
                starTime += dateStar.getDate() >= 10 ? dateStar.getDate() : ('0' + dateStar.getDate());
                endTime += (dateStar.getDate() + 1) >= 10 ? (dateStar.getDate() + 1) : ('0' + dateStar.getDate() + 1);
                classID = 12;
            } else {
                endTime = $('input[name=endTime]').val();
            }
            restype = $('#restype').val();
            DrowEchart(classID,restype, starTime, endTime);
        })
    },
    //资源类型
    Getrestype: function () {
        var html="";
        $.ajax({
            url: '/RIPSP/Base/GetResTypeList',
            method: 'GET',
            success: function (result) {
                if (result.Rcode == 1) {

                    //html += '<option value="0">-请选择-</option>';
                    $.each(result.Rdata, function (i, item) {
                        html += '<option value="' + item.databaseid + '">' + item.databasecname + '</option>'
                    });
                    $('#restype').append(html);
                }
                else {
                    console.log(result.Rmsg);
                }
            }
        });
    }
});

//function getQueryString(name) {
//    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
//    var r = window.location.search.substr(1).match(reg);
//    if (r != null) return (r[2]);
//    return null;
//}
