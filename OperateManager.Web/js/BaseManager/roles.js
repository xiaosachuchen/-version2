$(document).ready(function () {
    
    $.Search_Init();
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
        $('#edit_fm').find("select[name=roletype]").combox({
            url: "/RIPSP/Base/Options/GetDicsOptionsByDicType",
            data: { 'dicType': 'RoleGroup', 'WithNone': false }
        });
    },
    Search_Init: function () {
        //如果有插叙面板，这里绑定查询面板中的选项
    },
    Table_Init: function () {
        $('#TableData').bootstrapTable({
            url: '/RIPSP/Base/roles',
            toolbar: '#toolbar',
            uniqueId: 'roleid',
            queryParams: function (parm) {   //如果无查询面板，不设置这个参数
                var queryParams = {
                    offset: parm.offset,
                    limit: parm.limit
                };
                return queryParams;
            },
            columns: [{ checkbox: true },
           { field: 'rolename', title: '角色名称' },
           { field: 'roletypename', title: '角色类型' },
           { field: 'description', title: '角色描述' },
            ]
        });
        //获取角色类型
    },
    Add: function () {
        editurl = '/RIPSP/Base/roles';
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
        editurl = '/RIPSP/Base/roles/' + selrow[0].roleid;
        editmethod = 'Put';
        $.FormLoad(selrow[0]);
        $('#edit_modal').modal();
    },
    EditMenu: function () {
        var selrow = $('#TableData').bootstrapTable('getSelections');
        if (selrow.length < 1) {
            $.ShowMessage(11);
            return false;
        }
        if (selrow.length > 1) {
            $.ShowMessage(12);
            return false;
        }
        $('#roleid').val(selrow[0].roleid);
        $.ajax({
            url: "/RIPSP/Base/Options/GetMenuTreeOptionsByRole?userId=-1&roleId=" + $('#roleid').val(),
            method: "GET",
            success: function (data) {
                var treedata = OptionsToTreeViewData(data);            
                $menutree = $("#menuSelect").treeview({
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
                $.each($('#menuSelect').treeview('getEnabled'), function (i, item) {
                    if (item.selected == true)
                        $('#menuSelect').treeview('checkNode', item.nodeId, { silent: true });
                });
            }

        });
        $('#menu_modal').modal();

    },
    FormLoad: function (selrow) {
        //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
        //修改时，在这里获取详情，再绑定表单
        //$.ajax({
        //    url: '/RIPSP/Base/roles/' + selrow[0].roleid,
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
        //$.FormInit(selrow[0]);
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
                    $('#edit_form_sbtn').button('reset')
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        });
    },
    Save_RolesMenus: function (arg) {
        selecteds = [];
        arg.button('loading');
        $.each($("#menuSelect").treeview('getChecked'), function (i, item) {
            selecteds.push(item.id);
        });
        $.ajax({
            url: "/RIPSP/Base/rolemenu/" + $('#roleid').val(),
            method: "Put",
            data: {IdArray: selecteds },
            success: function (result) {
                if (result.Rcode == 1) {
                    $('#menu_modal').modal('hide');
                    $('#TableData').bootstrapTable('refresh');
                    $('#edit_form_rolesmenus').button('reset');
                }
                $.ShowMessage(result.Rcode, result.Rmsg);
            }
        })

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
            url: '/RIPSP/Base/roles/' + selrow[0].roleid,
            method: 'Delete',
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
                rolename: {
                    message: '角色名称无效!',
                    validators: {
                        notEmpty: {
                            message: '角色名称不能为空!',
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
