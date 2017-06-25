$(document).ready(function () {
    $.FormInit();
    $.Table_Init();
    $.Datepicker_time();
    $.UserOrgCount();
})
jQuery.extend({
    FormInit: function (data) {
        $("select[name=stype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'ServiceType', 'WithNone': true }
        });
    },
    Table_Init: function () {
        var selected = $("select[name=stype] option:selected").val();
        if (typeof (selected) == 'undefined') {
            selected = "";
        }
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/GetUserOrgList',
            toolbar: '#toolbar',
            uniqueId: 'seqid',
            showExport: true,
            exportDataType: 'basic', //basic', 'all', 'selected'.
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    starttime: $("input[name=starttime]").val(),
                    endtime: $("input[name=endtime]").val(),
                    stype: selected,
                    min_price: $("input[name=min_price]").val(),
                    max_price: $("input[name=max_price]").val(),
                    areacode: $("input[name=areacode]").val(),
                    industry: $("input[name=industry]").val()
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'servicename', title: '机构名称' },
           { field: 'areacode', title: '机构地区' },
           { field: 'industry', title: '行业' },
           { field: 'stypename', title: '服务类型' },
           { field: 'starttime', title: '服务时段', formatter: "infoFormatter" },
           { field: 'paytime', title: '购买时间', formatter: "infoFormatter" },
           { field: 'maxnum', title: '数量' },
           { field: 'm_price', title: '价格' },
            ]
        });
    },
    Search: function () {
        $("#TableData").bootstrapTable('destroy');
        $.Table_Init();
    },
    Datepicker_time: function () {
        $('.input-group.date').datepicker({ autoclose: true });
    },
    UserOrgCount: function () {
        //基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('liter_echart'));
        var Option = {
            title: {
                text: '机构用户人数柱状图',
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
                axisLabel:{
                    textStyle:{
                        color:"black", //刻度颜色
                        fontSize:16  //刻度大小
                    }
                },
                data: ['总用户数', '潜在用户数', '活跃用户数']
            }],
            yAxis: {
                name: '人数',
                backgroundColor: 'rgba(0,0,0,0)',
                borderColor: '#ccc',
                type: 'value',
                textStyle: {
                    color: "black", //刻度颜色
                    fontSize: 16  //刻度大小
                }
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
            url: '/RIPSP/Base/UserOrgCount',
            method: 'GET',
            data: { starttime: $("input[name=starttimebar]").val(), endtime: $("input[name=endtimebar]").val() },
            async: false,
            success: function (result) {
                seriesData[0] = result.rows[0].UsersCount;
                seriesData[1] = result.rows[0].UsersActCount;
            }

        });

        Option.series[0].data = seriesData;
        myChart.setOption(Option);
        myChart.hideLoading();
    }

});
function infoFormatter(value, row, index) {
    if (value == null) {
        return value;
    }

    return value.split("T", 1);
}