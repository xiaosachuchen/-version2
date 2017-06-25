$(document).ready(function () {
	  $.Table_Init();
	  $.Validator_Init();
	  $.Model_Close();
	  $.FormInit();
	  
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    FormInit: function (data) {  //表单初始化  选项绑定值等
        $('#edit_fm').find("select[name=sex]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'Sex', 'WithNone': false}
        });
        $('#edit_fm').find("select[name=status]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'UserStatus', 'WithNone': false }
        });
        $('#edit_fm').find("select[name=sourcetype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'UserSourceType', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=usertype]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'UserType', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=country]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'Country', 'WithNone': true },
            selected: function (i, data) {
                if (data != 'CN') {
                    $("#edit_fm select[name=areacode]").combox("setvalue", 0);
                } else {
                    $("#edit_fm select[name=areacode]").combox("setvalue", -1);
                }
            }
            
        });
        
        //$("#edit_fm select[name=country]").find("option[value='CN']").attr("selected", "selected");
        $('#edit_fm').find("select[name=areacode]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'Areacode', 'WithNone': true }
        });
        $.Datepicker_time();
        $("#ip_table_body button").on('click', function () {
            var mark = $(this).attr('data-mark');
            var markfun = eval('$.' + mark);
            if (markfun != undefined) {
                if (mark == 'DeleteIp') {
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
        $('#userforcus_modal').find("select[name=ufcSelected]").combox({
            url: "/RIPSP/Base/Options//GetCustomerOptions",
            data: {'page':0,'row':0,'WithNone':true},
        });
     
    },
	Search_Init:function(){
	      //如果有插叙面板，这里绑定查询面板中的选项
	    //$("#TableData").bootstrapTable('selstatus');
	    //$.Table_Init();
	   },
	Table_Init: function () {
	    var status = "";
	    //if ($("select[name=selstatus] option:selected").val())
	    //{
	    //    status = $("select[name=selstatus] option:selected").val();
	    //}
		     $('#TableData').bootstrapTable({
		      url: '/RIPSP/Base/users',
			      toolbar: '#toolbar',                
			    uniqueId: 'userid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit
					          //status: status.tostring()
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'username', title: '用户名'},
				 { field: 'realname', title: '用户姓名'},
				 { field: 'usertypename', title: '用户类型'},
				 { field: 'phone', title: '电话'},
				 { field: 'email', title: '邮箱'},
				 { field: 'countryname', title: '国别'},
				 { field: 'areacodename', title: '所在地区'},
				 { field: 'sexname', title: '性别'},
				 { field: 'birthday', title: '出生年月日',formatter:"infoFormatter"},
				 { field: 'createdtime', title: '创建时间', formatter: "infoFormatter" },
				 { field: 'sourcetypename', title: '来源类型'},
				 //{ field: 'sourceremarks', title: '来源描述'},
				 //{ field: 'customerid', title: '客户编号'},
				 { field: 'statusname', title: '状态'},
			      ]
		    });
	},
	IP_Table_Init: function (arg) {
	    console.log(arg);
	    $('#Ip_TableData').bootstrapTable({
	        url: '/RIPSP/Base/user_ip',
	        toolbar: '#ip_table_body',
	        uniqueId: 'seqid',
	        queryParams: function (parm) {   //如果无查询面板，不设置这个参数
	            var queryParams = {
	                userid: arg,
	                offset: parm.offset,
	                limit: parm.limit
	                //status: status.tostring()
	            };
	            return queryParams;
	        },
	        columns: [{ checkbox: true },
           { field: 'ip_start', title: 'ip段起始' },
           { field: 'ip_end', title: 'ip段结束' },
	        ]
	    });
	},
	Add: function () {

		   editurl = '/RIPSP/Base/users';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
		     $("#edit_fm select[name=country]").combox("setvalue", 'CN');
		     $("#myModalLabel").text("新增用户管理");
		     $("#myModalType").val("新增");
	},
	AddIp: function () {
	    $("#ip_fm").webform('clear');
	    editurl = '/RIPSP/Base/user_ip';
	    editmethod = 'Post';
	    $("#ip_modal").modal();
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
		  editurl = '/RIPSP/Base/users/' + selrow[0].userid;
		    editmethod = 'Put';
		    $.FormLoad(selrow[0]);
		    $('#edit_modal').modal();
		    $("#myModalLabel").text("修改用户管理");
		    $("#myModalType").val("修改");
		    $("#myModalUserId").val(selrow[0].userid);
	},
	IpEdit: function () {
	    var selrow = $('#TableData').bootstrapTable('getSelections');
	    if (selrow.length < 1) {
	        $.ShowMessage(11);
	        return;
	    }
	    if (selrow.length > 1) {
	        $.ShowMessage(12);
	        return;
	    }
	    $.IP_Table_Init(selrow[0].userid);
	    //console.log(selrow[0].userid);
	    $("#user_id").val(selrow[0].userid);
	    $('#ip_table_modal').modal();
	},
	Edit_UserForCus:function()
	{
	    var selrow = $('#TableData').bootstrapTable('getSelections');
	    if (selrow.length < 1) {
	        $.ShowMessage(11);
	        return false;
	    }
	    if (selrow.length > 1) {
	        $.ShowMessage(12);
	        return false;
	    }
	    $('#userforcus_modal').modal();
	    $("select[name=ufcSelected]").combox("setvalue",selrow[0].customerid);
	    editurl = '/RIPSP/Base/users/' + selrow[0].userid;
	    editmethod= 'Put';
	},
	Save_Ufc:function(arg)
	{
	    arg.button('loading');
	    $.ajax({
	        url: editurl,
	        method: editmethod,
	        data: { customerid: $('select[name=ufcSelected] option:selected').val()},
	        beforeSend: function () {
	            // 这里可做表单验证做不了的验证
	            return true;
	        },
	        success: function (result) {
	            if (result.Rcode == 1) {
	                $('#userforcus_modal').modal('hide');
	                $('#TableData').bootstrapTable('refresh');
	                $('#edit_form_userForCus').button('reset');
	            }
	            $.ShowMessage(result.Rcode, result.Rmsg);
	        }
	    });
	},
	FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单
		    //$.ajax({
		  //    url: '/RIPSP/Base/users/' + selrow[0].userid,
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
	       data.birthday=data.birthday.split("T", 1)
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
	IpSave: function (arg) {
	    if (!$('#ip_fm').data("bootstrapValidator").validate().isValid())
	        return false;
	    if ($("input[name=ip_end]").val() == "") {
	        $("input[name=ip_end]").val($("input[name=ip_start]").val());
	    }
	    arg.button('loading');
	    $.ajax({
	        url: editurl,
	        method: editmethod,
	        data: { userid: $("#user_id").val(), ip_start: $('input[name=ip_start]').val(), ip_end: $("input[name=ip_end]").val() },
	        beforeSend: function () {
	            // 这里可做表单验证做不了的验证
	            return true;
	        },
	        success: function (result) {
	            if (result.Rcode == 1) {
	                $('#ip_modal').modal('hide');
	                $('#Ip_TableData').bootstrapTable('refresh');
	                $('#edit_form_ip').button('reset')
	            } else {
	                $("input[name=ip_end]").val("");
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
		      url: '/RIPSP/Base/users/' + selrow[0].userid,
			      method: 'Delete',
			      success: function (result) {
				       if (result.Rcode == 1) {
					          $('#TableData').bootstrapTable('refresh');
				       }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },
	DeleteIp: function () {
	    var selrows = $('#Ip_TableData').bootstrapTable('getSelections');
		   if (selrows.length < 1) {
			       $.ShowMessage(11);
			       return;
		   }
		   if (selrows.length > 1) {
		       $.ShowMessage(12);
		       return;
		   }
		   $.ajax({
		       url: '/RIPSP/Base/user_ip/' + selrows[0].seqid,
		       method: 'Delete',
		       success: function (result) {
		           if (result.Rcode == 1) {
		               $('#Ip_TableData').bootstrapTable('refresh');
		           }
		           $.ShowMessage(result.Rcode, result.Rmsg);
		       }
		   });
	  },
	Validator_Init: function () {
	    $('#edit_fm').bootstrapValidator({
            excluded:[':disabled'],
	          fields: {
	              username: {
	                  message: 'the username is not valid !',
	                  validators: {
	                      notEmpty: {
	                          message: '用户名不能为空',
	                      },
	                      stringLength: {
	                          min: 4,
	                          max: 30,
	                          message: '用户名长度必须在4到30之间'
	                      },
	                          threshold: 4,//只有4位字符才发送ajax请求
	                          remote: {
	                             
	                              url: '/RIPSP/Base/users/',
	                              type: 'Get',
	                              dataType: "json",
	                              delay: 2000,
                                  async:false,
                                  message: '用户名重复！',
                                  data: function () {
                                      return {
                                          codes: $("input[name=username]").val(),
                                          type: "username",
                                          flig: $("#myModalType").val(),
                                          userid: $("#myModalUserId").val()
                                  };
                                  }
	                          
	                      },
	                      regexp: {
	                            regexp: /^[a-zA-Z0-9_\.]+$/,
	                            message: '用户名由数字字母下划线和.组成'
	                         }
	                  }
	              },
	              realname: {
	                  message: 'the usertype is not valid!',
	                  validators: {
	                      stringLength: {
	                          max: 60,
	                          message: '用户名姓名错误，请重新输入！'
	                      },
	                      regexp: {
	                          regexp: /^[A-z]+$|^[\u4E00-\u9FA5]+$/,
	                          message: '用户名姓名只能为中文或则英文'
	                      },
	                  }
	              },
	        phone: {
                    message: 'The phone is not valid!',
                    validators: {
                        regexp: {
                            regexp: /^1[3|5|8]{1}[0-9]{9}$/,
                            message: '请输入正确的手机号码!'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message:'请输入11位手机号码'
                        },
                        threshold: 11,//只有4位字符才发送ajax请求
                        remote: {
                            url: '/RIPSP/Base/users/',
                            type: 'Get',
                            dataType: "json",
                            delay: 2000,
                            async: false,
                            message: '手机号重复！',
                            data: function () {
                                return {
                                    codes: $("input[name=phone]").val(),
                                    type: "phone",
                                    flig: $("#myModalType").val(),
                                    userid: $("#myModalUserId").val()
                                };
                            }

                        }

                    }
                },
            email: {
                validators: {
                    emailAddress: {
                        message: '邮箱格式不正确！！'

                    },
                    remote: {
                        url: '/RIPSP/Base/users/',
                        type: 'Get',
                        dataType: "json",
                        delay: 2000,
                        async: false,
                        message: '邮箱重复！',
                        data: function () {
                            return {
                                codes: $("input[name=email]").val(),
                                type: "email",
                                flig: $("#myModalType").val(),
                                userid: $("#myModalUserId").val()
                            };
                        }

                    }
                }
            },
            country: {
                validators: {
                    callback: {
                        message: '请选择国别！！',
                        callback: function (value, validator) {
                            var options = validator.getFieldElements('country').find("option:selected").text();
                            return (options != "--请选择--");
                        }
                    }
                }
           },
            areacode: {
               validators: {
                   callback: {
                       message: '请选择所在地区！！',
                       callback: function (value, validator) {
                           var options = validator.getFieldElements('areacode').find("option:selected").text();
                           return (options != "--请选择--");
                       }
                   }
               }
           },
            sex: {
               validators: {
                   callback: {
                       message: '请选择性别！！',
                       callback: function (value, validator) {
                           var options = validator.getFieldElements('sex').find("option:selected").text();
                           return (options != "--请选择--");
                       }
                   }
               }
           },
            birthday: {
                validators: {

                    regexp: {
                      
                        regexp: /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/,
                        message: '请输入正确的日期格式：yyyy-MM-dd!'
                    }

               }
            },
            status: {
               validators: {
                   callback: {
                       message: '请选择所要设置的状态！！',
                       callback: function (value, validator) {
                           var options = validator.getFieldElements('status').find("option:selected").text();
                           return (options != "--请选择--");
                       }
                  }
               }
           }
        }
	    });
	    $('#ip_fm').bootstrapValidator({
	        excluded: [':disabled'],
	        fields: {
	            ip_start: {
	                message: 'ip段起始是无效的!',
	                validators: {
	                    notEmpty: {
	                        message: 'ip段起始不能为空',
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
        });
},

    Datepicker_time: function () {
        $('#edit_fm').find('.input-group.date').datepicker({ autoclose: true });
    }

});

function infoFormatter(value, row, index) {
    if (value == null) {
        return value;
    }

    return value.split("T",1);
}
