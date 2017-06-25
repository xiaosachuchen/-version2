$(document).ready(function () {

    //时间区间
    //时间范围（年月日周）
    //DrowEchart();
})
function DrowEchart(classID,restype, starTime, endTime) {

    //基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('liter_echart'));

    var years;
    var rows = 8;
    var setarr = new Array();
    //myChart.showLoading({
    //    text: "正在加载中...."
    //});
    //指定图表的配置项和数据
    var Option = {
        title: {
            text: '资源管理购买量统计图',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',       // 标题边框颜色
            borderWidth: 0,            // 标题边框线宽，单位px，默认为0（无边框）
            padding: 5,                // 标题内边距，单位px，默认各方向内边距为5，
            //// 接受数组分别设定上右下左边距，同css
            itemGap: 10,               // 主副标题纵向间隔，单位px，默认为10，
            //textStyle: {
            //    fontSize: 18,
            //    fontWeight: 'bolder',
            //    color: '#333'          // 主标题文字颜色

            //}
        },
        tooltip: {
            show: true
        },
        legend: {
            type: 'category',
            data: []
        },
        grid: {
            top: '20%',
            left: '10%',
            right: '20%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            name: '购买量',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'value',
            //axisLabel:{
            //    textStyle:{
            //        color:"black", //刻度颜色
            //        fontSize:16  //刻度大小
            //    }
            //},
           
        },
        yAxis: [{
            name: '标题',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'category',
            data: []
            //textStyle: {
            //    color: "black", //刻度颜色
            //    fontSize: 16  //刻度大小
            //}

        }],
        series: [{
            name: '',
            type: 'bar',
            barWidth: '30%',
            data: []
        }]

    };
    var arrx = [];
    var seriesData = [];
    $.ajax({
        url: '/RIPSP/Base/GetInfoY',
        method: 'GET',
        async: false,
        data: { type: 3, restype: restype, classid: classID, starTime: starTime, endTime: endTime },
        success: function (result) {
            if (result.Rcode == 1) {
                for (var i = 0; i < result.Rdata.length; i++) {
                    seriesData.push(result.Rdata[i].num);
              
                    arrx.push(result.Rdata[i].resname);
                }

            } else {
                seriesData.push(0);
            }

        }
    });

    Option.yAxis[0].data = arrx;
    Option.legend.data = arrx;
    Option.series[0].data = seriesData;
    //alert(Option.series);
    //myChart.setSeries(seriesData);
    myChart.setOption(Option);
    myChart.hideLoading();
}
