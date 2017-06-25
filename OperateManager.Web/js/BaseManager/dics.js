$(document).ready(function () {
    $.Button();
    $.Search_Init();
    $.FormInit();
    $.Table_Init();
    $.Validator_Init();
    $.Model_Close();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    Button: function () {
        $("#searchpanel button").on('click', function () {
            var mark = $(this).attr('data-mark');
            var markfun = eval('$.' + mark);
            if (markfun != undefined) {
                if (mark == 'Delete' || mark == 'DeleteType') {
                    $.webalert({
                        text: '确认继续操作？',
                        type: 'confirm',
                        fun: function (data) {
                            if (data == true) {
                                markfun();
                            }
                        }
                    });
                } else {
                    markfun();
                }
            }
        });
    },
    FormInit: function () {  //表单初始化  选项绑定值等
        $("select[name=dictypes]").combox({
            url: "/RIPSP/Base/Options/GetDicsOptionsByDicType",
            data: { 'dicType': '' },
            selected: function (i, data) {
                $("#TableData").bootstrapTable('destroy');
                $.Table_Init(data);
            }
        });
        $('#logourl_file').fileinput({
            language: 'zh',
            uploadUrl: "/RIPSP/Base/GlobalFile/FileUpload?dir=pic",
            allowedFileTypes: ['image'],
            deleteUrl: null,
            showPreview: false

        }).on({
            'fileselect': function (event, num, filename) {
                //if (num == 1)
                //    $(this).fileinput('upload');
            },
            'fileuploaded': function (event, data, previewId, index) {
                if (data.response.Success == "ok") {
                    var input = $('#edit_fm').find("input[name=" + $(this).attr('target') + "]");
                    input.val(data.response.url);
                } else {
                    console.log(data.response.ErrorMessage);
                }
            },
            'fileclear': function (event) {
                console.log('fileclear')
                var input = $('#edit_fm').find("input[name=" + $(this).attr('target') + "]");
                input.val('');
            },
        })
    },
    Search_Init: function () {

    },
    Table_Init: function (parameters) {

        if (typeof (parameters) == 'undefined') {
            parameters = "-1";
        }
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/dics',
            toolbar: '#toolbar',
            method: 'get',
            uniqueId: 'dicid',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    selected: parameters
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'dicname', title: '字典名' },
           { field: 'dicvalue', title: '字典值' },
           { field: 'dictype', title: '字典类型' },
           { field: 'isdictypename', title: '是否字典类型' },
           { field: 'icon', title: '图标' },
           { field: 'tag', title: '标签' },
           { field: 'remarks', title: '备注' },
           { field: 'sorts', title: '排序' },
            ],
            onClickCell: function (field, value, row, $this) {
                if (field == 'sorts') {
                    $('#TableData').ExtendSort({ idField: 'dicid', sortField: 'sorts', tableName: 'base_dics', cellObj: $this, id: row.dicid });
                }
            }
        });
    },
    AddType: function () {
        editurl = '/RIPSP/Base/dics';
        editmethod = 'Post';
        $('#disctype_modal').modal();
        $('#disctype_modal').on("shown.bs.modal", function () {
            $("#edit_fm").webform('clear');
        });
    },
    AddDice: function () {
        editurl = '/RIPSP/Base/dics';
        editmethod = 'Post';
        $('#edit_modal').modal();
        $('#edit_modal').on("shown.bs.modal", function () {
            $("#edit_fm").webform('clear');
            $("#myModalName").text("添加字典");
            $("#myModalType").val("添加字典");
            $("#dictype").val($("select[name=dictypes]").val());
        });

    },
    EditDice: function () {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return;
        }
        editurl = '/RIPSP/Base/dics/' + selrow[0].dicid;
        editmethod = 'Put';
        $('#edit_modal').modal();
        $('#edit_modal').on("shown.bs.modal", function () {
            $.FormLoad(selrow[0]);
            $("#myModalName").text("编辑字典");
            $("#myModalType").val("编辑字典");
            $("#myModalDiceId").val(selrow[0].dicid);
            $("#dictype").val($("select[name=dictypes]").val());
        });
    },
    FormLoad: function (selrow, filg) {
        //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
        //修改时，在这里获取详情，再绑定表单
        //$.ajax({
        //    url: '/RIPSP/Base/dics/' + selrow[0].dicid,
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
        if (!$("#edit_fm").data("bootstrapValidator").validate().isValid())
            return false;
        arg.button('loading');
        var sdata = $('#edit_fm').serializeArray();
        sdata.push({ "name": "dictype", "value": $("#dictype").val() });
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
    Save_Dtype: function (arg) {
        if (!$("#edit_dictype").data("bootstrapValidator").validate().isValid())
            return false;
        arg.button('loading');
        var sdata = $('#edit_dictype').serializeArray();
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
                    $('#disctype_modal').modal('hide');
                    $('#TableData').bootstrapTable('refresh');
                    $('#edit_dictype_sbtn').button('reset');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
                location.reload();
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
            url: '/RIPSP/Base/dics/' + selrow[0].dicid,
            method: 'Delete',
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#TableData').bootstrapTable('refresh');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    DeleteType: function (selrows) {
        var selrows = $("select[name=dictypes] option:selected").val();
        if (selrows == null) {
            $.ShowMessage(-1);
            return false;
        }
        $.ajax({
            url: '/RIPSP/Base/dics/',
            method: 'Delete',
            data: { StrIdArray: selrows },
            success: function (result) {
                if (result.Rcode == 1) {

                    $('#TableData').bootstrapTable('refresh');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
        location.reload();
    },
    Validator_Init: function () {
        $('#edit_fm').bootstrapValidator({
            fields: {
                dicname: {
                    message: '字典名无效!',
                    validators: {
                        notEmpty: {
                            message: '字典名不能为空!',
                        },
                        remote: {
                            message: '字典名已存在',
                            url: '/RIPSP/Base/dics',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="dicname"]').val(),
                                    type: "dicname",
                                    flig: $("#myModalType").val(),
                                    dicsid: $("#myModalDiceId").val(),
                                    dictype: $("#dictype").val()
                                };
                            }
                        }
                    }
                },
                dicvalue: {
                    validators: {
                        notEmpty: {
                            message: '字典值不能为空!',
                        },
                        remote: {
                            message: '字典值已存在',
                            url: '/RIPSP/Base/dics',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="dicvalue"]').val(),
                                    type: "dicvalue",
                                    flig: $("#myModalType").val(),
                                    dicsid: $("#myModalDiceId").val(),
                                    dictype: $("#dictype").val()
                                };
                            }
                        }
                    }
                }
            }
        });
        $('#edit_dictype').bootstrapValidator({
            fields: {
                dictype: {
                    message: '字典类型无效!',
                    validators: {
                        notEmpty: {
                            message: '字典类型不能为空!',
                        },
                        remote: {
                            message: '字典类型已存在',
                            url: '/RIPSP/Base/dics',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="dictype"]').val(),
                                    type: "dictype",
                                    flig: $("#myModalType").val(),
                                    dicsid: $("#myModalDiceId").val(),
                                    dictype: "dictype"
                                };
                            }
                        }
                    }
                },
                dictypename: {
                    validators: {
                        notEmpty: {
                            message: '字典类型名不能为空!',
                        },
                        remote: {
                            message: '字典类型名已存在',
                            url: '/RIPSP/Base/dics',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="dictypename"]').val(),
                                    type: "dictypename",
                                    flig: $("#myModalType").val(),
                                    dicsid: $("#myModalDiceId").val(),
                                    dictype: "dictypename"
                                };
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
            $('#edit_form_sbtn').removeAttr("disabled").button("result");
        })
    }
});
