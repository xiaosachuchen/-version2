$(document).ready(function () {
	  $.Search_Init();

	  $.Table_Init();
	  $.FormInit();
	  $.Datepicker_time();
	  $.Validator_Init();
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
        $('#edit_fm').find("input[name=subjectclass]").comboxtree({
            url: "/RIPSP/Base/Options/GetClassTreeOptionsByParent",
            data: { 'parentID': -1, 'WithNone': true }
        });
        $('#logo_file').fileinput({
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
        });
        $('#propaganda_file').fileinput({
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
        });
        $('#introduces').summernote({
            height: '300px',
            lang: 'zh-CN',
            callbacks: {
                // onImageUpload的参数为files，summernote支持选择多张图片
                onImageUpload: function (files) {

                },
                onChange: function (contents, $editable) {
                    $("#edit_fm").find("input[name=" + $(this).attr('target') + "]").val(contents);
                },
                onBlur: function () {
                    $("#edit_fm").find("input[name=" + $(this).attr('target') + "]").val($(this).summernote('code'));
                }
            }
        });
        $('#introduces').parent().find(".panel-heading").css('height', 'auto');
       
    },
	  Search_Init:function(){
		      //如果有查询面板，这里绑定查询面板中的选项
	   },
	  Table_Init: function () {
		     $('#TableData').bootstrapTable({
		      url: '/RIPSP/Base/organization',
			      toolbar: '#toolbar',                
			      uniqueId: 'orgid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'orgname', title: '机构名称'},
				 //{ field: 'industry', title: '行业'},
				 //{ field: 'logo', title: 'logo'},
				 //{ field: 'propaganda', title: '宣传图'},
				 //{ field: 'country', title: '国别'},
				 //{ field: 'areacode', title: '地区'},
				 //{ field: 'address', title: '地址'},
				 { field: 'contacts', title: '联系人'},
				 { field: 'telphone', title: '电话'},
				 { field: 'fax', title: '传真'},
				 { field: 'website', title: '网址'},
				 { field: 'email', title: '邮箱'},
				 { field: 'createddate', title: '成立时间',formatter:"infoFormatter"},
				 { field: 'corporate', title: '机构法人'},
				 //{ field: 'subjectclass', title: '主题分类'},
				 //{ field: 'introduce', title: '详细介绍'},
				 //{ field: 'hits', title: '点击量'},
				 //{ field: 'createdtime', title: '创建时间'},
				 //{ field: 'creator', title: '创建人'},
				 //{ field: 'status', title: '状态'},
			      ]
		    });
	  },
	  Add: function () {
		   editurl = '/RIPSP/Base/organization';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
		     $("#edit_fm select[name=country]").combox("setvalue", 'CN');
		     $("#myModalLabel").text("新增机构管理");
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
		  editurl = '/RIPSP/Base/organization/' + selrow[0].orgid;
		    editmethod = 'Put';
		    $.FormLoad(selrow[0]);
		    $('#edit_modal').modal();
		    $("#myModalLabel").text("修改机构管理");
		    $("#myModalType").val("修改");
		    $("#myModalOrgId").val(selrow[0].orgid);
	  },
	  FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单

	      var data = selrow;
	      $('#edit_fm').webform('clear');
	      if (!!data) {
	          data.createddate = data.createddate.split("T",1);
	          $('#edit_fm').webform('load', data);
	      }

	  },

	  Save: function (arg) {
	      if (!$("#edit_fm").data("bootstrapValidator").validate().isValid())
	          return false;
	      arg.button('loading');
	      $("input[name=logo]").val($('#logo_file').val());
	      $("input[name=propaganda]").val($('#propaganda_file').val().substring(12));
	      //$("input[name=introduce]").val($('#introduces').val());
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
					          $('#edit_fm').find("input[name=subjectclass]").comboxtree('refresh');
					          //destroy
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
		      url: '/RIPSP/Base/organization/' + selrow[0].orgid,
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
		      url: '/RIPSP/Base/organization/' ,
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
	  Model_Close: function () {
	      $('#edit_modal').on('hidden.bs.modal', function () {
	          $("#edit_fm").data('bootstrapValidator').destroy();
	          $('#edit_fm').data('bootstrapValidator', null);
	          $.Validator_Init();
	          $("#edit_fm").webform('clear');
	          //$('#introduces').summernote('destroy');
	          //$(".note-editable").html("");
	      })
	  },
	  Datepicker_time: function () {
	      $('#edit_fm').find('.input-group.date').datepicker({ autoclose: true });
	  },
	  Validator_Init: function () {
	      $('#edit_fm').bootstrapValidator({
	          fields: {
	              orgname: {
	                  message: 'the orgname is not valid !',
	                  validators: {
	                      notEmpty: {
	                          message: '机构名称不能为空',
	                      },
	                      stringLength: {
	                          min: 3,
	                          max: 100,
	                          message: '机构名称长度必须在3到30之间'
	                      },
	                      regexp: {
	                          regexp: /^[A-z]+$|^[\u4E00-\u9FA5]+$/,
	                          message: '机构名称只能为中文或则英文'
	                      },
	                      threshold: 3,//只有4位字符才发送ajax请求
	                      remote: {
	                          url: '/RIPSP/Base/organization/',
	                          type: 'Get',
	                          dataType: "json",
	                          delay: 2000,
	                          async: false,
	                          message: '机构名称重复！',
	                          data: function () {
	                              return {
	                                  codes: $("input[name=orgname]").val(),
	                                  type: "orgname",
	                                  flig: $("#myModalType").val(),
	                                  orgid: $("#myModalOrgId").val()
	                              };
	                          }

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
	              contacts: {
	                  message: 'the contacts is not valid !',
	                  validators: {
	                      regexp: {
	                          regexp: /^[A-z]+$|^[\u4E00-\u9FA5]+$/,
	                          message: '联系人只能为中文或则英文'
	                      },
	                      stringLength: {
	                          min: 3,
	                          max: 20,
	                          message: '联系人长度必须在3到20之间'
	                      }
	                  }
	              },
	              //email: {
	              //    validators: {
	              //        emailAddress: {
	              //            message: '邮箱格式不正确！！'

	              //        },
	              //        remote: {
	              //            url: '/RIPSP/Base/customers/',
	              //            type: 'Get',
	              //            dataType: "json",
	              //            delay: 2000,
	              //            async: false,
	              //            message: '客户名称重复！',
	              //            data: function () {
	              //                return { codes: $("input[name=email]").val(), type: "email" };
	              //            }

	              //        }
	              //    }
	              //},
	              //phone: {
	              //    message: 'The phone is not valid!',
	              //    validators: {
	              //        regexp: {
	              //            regexp: /^1[3|5|8]{1}[0-9]{9}$/,
	              //            message: '请输入正确的手机号码!'
	              //        },
	              //        stringLength: {
	              //            min: 11,
	              //            max: 11,
	              //            message: '请输入11位手机号码'
	              //        },
	              //        notEmpty: {
	              //            message: '电话号不能为空!'
	              //        },
	              //        threshold: 11,
	              //        remote: {
	              //            url: '/RIPSP/Base/customers/',
	              //            type: 'Get',
	              //            dataType: "json",
	              //            delay: 2000,
	              //            async: false,
	              //            message: '手机号重复！',
	              //            data: function () {
	              //                return { codes: $("input[name=phone]").val(), type: "phone" };
	              //            }

	              //        }
	              //    }
	              //},
	              //fax: {
	              //    message: 'the fax is not valid!',
	              //    validators: {
	              //        fax: {
	              //            message: '传真格式不正确!'
	              //        }
	              //    }
	              //},
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
	              //postcode: {
	              //    validators: {
	              //        stringLength: {
	              //            min: 6,
	              //            max: 6,
	              //            message: '邮编格式不正确'
	              //        }
	              //    }
	              //},
	            

	              //website: {

	              //    validators: {
	              //        url: {
	              //            message: '网站格式不正确！！'
	              //        }
	              //    }
	              //}

	          }
	      });
	  }
});
function infoFormatter(value, row, index) {
    if (value == null) {
        return value;
    }

    return value.split("T", 1);
}