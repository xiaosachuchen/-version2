$(document).ready(function () {
    //$.Table_Count_Init();
    $.Table_Init();
    $.Datepicker_time();
    $.Search();
})
jQuery.extend({
   
    Table_Count_Init: function () {
        $('#TableDataUserCount').bootstrapTable({
            url: '/RIPSP/Base/usersment',
            toolbar: '#toolCount',
            uniqueId: 'seqid',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    starttime: $("input[name=starttime]").val(),
                    endtime: $("input[name=endtime]").val()
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'UsersCount', title: '总用户数' },
           { field: 'UsersNewCount', title: '新注册用户数' },
           { field: 'UsersActCount', title: '活跃用户数' },
            ]
        });
        
    },
    Table_Init: function () {
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/UserSource',
            toolbar: '#toolbar',
            uniqueId: 'seqid',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    starttime: $("input[name=starttimebar]").val(),
                    endtime: $("input[name=endtimebar]").val()
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'UsersLoss', title: '用户流失率' },
           { field: 'UsersRete', title: '用户留存率' },
           { field: 'UsersActCount', title: '重复购买率' },
            ]
        });
    },
    Search: function () {
        $("#Search_Count").on('click', function () {
            DrowEchart();
        })
    },
    Search_User: function () {
        UserPerRate();
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