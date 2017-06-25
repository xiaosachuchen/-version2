$(document).ready(function () {
    $.FormInit();
    $.Table_Init();
    $.Datepicker_time();
})
jQuery.extend({
    FormInit: function () {
    },
    Table_Init: function () {
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/retrievalment',
            toolbar: '#toolbar',
            uniqueId: 'seqid',
            showExport: true,
            exportDataType: 'selected', //basic', 'all', 'selected'.
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    keyword: $("input[name=keyword]").val(),
                    starttime: $("input[name=starttime]").val(),
                    endtime: $("input[name=endtime]").val()
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'keyword', title: '检索词' },
           { field: 'username', title: '用户名' },
            { field: 'createdtime', title: '检索时间', formatter: "infoFormatter" },
            ]
        });
    },
    Search: function () {
        $("#TableData").bootstrapTable('destroy');
        $.Table_Init();
    },
    Datepicker_time: function () {
    $('.input-group.date').datepicker({ autoclose: true });
}
});
function infoFormatter(value, row, index) {
    if (value == null) {
        return value;
    }

    return value.split("T", 1);
}