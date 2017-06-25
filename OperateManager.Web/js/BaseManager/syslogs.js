$(document).ready(function () {
    $.FormInit();
    $.Table_Init();
    $.Datepicker_time();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    FormInit: function () {  //表单初始化  选项绑定值等
        $('#toolbar').find("select[name=syslogtypes]").combox({
            url: "/RIPSP/Base/Options/GetDicsOptionsByDicType",
            data: { 'dicType': 'SysLogType', 'WithNone': true },
            selected: function (i, data) {
                $("#TableData").bootstrapTable('destroy');
                $.Table_Init(data);
            }
        });
        $('#toolbar').find("select[name=creator]").combox({
            url: "/RIPSP/Base/Options/GetManagersOptionsByRole",
            data: { 'roleId': 0,'orgId':-1, 'WithNone': true },
        });
    },
    Table_Init: function (logtypes) {
        if (typeof (logtypes) == 'undefined') {
            logtypes = "-1";
        }
        var creators = $("select[name=creator] option:selected").val();
        if (typeof (creators) == 'undefined' || creators==-1) {
            creators = "";
        }
        $('#TableData').bootstrapTable({
		      url: '/RIPSP/Base/syslogs',
			      toolbar: '#toolbar',                
			    uniqueId: 'logid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
			          var queryParams = {
			              offset: parm.offset,
			              limit: parm.limit,
			              logtype: logtypes.toString(),
			              creator: creators,
			              startTime: $("input[name=startTime]").val(),
			              endTime: $("input[name=endTime]").val()
			          };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'logmenu', title: '操作页面'},
				 { field: 'logfunname', title: '操作功能'},
				 { field: 'logtypename', title: '操作类型' },
				 { field: 'logcontent', title: '日志内容'},
				 { field: 'createdtime', title: '创建时间', formatter: "infoFormatter" },
			      ]
		    });
    },
    Search: function () {
        $("#TableData").bootstrapTable('destroy');
        var logtypes = $("select[name=syslogtypes]").val();
        $.Table_Init(logtypes);
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