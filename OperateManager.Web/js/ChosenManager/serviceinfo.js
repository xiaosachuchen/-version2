$(document).ready(function () {
        $.Button();
        $.Search_Init();
        $.Table_Init();
        $.FormInit();
        $.Validator_Init();
        $.Model_Close();
   
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    Button: function () {
        $("#resou_table_body button").on('click', function () {
            var mark = $(this).attr('data-mark');
            var markfun = eval('$.' + mark);
            if (markfun != undefined) {
                if (mark == 'DeleteResouAut') {
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
        //服务授权
        $("#save_aut").on('click', function () {
            $.Save_aut($(this));
        });
        //指定资源
        $("#save_resou").on('click', function () {
            $.Save_resou($(this));
        });
    },
    FormInit: function (data) {  //表单初始化  选项绑定值等

        $('#edit_fm').find("select[name=status]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'ResServiceStatus', 'WithNone': true }
        });

        $('#edit_fm').find("select[name=stype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'ServiceType', 'WithNone': false }
        });
        $.Datepicker_time();
        $('#demo-text-select').combox({
            url: "/RIPSP/Base/Options/GetDicsOptionsByDicType",
            data: { 'dicType': 'ResServiceStatus', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=customerid]").combox({
            url: '/RIPSP/Base/Options/GetCustomerOptions',
            data: { 'page': 0, 'rows':0,'WithNone':false }
        });
        $("select[name=username]").combox({
            url: '/RIPSP/Base/Options/GetUserOptionsByCustomer',
            data: { 'customerId': -1, 'WithNone': false }
        });
        $("select[name=restype]").combox({
            url: '/RIPSP/Base/Options/GetResourceDBOptionsByManager',
            data: { 'coltype': 0, 'userid': 0, 'isAll': true, 'isTopic': false, 'WithNone': true },
            selected: function (i, data) {
                $("select[name=rulename]").combox({
                    url: '/RIPSP/Base/Options/GetMetaOptionsByDataLibrarys',
                    data: { 'dbid': data, 'WithNone': false }
                });
            }
        });
        $("select[name=rulename]").combox({
            url: '/RIPSP/Base/Options/GetMetaOptionsByDataLibrarys',
            data: {'dbid':0,'WithNone': true }
        });
        $("select[name=types]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'ServiceType', 'WithNone': false },
            selected: function (i, data) {
                $("#TableData").bootstrapTable('destroy');
                $.Table_Init(data);
            }
        });
    },
  
	  Search_Init:function(){
	      //如果有插叙面板，这里绑定查询面板中的选项
	      //$("#TableData").bootstrapTable('destroy');
	      //$.Table_Init();
	   },
	  Table_Init: function (data) {
	      if (typeof data == 'undefined') {
	          data = "1";
	      }
		     $('#TableData').bootstrapTable({
		         url: '/RIPSP/Base/serviceinfo',
			      toolbar: '#toolbar',                
			    uniqueId: 'serviceno',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
			          var queryParams = {
                             type:data,
					          offset: parm.offset,
					          limit: parm.limit
					         
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'serviceno', title: '服务编号'},
				 { field: 'customername', title: '客户名称'},
				 { field: 'stypename', title: '服务类型'},
				 { field: 'starttime', title: '服务开通时间', formatter: "infoFormatter" },
				 { field: 'endtime', title: '服务截至时间', formatter: "infoFormatter" },
				 { field: 'maxnum', title: '数量上限'},
				 { field: 'leftnum', title: '剩余数量'},
				 //{ field: 'creator', title: '创建人'},
				 { field: 'createdtime', title: '创建时间', formatter: "infoFormatter" },
				 { field: 'statusname', title: '服务状态'},
			      ]
		    });
	  },
	  Add: function () {
	      editurl = '/RIPSP/Base/serviceinfo';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
	  },
	  AddResouAut: function () {
	      if ($("#edit_name").text() === "指定资源")
	      {
	          $("#add_resou_modal").modal();
	      } else {
	          $("#add_aut_modal").modal();
	      }
	  },
	  KtService:function()
	  {
	      var selrow = $('#TableData').bootstrapTable('getSelections');
	      if (selrow.length < 1) {
	          $.ShowMessage(11);
	          return;
	      }
	      if (selrow.length > 1) {
	          $.ShowMessage(12);
	          return;
	      }
	      window.location="orderinfo.shtml?serviceno="+selrow[0].serviceno;
	  },
	  Edit:function(){
		     var selrow = $('#TableData').bootstrapTable('getSelections');
		    if (selrow.length < 1) {
			       $.ShowMessage(11);
			       return;
		    }
		    if (selrow.length > 1) {
			       $.ShowMessage(12);
			       return;
		   }
		    editurl = '/RIPSP/Base/serviceinfo/' + selrow[0].serviceno;
		    editmethod = 'Put';
		    $.FormLoad(selrow[0]);
		    $('#edit_modal').modal();
	  },
    //指定资源model
	  EditResou: function () {
	      var selrow = $('#TableData').bootstrapTable('getSelections');
	      if (selrow.length < 1) {
	          $.ShowMessage(11);
	          return;
	      }
	      if (selrow.length > 1) {
	          $.ShowMessage(12);
	          return;
	      }
	      $("#edit_name").text("指定资源");
	      $('#Resou_aut_TableData').bootstrapTable("destroy");
	      $('#Resou_aut_TableData').bootstrapTable({
	          url: '/RIPSP/Base/servicecont',
	          toolbar: '#resou_table_body',
	          uniqueId: 'seqid',
	          queryParams: function (parm) {   //如果无查询面板，不设置这个参数
	              var queryParams = {
	                  serviceno: selrow[0].serviceno,
	                  offset: parm.offset,
	                  limit: parm.limit

	              };
	              return queryParams;
	          },
	          columns: [{ checkbox: true },
                 { field: 'seqid', title: '编号' },
                 { field: 'restype', title: '资源类型' },
                 { field: 'rulename', title: '范围名称' },
                 { field: 'rulevalue', title: '范围值' },
                 { field: 'createdtime', title: '创建时间',formatter: "infoFormatter"  },
	          ]
	      });
	      $("#resou_aut_model").modal();
	  },
    //服务授权model
	  EditAut: function () {
	      var selrow = $('#TableData').bootstrapTable('getSelections');
	      if (selrow.length < 1) {
	          $.ShowMessage(11);
	          return;
	      }
	      if (selrow.length > 1) {
	          $.ShowMessage(12);
	          return;
	      }
	      $("#edit_name").text("服务授权");
	      $('#Resou_aut_TableData').bootstrapTable("destroy");
	      $('#Resou_aut_TableData').bootstrapTable({
	          url: '/RIPSP/Base/servicepermit',
	          toolbar: '#resou_table_body',
	          uniqueId: 'seqid',
	          queryParams: function (parm) {   //如果无查询面板，不设置这个参数
	              var queryParams = {
	                  serviceno: selrow[0].serviceno,
	                  offset: parm.offset,
	                  limit: parm.limit
	              };
	              return queryParams;
	          },
	          columns: [{ checkbox: true },
             { field: 'seqid', title: '编号' },
             { field: 'userid', title: '用户编号' },
             { field: 'nickname',title:'用户名称' }
	          ]
	      });
	      $("#resou_aut_model").modal();
	  },
	  FormLoad:function(selrow){
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
    //提交指定资源
	  Save_resou: function (arg) {
	      arg.button('loading');
	    
	      var selrow = $('#TableData').bootstrapTable('getSelections');
	      var restype = $("select[name=restype] option:selected").val();
	      var rulename = $("select[name=rulename] option:selected").val();
	      var rulevalue = $("input[name=rulevalue]").val();
	      $.ajax({
	          url: "/RIPSP/Base/servicecont",
	          method: "POST",
	          data: { serviceno: selrow[0].serviceno, restype: restype, rulename: rulename, rulevalue: rulevalue },
	          beforeSend: function () {
	              // 这里可做表单验证做不了的验证
	              return true;
	          },
	          success: function (result) {
	              if (result.Rcode == 1) {
	                  $('#add_resou_modal').modal('hide');
	                  $('#Resou_aut_TableData').bootstrapTable('refresh');
	                  $('#save_resou').button('reset')
	              }
	              $.ShowMessage(result.Rcode, result.Rmsg);
	          }
	      });
	  },
    //提交服务授权
	  Save_aut: function (arg) {
	      arg.button('loading');
	      var selrow = $('#TableData').bootstrapTable('getSelections');
	      $.ajax({
	          url: "/RIPSP/Base/servicepermit",
	          method: "POST",
	          data: { customerid:selrow[0].customerid,serviceno:selrow[0].serviceno,userid:$("select[name=username] option:selected").val()},
	          beforeSend: function () {
	              // 这里可做表单验证做不了的验证
	              return true;
	          },
	          success: function (result) {
	              if (result.Rcode == 1) {
	                  $('#add_aut_modal').modal('hide');
	                  $('#Resou_aut_TableData').bootstrapTable('refresh');
	                  $('#save_aut').button('reset')
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
		       url: '/RIPSP/Base/serviceinfo/' + selrow[0].serviceno,
			      method: 'Delete',
			      success: function (result) {
				       if (result.Rcode == 1) {
					          $('#TableData').bootstrapTable('refresh');
				       }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },

	  DeleteResouAut: function (selrows) {
	      var selrow = $('#Resou_aut_TableData').bootstrapTable('getSelections');
	      if (selrow.length < 1) {
	          $.ShowMessage(11);
	          return;
	      }
	      if (selrow.length > 1) {
	          $.ShowMessage(12);
	          return;
	      }
	      if ($("#edit_name").text() === "指定资源") {
	          $.ajax({
	              url: '/RIPSP/Base/servicecont/' + selrow[0].seqid,
	              method: 'Delete',
	              success: function (result) {
	                  if (result.Rcode == 1) {
	                      $('#Resou_aut_TableData').bootstrapTable('refresh');
	                  }
	                  $.ShowMessage(result.Rcode, result.Rmsg);
	              }
	          });
	      } else {
	          $.ajax({
	              url: '/RIPSP/Base/servicepermit/' + selrow[0].seqid,
	              method: 'Delete',
	              success: function (result) {
	                  if (result.Rcode == 1) {
	                      $('#Resou_aut_TableData').bootstrapTable('refresh');
	                  }
	                  $.ShowMessage(result.Rcode, result.Rmsg);
	              }
	          });
	      }
		   
	  },
    Validator_Init: function () {
    $('#edit_fm').bootstrapValidator({
        fields: {
            servicename: {
                message: '服务名称无效!',
                validators: {
                    notEmpty: {
                        message: '服务名称不能为空!',
                    }
                }
            },
           customerid: {
                message: 'the customerid is not valid !',
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
            starttime: {
                validators: {
                    //notEmpty: {
                    //    message: '服务开通时间不能为空！！'

                    //},
                    regexp: {
                        regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                        message: '请输入正确的日期格式：yyyy-MM-dd!'
                    }
                }
            },
            endtime: {
                validators: {
                   
                    date: {
                        format: 'YYYY-MM-DD',
                        message:'服务截止时间格式不正确！'
                    }
                }
            },
            maxnum: {
                message: 'the maxnum is not valid!',
                validators: {
                    notEmpty: {
                        message: '数量上限不能为空！！'

                    },
                    stringLength: {
                        min:1,
                        max: 11,
                        message: '数量上限不能大于11位'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: '数量上限只能为数字！'
                    }
                }
            },
            leftnum: {
                message: 'the usertype is not valid!',
                validators: {
                    stringLength:{
                        max: 11,
                        message:'剩余数量长度过长！'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: '数量上限只能为数字！'
                    }
                }
            },
           //status: {
           //    message: 'the status is not valid!!',
           //     validators: {
              

           //         callback: {
           //             message: '请选择服务状态！！',
           //             callback: function (value, validator) {
           //                 var options = validator.getFieldElements('status').find("option:selected").text();
           //                 return (options != "--请选择--");
           //                 }
           //             }
           //     }
           //},
           stype: {

               message: 'the stype is not valid!!',
               validators: {
                    callback: {
                        message: '请选择服务类型！！',
                        callback: function (value, validator) {
                            var options = validator.getFieldElements('stype').find("option:selected").text();
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
}
