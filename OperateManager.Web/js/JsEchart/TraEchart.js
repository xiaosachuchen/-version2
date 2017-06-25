$(document).ready(function () {

    //时间区间
    //时间范围（年月日周）
    //
   
})
function DrowEchart(arrx,timetype,starDate,endDate) {
  
    //基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('liter_echart'));
    var restype;
    var years;
    var rows = 8;
    var setarr = new Array();
    //myChart.showLoading({
    //    text: "正在加载中...."
    //});
    //指定图表的配置项和数据
    var Option = {
        title: {
            text: '交易管理趋势图',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',       // 标题边框颜色
            borderWidth: 0,            // 标题边框线宽，单位px，默认为0（无边框）
            padding: 5,                // 标题内边距，单位px，默认各方向内边距为5，
           
            itemGap: 10,               // 主副标题纵向间隔，单位px，默认为10，
           
        },
        tooltip: {
            show: true
        },
        legend: {
            type: 'category',
            data: ['个人','机构']
        },
        grid: {
            top: '20%',
            left: '10%',
            right: '20%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [{
            name: '时间',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'category',
            data: []
        }],
        yAxis: {
            name: '金额',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'value',

        },
        series: [{
            name: '个人',
            type: 'line',
            //barWidth: '40%',
            data: []
        }, {
            name: '机构',
            type: 'line',
            data:[]
        }]

    };
   
    Option.xAxis[0].data = arrx;
   // Option.legend.data = arrx;
    var seriesData = [];
    $.ajax({
        url: '/RIPSP/Base/orderinfo',
        method: 'GET',
        data: { timetype: timetype, starDate: starDate, endDate: endDate },
        async: false,
        success: function (result) {
            if (result.Rcode == 1) {
                for (var j = 0; j < arrx.length; j++) {
                    for (var i = 0; i < result.Rdata.length; i++) {
                        if (arrx[j] == result.Rdata[i].times) {
                            seriesData[j] = result.Rdata[i].nums

                            break;
                        }
                        else {
                            seriesData[j] = 0;
                        }
                    }
                }

            }
            else {
                console.log(result.Rmsg);
            }
        }

    });
    var seriesDataORG = [];
    $.ajax({
        url: '/RIPSP/Base/orderinfo',
        method: 'GET',
        data: { timetype: timetype, starDate: starDate, endDate: endDate, i:1 },
        async: false,
        success: function (result) {
            if (result.Rcode == 1) {
                for (var j = 0; j < arrx.length; j++) {
                    for (var i = 0; i < result.Rdata.length; i++) {
                        if (arrx[j] == result.Rdata[i].times) {
                            seriesDataORG[j] = result.Rdata[i].nums

                            break;
                        }
                        else {
                            seriesDataORG[j] = 0;
                        }
                    }
                }

            }
            else {
                console.log(result.Rmsg);
            }
        }

    });
    Option.series[0].data = seriesData;
    Option.series[1].data = seriesDataORG;
    //alert(Option.series);
    //myChart.setSeries(seriesData);
    myChart.setOption(Option);
    myChart.hideLoading();
}
function GetarrX(starTime, endTime, TimeType,axx) {
    //var axx = [];
    var starDate = new Date(starTime);
    var endDate = new Date(endTime);
    var DateType = TimeType;
    var dateDel=0;
    //天
    if (DateType == 3) {
        getAll(starTime, endTime, axx);
     
    } else if (DateType == 2) {
        //月
        var year;
        var month;
      
        var yearToMonth = (endDate.getFullYear() - starDate.getFullYear()) * 12;
        dateDel+=yearToMonth;
        var monthToMonth = endDate.getMonth() - starDate.getMonth();
        dateDel += monthToMonth;
        year = starDate.getFullYear();
        month = starDate.getMonth();
        dateDel = parseInt(Math.ceil(dateDel));
         for (var i = 0; i <= dateDel; i++) {
            month += 1;
            if (month > 12) {
                month -= 12;
                year += 1;
            }
            yearandmonth = year + '-' + month;
            axx.push(yearandmonth);
           
        }
    } else if (DateType == 4) {
        //周
        //dateDel = (endDate - starDate) / (1000 * 3600 * 24 * 7);
        //dateDel = parseInt(Math.ceil(dateDel));
        getWeekAll(starDate, endDate, axx);
       
    }else if(DateType == 1){
        dateDel = endDate.getFullYear() - starDate.getFullYear();
        dateDel = parseInt(Math.ceil(dateDel));
      
        for (var i = 0; i <= dateDel; i++) {
           
            axx.push(starDate.getFullYear() + i);
        }
      
    }
}
Date.prototype.format = function (axx) {
    var s = '';
    var mouth = (this.getMonth() + 1) >= 10 ? (this.getMonth() + 1) : ('0' + (this.getMonth() + 1));
    var day = this.getDate() >= 10 ? this.getDate() : ('0' + this.getDate());
    s += this.getFullYear() + '-'; // 获取年份。  
    s += mouth + "-"; // 获取月份。  
    s += day; // 获取日。  
    axx.push(s);
};
Date.prototype.formatWeek = function (axx) {
    var s = '';
    var a=0;
    var mouth = (this.getMonth() + 1) >= 10 ? (this.getMonth() + 1) : ('0' + (this.getMonth() + 1));
    var day = this.getDate() >= 10 ? this.getDate() : ('0' + this.getDate());
    s += this.getFullYear() + '-'; // 获取年份。  
    s += mouth + "-"; // 获取月份。  
    s += day; // 获取日。
    //if (s == begin) {
    //    a = new Date(s);
    //    a = a.getDay() - 1;
    //    axx.push(s);
    //} else {
       
    //}
    axx.push(s);
  
};
function getAll(begin, end,axx) {
    var ab = begin.split("-");
    var ae = end.split("-");
    var db = new Date();
    db.setUTCFullYear(ab[0], ab[1] - 1, ab[2]);
    var de = new Date();
    de.setUTCFullYear(ae[0], ae[1] - 1, ae[2]);
    var unixDb = db.getTime();
    var unixDe = de.getTime();
    for (var k = unixDb; k <= unixDe;) {
        console.log((new Date(parseInt(k))).format(axx));
        k = k + 24 * 60 * 60 * 1000;
    }
}
function getWeekAll(begin, end, axx) {
    var ab = begin.split("-");
    var ae = end.split("-");
    var db = new Date();
    db.setUTCFullYear(ab[0], ab[1] - 1, ab[2]);
    var de = new Date();
    de.setUTCFullYear(ae[0], ae[1] - 1, ae[2]);
    var unixDb = db.getTime();
    var unixDe = de.getTime();
    for (var k = unixDb; k <= unixDe;) {
        console.log((new Date(parseInt(k))).formatWeek(axx));
        k = k + 24 * 60 * 60 * 1000 * 7;
    }
}