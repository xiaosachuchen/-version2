$(document).ready(function () {
	  $.Search_Init();
	  $.Table_Init();
	  $.Validator_Init();
	  $.Model_Close();
	  $.FormInit();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    FormInit: function (data) {  //表单初始化  选项绑定值等
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
        $('#edit_fm').find("select[name=areacode]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'AreaCode', 'WithNone': true }
        });
        $('#edit_fm').find("select[name=industry]").combox({
            url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
            data: { 'dicType': 'Industry', 'WithNone': true }
        });
        $('#logourl_file').fileinput({
            language: 'zh',
            uploadUrl: "/RIPSP/Base/GlobalFile/FileUpload?dir=pic",
            allowedFileTypes: ['image'],
            deleteUrl: null,
            showPreview:false
          
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
	  Search_Init:function(){
		      //如果有插叙面板，这里绑定查询面板中的选项
	   },
	  Table_Init: function () {
		     $('#TableData').bootstrapTable({
		      url: '/RIPSP/Base/customers',
			      toolbar: '#toolbar',                
			      uniqueId: 'customerid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'customername', title: '客户名称'},
				 { field: 'contacts', title: '联系人'},
				 { field: 'email', title: '邮箱'},
				 { field: 'phone', title: '电话'},
				 { field: 'fax', title: '传真'},
				 { field: 'countryname', title: '国别'},
				 { field: 'areacodename', title: '所在地区'},
				 { field: 'postcode', title: '邮编'},
				 { field: 'industryname', title: '行业'},
				 { field: 'website', title: '网站'},
				 { field: 'logourl', title: 'logo路径'},
				
				 { field: 'createdtime', title: '创建时间', formatter: "infoFormatter" },
			      ]
		    });
	  },
	  Add: function () {
		   editurl = '/RIPSP/Base/customers';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
		     $("#edit_fm").webform('clear');
		     $("#edit_fm select[name=country]").combox("setvalue", 'CN');
		     $("#myModalName").text("新增客户管理");
		     $("#myModalType").val("新增");
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
		  editurl = '/RIPSP/Base/customers/' + selrow[0].customerid;
		    editmethod = 'Put';
		    $.FormLoad(selrow[0]);
		    $('#edit_modal').modal();
		    $("#myModalName").text("修改客户管理");
		    $("#myModalType").val("修改");
		
		    $("#myModalCustomerId").val(selrow[0].customerid);
	 },
	  FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单
		    //$.ajax({
		  //    url: '/RIPSP/Base/customers/' + selrow[0].customerid,
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
	      $('#edit_fm').webform('clear');
	      if (!!data) {
	          $('#edit_fm').webform('load', data);
	      }
	  },
	
	  Save: function (arg) {
	        if (!$("#edit_fm").data("bootstrapValidator").validate().isValid())
	            return false;
	        arg.button('loading');
	        $("input[name=logourl]").val($('#logourl_file').val());
	        $("input[name=introduction]").val($("textarea[name=introductions]").val());
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
		      url: '/RIPSP/Base/customers/' + selrow[0].customerid,
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
			      IdArray.push(selrows[i].menuid);
		    }
		    $.ajax({
		      url: '/RIPSP/Base/customers/' ,
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
            customername: {
                message: 'the customername is not valid !',
                validators: {
                    notEmpty: {
                        message: '客户名称不能为空',
                    },
                    stringLength: {
                        min: 3,
                        max: 30,
                        message: '客户名称长度必须在3到30之间'
                    },
                    regexp: {
                        regexp: /^[A-z]+$|^[\u4E00-\u9FA5]+$/,
                        message: '客户名称只能为中文或则英文'
                    },
                        threshold: 4,//只有4位字符才发送ajax请求
                        remote: {
                            url: '/RIPSP/Base/customers/',
                            type: 'Get',
                            dataType: "json",
                            delay: 2000,
                            async: false,
                            message: '客户名称重复！',
                            data: function () {
                                return {
                                    codes: $("input[name=customername]").val(),
                                    type: "customername",
                                    flig: $("#myModalType").val(),
                                    customerid: $("#myModalCustomerId").val()
                                };
                            }

                        }
                }
            },
            contacts: {
                message: 'the contacts is not valid !',
                validators: {
                    regexp: {
                        regexp: /^[A-z]+$|^[\u4E00-\u9FA5]+$/,
                        message: '联系人只能为中文或则英文'
                    },
                    notEmpty: {
                        message: '联系人不能为空!'
                    },
                    stringLength: {
                        min: 3,
                        max: 30,
                        message:'联系人长度必须在6到30之间'
                    }
                }
            },
            email: {
                validators: {
                    emailAddress: {
                        message: '邮箱格式不正确！！'

                    },
                    notEmpty: {
                        message: '邮箱不能为空!'
                    },
                    remote: {
                        url: '/RIPSP/Base/customers/',
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
                                customerid: $("#myModalCustomerId").val()
                            };
                        }

                    }
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
                        message: '请输入11位手机号码'
                    },
                    notEmpty: {
                        message: '电话号不能为空!'
                    },
                    threshold: 11,
                    remote: {
                        url: '/RIPSP/Base/customers/',
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
                                customerid: $("#myModalCustomerId").val()
                            };
                        }

                    }
                }
            },
            fax:{
                message:'the fax is not valid!',
                validators: {
                    fax: {
                        message:'传真格式不正确!'
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
                            message: '请选择所在地！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('areacode').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }
                    }
                },
           postcode: {
                    validators: {
                        stringLength: {
                            min: 6,
                            max: 6,
                            message: '邮编格式不正确'
                        }
                    }
                },
            industry: {
                    validators: {
                        callback: {
                            message: '请选择行业！！',
                            callback: function (value, validator) {
                                var options = validator.getFieldElements('industry').find("option:selected").text();
                                return (options != "--请选择--");
                            }
                        }

                    }
                },
           
            website: {
               
                validators: {
                    url: {
                        message: '网站格式不正确！！'
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

});
function infoFormatter(value, row, index) {
    if (value == null) {
        return value;
    }

    return value.split("T", 1);
}

