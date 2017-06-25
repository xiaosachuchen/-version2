$(document).ready(function () {

    //时间区间
    //时间范围（年月日周）
    $.Datepicker_time();
    $.search();
    var starTime="";
    var endTime="";
    var TimeType=1;
    if (starTime == null || starTime == '') {
        var dateStar = new Date();
        endTime += dateStar.getFullYear() + '-';
        starTime += (dateStar.getFullYear() - 1) + '-';
        starTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
        endTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
        starTime += dateStar.getDate() >= 10 ? dateStar.getDate() : ('0' + dateStar.getDate());
        endTime += dateStar.getDate() >= 10 ? dateStar.getDate() : ('0' + dateStar.getDate());
       
    }
    var arrx = [];
    GetarrX(starTime, endTime, TimeType, arrx);
    DrowEchart(arrx, TimeType, starTime, endTime);
})
jQuery.extend({

    Datepicker_time: function () {
        $('.input-group.date').datepicker({ autoclose: true });
    },
    search: function () {
        var search = $('.row').find('button');
        var starTime;
        var endTime;
        var TimeType;
        search.click(function () {
            starTime = $('input[name=startTime]').val();
            if (starTime == null || starTime == '') {
                var dateStar = new Date();
                starTime += dateStar.getFullYear() + '-';

                starTime += (dateStar.getMonth() + 1) >= 10 ? (dateStar.getMonth() + 1) : ('0' + (dateStar.getMonth() + 1)) + '-';
                starTime += dateStar.getDate() >= 10 ? dateStar.getDate() : ('0' + dateStar.getDate());
                endTime = starTime;
            } else {
                endTime = $('input[name=endTime]').val();
            }
          
            TimeType = $('#TimeType').val();
            var arrx = [];
            GetarrX(starTime,endTime,TimeType,arrx);
            DrowEchart(arrx,TimeType, starTime, endTime);
        });

    }
})