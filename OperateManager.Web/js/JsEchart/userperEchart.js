$(document).ready(function () {
    DrowEchart();
    UserPerRate();
})
function DrowEchart() {
    //基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('liter_echart'));
    var Option = {
        title: {
            text: '个人用户数量柱状图',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',       // 标题边框颜色
            borderWidth: 0,            // 标题边框线宽，单位px，默认为0（无边框）
            padding: 5,                // 标题内边距，单位px，默认各方向内边距为5，
            //// 接受数组分别设定上右下左边距，同css
            itemGap: 10,               // 主副标题纵向间隔，单位px，默认为10，
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
        xAxis: [{
            name: '用户类型',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'category',
            //axisLabel:{
            //    textStyle:{
            //        color:"black", //刻度颜色
            //        fontSize:16  //刻度大小
            //    }
            //},
            data: ['总用户','新注册用户','活跃用户']
        }],
        yAxis: {
            name: '人数',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'value',

            //textStyle: {
            //    color: "black", //刻度颜色
            //    fontSize: 16  //刻度大小
            //}

        },
        series: [{
            name: '',
            type: 'bar',
            barWidth: '70%',
            data: []
        }]

    };
    var arrx = [];
    arrx = Option.xAxis[0].data;
    var seriesData = [];
    $.ajax({
        url: '/RIPSP/Base/usersment',
        method: 'GET',
        data: { offset: 1, limit: 12, starttime: $("input[name=starttime]").val(), endtime: $("input[name=endtime]").val() },
        async: false,
        success: function (result) {
                seriesData[0] = result.rows[0].UsersCount;
                seriesData[1] = result.rows[0].UsersNewCount;
                seriesData[2] = result.rows[0].UsersActCount
        }

    });
  
    Option.series[0].data = seriesData;
    myChart.setOption(Option);
    myChart.hideLoading();
}
function UserPerRate()
{
    //基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('ratelist_echart'));
    var Option = {
        title: {
            text: '个人用户比例柱状图',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',       // 标题边框颜色
            borderWidth: 0,            // 标题边框线宽，单位px，默认为0（无边框）
            padding: 5,                // 标题内边距，单位px，默认各方向内边距为5，
            //// 接受数组分别设定上右下左边距，同css
            itemGap: 10,               // 主副标题纵向间隔，单位px，默认为10，
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
        xAxis: [{
            name: '用户类型',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'category',
            //axisLabel:{
            //    textStyle:{
            //        color:"black", //刻度颜色
            //        fontSize:16  //刻度大小
            //    }
            //},
            data: ['用户流失率', '用户留存率', '重复购买率']
        }],
        yAxis: {
            name: '百分百',
            backgroundColor: 'rgba(0,0,0,0)',
            borderColor: '#ccc',
            type: 'value',

            //textStyle: {
            //    color: "black", //刻度颜色
            //    fontSize: 16  //刻度大小
            //}

        },
        series: [{
            name: '',
            type: 'bar',
            barWidth: '70%',
            data: []
        }]

    };
    var arrx = [];
    arrx = Option.xAxis[0].data;
    var seriesData = [];
    $.ajax({
        url: '/RIPSP/Base/UserSource',
        method: 'GET',
        data: { starttime: $("input[name=starttimebar]").val(), endtime: $("input[name=endtimebar]").val() },
        async: false,
        success: function (result) {
            seriesData[0] = result.rows[0].UsersLoss;
            seriesData[1] = result.rows[0].UsersRete;
        }

    });

    Option.series[0].data = seriesData;
    myChart.setOption(Option);
    myChart.hideLoading();
}
