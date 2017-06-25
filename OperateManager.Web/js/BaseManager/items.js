$(document).ready(function () {
	  $.Search_Init();
	  $.Table_Init();
	  $.Validator_Init();
	  $.FormInit();
	  $.Model_Close();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({

    FormInit: function (data) {  //表单初始化  选项绑定值等
        
        $('#edit_fm').find("select[name=navigation]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'YesOrNo', 'WithNone': false }
        });
        $('#edit_fm').find("select[name=status]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'EnableStatus', 'WithNone': false}
        }); 
        $('#edit_fm').find("select[name=sourcetype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'ItemSourceType', 'WithNone': false }
        });
        $('#edit_fm').find("input[name=parentid]").comboxtree({
            url: '/RIPSP/Base/Options/GetItemTreeOptionsByParent',
            data: { 'parentID': 0, 'WithNone': false }
        });
    },
	  Search_Init:function(){
		      //如果有插叙面板，这里绑定查询面板中的选项
	   },
	  Table_Init: function () {
	      $('#TableTree').supertreegrid({
		        url: '/RIPSP/Base/items',
			    method:'Get',
		
			      columns: [
				 //{ field: 'parentid', title: '父级编号'},
				 { field: 'itemname', title: '标题'},
				 { field: 'itemmark', title: '栏目标识' },
                
				 { field: 'navigationname', title: '导航'},
				 { field: 'sourcevalue', title: '资源表达式'},
				 { field: 'sourcetypename', title: '资源来源'},
				 { field: 'sorts', title: '排序'},
				 { field: 'statusname', title: '状态'},
			      ],
			      idfield: 'itemid',
			      parentfield: 'parentid',
			      onClickCell: function (field, value, row, $this) {
			          if (field == 'sorts') {
			              $('#TableTree').ExtendSort({ idField: 'itemid', sortField: 'sorts', tableName: 'base_items', cellObj: $this, id: row.itemid });
			          }
			      }
		    });
	  },
	  Add: function () {
		   editurl = '/RIPSP/Base/items';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
		     $("#myModalLabel").text("添加栏目管理");
		     $("#myModalType").val("添加");
	  },
	  Edit:function(){
	      var selrow = $('#TableTree').data('selectedRow');
		    if (selrow==null) {
			       $.ShowMessage(11);
			       return;
		    }
		    editurl = '/RIPSP/Base/items/' + selrow.itemid;
		    editmethod = 'Put';
		    $.FormLoad(selrow);
		    $('#edit_modal').modal();
		    $("#myModalLabel").text("编辑栏目管理");
		    $("#myModalType").val("编辑");
		    $("#myModalItemId").val(selrow.itemid);
	  },
	  FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单

	      var data = selrow;
	      $('#edit_fm').webform('clear');
	      if (!!data) {
	          $('#edit_fm').webform('load', data);
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
					          $('#TableTree').supertreegrid('refresh');
					          $('#edit_fm').find("input[name=parentid]").comboxtree('refresh');
					          $('#edit_form_sbtn').button('reset')
				        }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },
	  Delete: function (selrow) {

	      var selrow = $('#TableTree').data('selectedRow');
		    if (selrow==null) {
			       $.ShowMessage(11);
			      return;
		   }
		   $.ajax({
		      url: '/RIPSP/Base/items/' + selrow.itemid,
			      method: 'Delete',
			      success: function (result) {
			          if (result.Rcode == 1) {
			              $('#edit_fm').find("input[name=parentid]").comboxtree('refresh');
			              $('#TableTree').supertreegrid('refresh');
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
			      IdArray.push(selrows[i].menuid);
		    }
		    $.ajax({
		      url: '/RIPSP/Base/items/' ,
			      method: 'Delete',
			      data: {IdArray: IdArray},
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
     
            itemname: {
                message: 'the itemname is not valid !',
                validators: {
                 notEmpty: {
                        message: '标题不能为空!'
                    },
                    stringLength: {
                       max: 50,
                        message:'标题长度过长！'
                    }
                }
            },
            itemmark: {
                message: 'the itemmark is not valid!',
                validators: {
                    notEmpty: {
                        message: '栏目标识不能为空!'
                    },
                    stringLength: {
                        max: 50,
                        message: '栏目标识长度过长！'
                    },
                    threshold: 2,
                    remote: {

                        url: '/RIPSP/Base/items/',
                        type: 'Get',
                        dataType: "json",
                        delay: 2000,
                        async: false,
                        message: '栏目标识重复！',
                        data: function () {
                            return {
                                codes: $("input[name=itemmark]").val(),
                                type: "itemmark",
                                flag: $("#myModalType").val(),
                                itemid: $("#myModalItemId").val()
                            };
                        }

                    },
                }
            },
            navigation: {
                message: 'the navigation is not valid!',
                validators: {
                    callback: {
                        message: '请选择是否导航!',
                        callback: function (value, validator) {
                            var options = validator.getFieldElements('navigation').find("option:selected").text();
                            return (options != "--请选择--");
                        }
                    }
                }
            },
            //sourcevalue: {
            //    message: '用户类型无效!',
            //    validators: {
            //        regexp: {
            //            regexp: /^[0-9]*$/,
            //            message: '用户类型只能是正整数!'
            //        },
            //        notEmpty: {
            //            message: '用户类型不能为空',
            //        }
            //    }
            //},
            sourcetype: {
                message: '请选择资源来源!',
                validators: {
                   
                    callback: {
                        message: '请选择资源来源!',
                        callback: function (value, validator) {
                            var options = validator.getFieldElements('sourcetype').find("option:selected").text();
                            return (options != "--请选择--");
                        }
                    }
                }
            },
            sorts: {
                message: '排序无效!',
                validators: {
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: '排序只能数字!'
                    }
                    //notEmpty: {
                    //    message: '账户不能为空',
                    //},
                }
            },
            status: {
                message: '请选择状态!',
                validators: {
                    callback: {
                        message: '请选择状态！',
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
