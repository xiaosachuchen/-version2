$(document).ready(function () {

    $.Search_Init();
    $.Table_Init();
    $.FormInit();
    $.Model_Close();
    $.Validator_Init(); 
    //$.KT();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    FormInit: function (data) {
        $('#serviceno').combox({
            url: '/RIPSP/Base/Options/GetServiceOptions',
            data: { 'customerid': 0, 'page': 0, 'rows': 0, 'WithNone': true },
        });
        $('#status').combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'OrderStatus', 'WithNone': true }
        });
        $('#customerid').combox({
            url: '/RIPSP/Base/Options/GetCustomerOptions',
            data: { 'page': 0, 'rows': 0, 'WithNone': true }
        });
        $('#edit_fm').find("select[name=serviceno]").combox({
            url: '/RIPSP/Base/Options/GetServiceOptions',
            data: { 'customerid':0,'page': 0, 'rows': 0, 'WithNone': true },
            selected: function (i,data)
            {
                $("#edit_fm select[name=customerid]").combox("setvalue", $("#edit_fm select[name=serviceno]").combox("getitem").tag);
            },
            loadcomplete: function (i) {
                //.Add();
                if ($.getUrlParam('serviceno') != null) {
                    $('#edit_modal').modal();
                    $("#edit_fm select[name=serviceno]").combox("setvalue", $.getUrlParam('serviceno'));
                    var dataID = $("#edit_fm select[name=serviceno]").combox("getitem").tag;
                    $("#edit_fm select[name=customerid]").combox("setvalue", dataID);
                }
            }

        });
        $('#edit_fm').find("select[name=paybank]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'PayBank', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=status]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'OrderStatus', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=paytype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'PayType', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=terminaltype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'TerminalType', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=ctype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'Ctype', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=customerid]").combox({
            url: '/RIPSP/Base/Options/GetCustomerOptions',
            data: { 'page': 0, 'rows': 0, 'WithNone': true }
        });
        $.Datepicker_time();


    },
    Search_Init: function () {
        //如果有插叙面板，这里绑定查询面板中的选项
        $.Table_Init();
    },
    Search: function () {
        $("#TableData").bootstrapTable('destroy');
        $.Table_Init();
    },
    Table_Init: function () {
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/orderinfo',
            toolbar: '#toolbar',
            uniqueId: 'orderno',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    serviceno:$('#serviceno').val(),
                    customerid: $('#customerid').val(),
                    status: $('#status').val()
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'orderno', title: '订单编号' },
           { field: 'ordername', title: '订单名称' },
           { field: 'customername', title: '客户名称' },
           { field: 'servicename', title: '服务名称' },
           { field: 'ctypename', title: '下单方式' },
           { field: 'paytypename', title: '支付方式' },
           { field: 'paybankname', title: '支付渠道' },
           { field: 'payno', title: '支付流水' },
           { field: 'createdtime', title: '下单时间', formatter: "infoFormatter" },
           //{ field: 'deliveryoktime', title: '收货时间', formatter: "infoFormatter" },
           { field: 'logisticaltype', title: '物流类型' },
           { field: 'logisticalcode', title: '物流编号' },
           { field: 'statusname', title: '订单状态' },
           //{ field: 'creator', title: '操作人'},
           { field: 'terminaltypename', title: '操作终端' },
            ]
        });
    },
    Add: function () {
        editurl = '/RIPSP/Base/orderinfo';
        editmethod = 'Post';
        $.FormLoad();
        $('#edit_modal').modal();
       
       
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
        editurl = '/RIPSP/Base/orderinfo/' + selrow[0].orderno;
        editmethod = 'Put';
        $.FormLoad(selrow[0]);
        $('#edit_modal').modal();
    },
    FormLoad: function (selrow) {
        //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
        //修改时，在这里获取详情，再绑定表单

        var data = selrow;
        $("#edit_fm").webform('clear');
        if (!!data) {
            $("#edit_fm").webform('load', data);
        }

    },

    Save: function (arg) {

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
                    $('#edit_form_sbtn').button('reset')
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    Delete: function (selrow) {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return;
        }
        $.ajax({
            url: '/RIPSP/Base/orderinfo/' + selrow[0].orderno,
            method: 'Delete',
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#TableData').bootstrapTable('refresh');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    BatchDelete: function (selrows) {
        var selrows = $('#TableData').bootstrapTable('getSelections');
        if (selrows.length < 1) {
            $.ShowMessage(11);
            return;
        }
        var IdArray = [];
        for (var i in selrows) {
            IdArray.push(selrows[i].orderno);
        }
        $.ajax({
            url: '/RIPSP/Base/orderinfo/',
            method: 'Delete',
            data: { IdArray: IdArray },
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#TableData').bootstrapTable('refresh');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },

    Validator_Init: function () {
        $('#edit_fm').bootstrapValidator({

            fields: {

                ordername: {
                    message: 'the ordername is not valid !',
                    validators: {
                        notEmpty: {
                            message: '订单名称不能为空!'
                        },
                        stringLength: {
                            min: 1,
                            max: 200,
                            message: '请输入订单名称！'
                        }
                    }
                },
                userid: {
                    message: 'the userid is not valid!',
                    validators: {
                        stringLength: {
                            max: 11,
                            message: '用户账户过长，请重新输入！'
                        },
                        regexp: {
                            regexp: /^\d+$/,
                            message: '用户账户只能为数字'
                        },
                    }
                },
                customerid: {
                    message: 'the customerid is not valid!',
                    validators: {
                        callback: {
                            message: '请选择客户！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('customerid').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                },
                serviceno: {
                    message: 'the serviceno is not valid!!',
                    validators: {
                        stringLength: {
                            max: 20,
                            message: '服务编号长度过长！'
                        }
                        //regexp: {
                        //    regexp: /^[0-9]*$/,
                        //    message: '客户编号只能是正整数!'
                        //},
                        //notEmpty: {
                        //    message: '用户类型不能为空',
                        //}
                    }
                },
                feetype: {
                    message: 'the feetype is not valid!!!',
                    validators: {
                        //regexp: {
                        //    regexp: /^[0-9]*$/,
                        //    message: '排序只能是正整数!'
                        //},
                        stringLength: {
                            max: 20,
                            message: '货币类型长度过长！'
                        },
                        notEmpty: {
                            message: '货币类型不能为空',
                        },
                    }
                },
                m_price: {
                    message: 'the m_price is not valid!!!',
                    validators: {
                        regexp: {
                            regexp: /^\d+$/,
                            message: '市场价只能为数字!'
                        },
                        stringLength: {
                            min: 1,
                            max: 20,
                            message: '市场价长度过长！'
                        },
                        notEmpty: {
                            message: '市场价不能为空',
                        },
                    }
                },
                s_price: {
                    message: 'the s_price is not valid!!',
                    validators: {
                        regexp: {
                            regexp: /^\d+$/,
                            message: '销售价只能为数字!'
                        },
                        notEmpty: {
                            message: '销售价不能为空',
                        },
                        stringLength: {
                            min: 1,
                            max: 20,
                            message: '销售价长度过长！'
                        }
                    }
                },
                addressid: {
                    message: 'the addressid is not valid!!!',
                    validators: {
                        regexp: {

                            regexp: /^\d+$/,
                            message: '收货地址编号只能为数字！！',
                        },
                        stringLength: {
                            min: 1,
                            max: 11,
                            message: '收货地址编号长度过长！'
                        }
                    }
                },
                ctype: {
                    message: 'The ctype is not valid!',
                    validators: {
                        callback: {
                            message: '请选择下单方式！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('ctype').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                },
                paytype: {
                    message: 'The paytype is not valid!',
                    validators: {
                        callback: {
                            message: '请选择支付方式！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('paytype').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                },
                paybank: {
                    message: 'The paybank is not valid!',
                    validators: {
                        callback: {
                            message: '请选择支付渠道！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('paybank').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                },
                payno: {
                    message: 'The paybank is not valid!',
                    validators: {
                        stringLength: {
                            min: 1,
                            max: 50,
                            message: '支付流水长度过长！'
                        }
                    }
                },
                //createdtime: {
                //    //message: 'The createdtime is not valid!',
                //    validators: {
                //        notEmpty: {
                //            message: '下单时间不能为空！！'
                //        },
                //        regexp: {
                //            regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                //            message: '请输入正确的日期格式：yyyy-MM-dd!'
                //        }
                //        //date: {
                //        //    format: 'MM/DD/YYYY h:m:A'
                //        //}
                //    }
                //},
                paytime: {
                    message: 'The paytime is not valid!',
                    validators: {

                        regexp: {

                            regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                            message: '请输入正确的日期格式：yyyy-MM-dd!'
                        }

                    }
                },
                payresulttime: {//支付通知时间 
                    message: 'The payresulttime is not valid!',
                    validators: {

                        regexp: {

                            regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                            message: '请输入正确的日期格式：yyyy-MM-dd!'
                        }

                    }
                },
                deliverytime: {//发货时间
                    message: 'The deliverytime is not valid!',
                    validators: {

                        regexp: {

                            regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                            message: '请输入正确的日期格式：yyyy-MM-dd!'
                        }

                    }
                },
                deliveryoktime: {//收货时间
                    message: 'The deliveryoktime is not valid!',
                    validators: {

                        regexp: {

                            regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                            message: '请输入正确的日期格式：yyyy-MM-dd!'
                        }

                    }
                },
                logisticaltype: {
                    message: 'The logisticaltype is not valid!',
                    validators: {
                        stringLength: {
                            min: 1,
                            max: 30,
                            message: '物流类型长度过长！'
                        }
                    }
                },
                logisticalcode: {
                    message: 'The logisticalcode is not valid!',
                    validators: {
                        stringLength: {
                            min: 1,
                            max: 50,
                            message: '物流类型长度过长！'
                        }
                    }
                },

                status: {
                    message: 'The status is not valid!',
                    validators: {
                        callback: {
                            message: '请选择状态！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('status').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                },
                terminaltype: {
                    message: 'The terminaltype is not valid!',
                    validators: {
                        callback: {
                            message: '请选择操作终端！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('terminaltype').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                }
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
};

//function getQueryString(name) {
//    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
//    var r = window.location.search.substr(1).match(reg);
//    if (r != null) return (r[2]);
//    return null;
//}
