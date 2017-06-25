$(document).ready(function () {
    //$.Search_Init();
    $.Table_Init();
    //$.Model_Close();
    //$.Validator_Init();
    //$.FormInit();
})
var editurl = '';
var editmethod = 'Get';
jQuery.extend({
    Search: function () {
        $("#TableData").bootstrapTable('destroy');
        $.Table_Init();
    },
    FormInit: function () {
     
    },
    Search_Init: function () {
        //如果有插叙面板，这里绑定查询面板中的选项
        $.Table_Init();
    },
    Table_Init: function () {
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/orderinfos',
            method: 'Get',
            showExport: true,
            exportDataType: 'selected',
            columns:[
           { field: 'Username', title: '用户名称' },
            { field: 'CreatedTime', title: '注册时间', formatter: "infoFormatter" },
          
           { field: 'CountNum', title: '第一次消费金额' },
           { field: 'PayTime', title: '第一次消费时间', formatter: "infoFormatter" },
           { field: 'PayRestype', title: '资源名称' },
           
            ],
          
        });
    },

    FormLoad: function (selrow) {
        //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
        //修改时，在这里获取详情，再绑定表单
        //$.ajax({
        //    url: '/RIPSP/Base/itemcontents/' + selrow[0].seqid,
        //    method: 'Get',
        //    success: function (result) {
        //        if (result.Rcode == 1) {
        //            $.FormInit(result.Rdata);
        //        } else {
        //            $.ShowMessage(result.Rcode, result.Rmsg);
        //            return;
        //        }
        //    }
        //});
        var data = selrow;
        $("#edit_fm").webform('clear');
        if (!!data) {
            $("#edit_fm").webform('load', data);
        }
    },
    Save: function (arg) {
      
    },
    Delete: function (selrow) {
     
    },
    BatchDelete: function (selrows) {
     
    },
    Validator_Init: function () {
    
    },
    Model_Close: function () {
      
    },
    Datepicker_time: function () {
        $('#edit_fm').find('.input-group.date').datepicker({ autoclose: true });
    }
});

function infoFormatter(value, row, index) {
    if (value == null) {
        return value;
    }

    return value.split("T", 1);
}
