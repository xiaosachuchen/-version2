$(document).ready(function () {
    $.Table_Init();
    $.FormInit();
    $.Validator_Init();
    $.Model_Close();
   //chosen_shopinfo

})
var editurl = '';
var editmethod = 'Post';
var selecteds = [];
jQuery.extend({
    FormInit: function () {  //表单初始化  选项绑定值等
        $("select[name=shopinfo]").combox({
            url: "/RIPSP/Base/WareManager",
            data: { 'WithNone': false },
            selected: function (i, data) {
                $("#TableData").bootstrapTable('destroy');
                $.Table_Init(data);
            }
        });
    },
    Search_Init: function (arg) {
        //如果有插叙面板，这里绑定查询面板中的选项
    },
    Table_Init: function (arg) {
        var dbname = "";
        if (typeof (arg) == 'undefined' || arg=="-1") {
            dbname = 'self_test';
        } else {
            dbname = $("select[name=shopinfo] option:selected").val();
        }
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/Options/GetShopTableDataList',
            toolbar: '#toolbar',
            uniqueId: 'seqid',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    dbname: dbname,
                    offset: 0,
                    limit: parm.limit
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'title', title: '商品名称' },
           { field: 'm_price', title: '价格' },
           { field: 'm_discount', title: '折扣' },
           { field: 'm_stock', title: '库存数' },
           { field: 'm_deal', title: '交易量' },
           { field: 'hits', title: '点击量' },
           {
               field: 'createdtime',
               title: '创建时间',
               formatter: function (value, row, index) {
                   if (value == null) {
                       return value;
                   }
                   return value.substring(0, 10);
               }
           },
            { field: 'statusname', title: '状态' },
            ]
        });
    },
    Edit: function () {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return;
        }
        editurl = '/RIPSP/Base/Options/PutShopPrices?dbname=' + $("select[name=shopinfo] option:selected").val();
        editmethod = 'Put';
        $.FormLoad(selrow[0]);
        $('#edit_modal').modal();
    },
    FormLoad: function (selrow) {
        var data = selrow;
        $("#edit_fm").webform('clear');
        if (!!data) {
            $("#edit_fm").webform('load', data);
        }
    },
    Save: function (arg) {
        //判断表单验证是否全部通过
        if (!$("#edit_fm").data("bootstrapValidator").validate().isValid())
            return false;
        arg.button('loading');
        var sdata = $('#edit_fm').serializeArray();
        //表单值调整
        $.ajax({
            url: editurl,
            method: editmethod,
            data: sdata,
            beforeSend: function () {
                // 这里可做表单验证做不了的验证
                return true;
            },
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#edit_modal').modal('hide');
                    $('#TableData').bootstrapTable('refresh');
                    $('#edit_form_sbtn').button('reset');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    Validator_Init: function () {
        $('#edit_fm').bootstrapValidator({
            fields: {
                m_price: {
                    message: '价格无效!',
                    validators: {
                        notEmpty: {
                            message: '价格不能为空!',
                        },
                    }
                },
                m_discount: {
                    message: '折扣码无效',
                    validators: {
                        notEmpty: {
                            message: '折扣不能为空!',
                        },
                    },
                },
                m_stock: {
                    message: '库存数无效',
                    validators: {
                        notEmpty: {
                            message: '库存数不能为空!',
                        },
                    },
                },
            }
        });
    },
    Model_Close: function () {
        $('#edit_modal').on('hidden.bs.modal', function () {
            $("#edit_fm").data('bootstrapValidator').destroy();
            $('#edit_fm').data('bootstrapValidator', null);
            $.Validator_Init();
            $("#edit_fm").webform('clear');
        })
    }
});
