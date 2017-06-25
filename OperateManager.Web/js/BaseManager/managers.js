$(document).ready(function () {
    $.Table_Init();
    $.FormInit();
    $.Validator_Init();
    $.Model_Close();

})
var editurl = '';
var editmethod = 'Post';
var selecteds = [];
jQuery.extend({
    FormInit: function () {  //表单初始化  选项绑定值等
        $('#edit_fm').find("select[name=status]").combox({
            url: "/RIPSP/Base/Options/GetDicsOptionsByDicType",
            data: { 'dicType': 'UserStatus', 'WithNone': false },
        });
        $('#org_modal').find("select[name=orgSelected]").combox({
            url: "/RIPSP/Base/Options/GetOrgOptions",
            data: { 'page': 0, 'WithNone': true },
        });
    },
    Table_Init: function () {
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/managers',
            toolbar: '#toolbar',
            uniqueId: 'userid',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit,
                    nametype: $("#search_select").find("option:selected").val(),
                    name: $("input[name=search_name]").val()
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'username', title: '用户名' },
           { field: 'realname', title: '用户姓名' },
           { field: 'phone', title: '电话' },
           { field: 'email', title: '邮箱' },
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
    Add: function () {
        editurl = '/RIPSP/Base/managers';
        editmethod = 'Post';
        $.FormInit();
        $('#edit_modal').modal();
        $("#myModalLabel").text("添加管理员管理");
        $("#myModalType").val("添加");
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
        editurl = '/RIPSP/Base/managers/' + selrow[0].userid;
        editmethod = 'Put';
        $.FormLoad(selrow[0]);
        $('#edit_modal').modal();
        $("#myModalLabel").text("编辑管理员管理");
        $("#myModalType").val("编辑");
        $("#myModalUserId").val(selrow[0].userid);
    },
    EditOrg: function () {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return false;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return false;
        }

        $('#org_modal').modal();

        $("select[name=orgSelected]").combox("setvalue", selrow[0].orgid);
        editurl = '/RIPSP/Base/managers/' + selrow[0].userid;
        editmethod = 'Put';
    },
    EditDb: function () {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return false;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return false;
        }
        $('#databaseid').val(selrow[0].userid);
        $.ajax({
            url: "/RIPSP/Base/managers/" + $('#databaseid').val(),
            method: "GET",
            success: function (data) {
                var treedata = OptionsToTreeViewData(data.result);
                $menutree = $("#DbSelect").treeview({
                    data: treedata,
                    levels: 2,
                    showCheckbox: true,
                    highlightSelected: false,
                    state: {
                        checked: true,
                        selected: false
                    },
                    onNodeSelected: function (event, nodedata) {
                        var checkednodes = $("#tree").treeview('getChecked');
                        var ischecked = false;
                        $.each(checkednodes, function (i, item) {
                            if (item.id == nodedata.id) {
                                ischecked = true;
                                return false;
                            }
                        })
                        if (ischecked)
                            $('#tree').treeview('uncheckNode', nodedata.nodeId, { silent: true });
                        else
                            $('#tree').treeview('checkNode', nodedata.nodeId, { silent: true });
                        $('#tree').treeview('unselectNode', nodedata.nodeId, { silent: true });
                    },
                });
                $.each($('#DbSelect').treeview('getEnabled'), function (i, item) {
                    if (item.selected == true)
                        $('#DbSelect').treeview('checkNode', item.nodeId, { silent: true });
                });
            }

        });
        $("#db_modal").modal();
    },
    EditDbRoles: function () {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return false;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return false;
        }
        $('#databaseid').val(selrow[0].userid);
        $.ajax({
            url: "/RIPSP/Base/mgdbrole/" + $('#databaseid').val(),
            method: "GET",
            success: function (data) {
                var treedata = OptionsToTreeViewData(data.result);
                $menutree = $("#MgRoleDbSelect").treeview({
                    data: treedata,
                    levels: 2,
                    showCheckbox: true,
                    highlightSelected: false,
                    state: {
                        checked: true,
                        selected: false
                    },
                    onNodeSelected: function (event, nodedata) {
                        var checkednodes = $("#tree").treeview('getChecked');
                        var ischecked = false;
                        $.each(checkednodes, function (i, item) {
                            if (item.id == nodedata.id) {
                                ischecked = true;
                                return false;
                            }
                        })
                        if (ischecked)
                            $('#tree').treeview('uncheckNode', nodedata.nodeId, { silent: true });
                        else
                            $('#tree').treeview('checkNode', nodedata.nodeId, { silent: true });
                        $('#tree').treeview('unselectNode', nodedata.nodeId, { silent: true });
                    },
                });
                $.each($('#MgRoleDbSelect').treeview('getEnabled'), function (i, item) {
                    if (item.selected == true)
                        $('#MgRoleDbSelect').treeview('checkNode', item.nodeId, { silent: true });
                });
            }

        });
        $("#mgrole_modal").modal();
    },
    FormLoad: function (selrow) {
        //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
        //修改时，在这里获取详情，再绑定表单
        //$.ajax({
        //    url: '/RIPSP/Base/managers/' + selrow[0].userid,
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
        //判断表单验证是否全部通过
        if (!$("#edit_fm").data("bootstrapValidator").validate().isValid())
            return false;
        arg.button('loading');
        var sdata = $('#edit_fm').serializeArray();
        // return false;
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
            url: '/RIPSP/Base/managers/' + selrow[0].userid,
            method: 'Delete',
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#TableData').bootstrapTable('refresh');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    Save_Org: function (arg) {
        arg.button('loading');
        $.ajax({
            url: editurl,
            method: editmethod,
            data: { orgid: $('select[name=orgSelected] option:selected').val() },
            beforeSend: function () {
                // 这里可做表单验证做不了的验证
                return true;
            },
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#org_modal').modal('hide');
                    $('#TableData').bootstrapTable('refresh');
                    $('#edit_form_magagerorg').button('reset');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    Save_Db: function (arg) {
        selecteds = [];
        arg.button('loading');
        $.each($("#DbSelect").treeview('getChecked'), function (i, item) {
            selecteds.push(item.id);
        });
        $.ajax({
            url: "/RIPSP/Base/mgdbmenu/" + $('#databaseid').val(),
            method: "Put",
            data: { IdArray: selecteds },
            success: function (result) {
                if (result.ewsqazc == 1) {
                    $('#db_modal').modal('hide');
                    $('#TableData').bootstrapTable('refresh');
                    $('#edit_form_db').button('reset');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        })
    },
    Save_MangDbRoles: function (arg) {
        selecteds = [];
        arg.button('loading');
        $.each($("#MgRoleDbSelect").treeview('getChecked'), function (i, item) {
            selecteds.push(item.id);
        });
        if (selecteds.length == 0) {
            selecteds.push(0);
        }
        $.ajax({
            url: "/RIPSP/Base/mgdbrole/" + $('#databaseid').val(),
            method: "Put",
            data: { IdArray: selecteds },
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#mgrole_modal').modal('hide');
                    $('#TableData').bootstrapTable('refresh');
                    $('#edit_form_mgrole').button('reset');
                    $.ShowMessage(result.Rcode, result.Rmsg);
                } else if (result.Rcode==-2) {
                    $.ShowMessage(11);
                }
                
            }
        })
    },
    Validator_Init: function () {
        $('#edit_fm').bootstrapValidator({
            fields: {
                username: {
                    message: '用户名无效!',
                    validators: {
                        notEmpty: {
                            message: '用户名不能为空!',
                        },
                        stringLength: {
                            min: 5,
                            max: 30,
                            message: '用户名长度必须在5到30之间'
                        },
                        remote: {
                            message: '用户名已存在',
                            url: '/RIPSP/Base/managers',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="username"]').val(),
                                    type: "username",
                                    flig: $("#myModalType").val(),
                                    userid: $("#myModalUserId").val()
                                };
                            }

                        }
                    }
                },
                phone: {
                    message: '手机号码无效',
                    validators: {
                        notEmpty: {
                            message: '手机号码不能为空!',
                        },
                        regexp: {
                            regexp: /^1[0-9]{10}$/,
                            message: '请输入正确的手机号码'
                        },
                        remote: {
                            message: '手机号码已存在',
                            url: '/RIPSP/Base/managers',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="phone"]').val(),
                                    type: "phone",
                                    flig: $("#myModalType").val(),
                                    userid: $("#myModalUserId").val()
                                };
                            }

                        }
                    },
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: '邮件地址不能为空!',
                        },
                        emailAddress: {
                            message: '邮箱地址格式有误！',
                        },
                        remote: {
                            message: '邮件地址已存在',
                            url: '/RIPSP/Base/managers',
                            type: 'GET',
                            delay: 2000,
                            data: function (validator) {
                                return {
                                    codes: $('input[name="email"]').val(),
                                    type: "email",
                                    flig: $("#myModalType").val(),
                                    userid: $("#myModalUserId").val()
                                };
                            }
                        }
                    }
                },
                status: {
                    validators: {
                        callback: {
                            message: '请选择有效内容!',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('status').find("option:selected").text();
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
    }
});
