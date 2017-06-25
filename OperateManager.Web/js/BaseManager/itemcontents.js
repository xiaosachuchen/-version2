$(document).ready(function () {
      $.Search_Init();
      $.Table_Init();
	  $.Model_Close();
	  $.Validator_Init();
	  $.FormInit();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    Search: function () {
        $("#TableData").bootstrapTable('destroy');
        $.Table_Init();
    },
    FormInit: function () {
        //$('#searchpanel').find("input[name=itemname]").comboxtree({
        //    url: "/RIPSP/Base/Options/GetItemTreeOptionsByParent",
        //    data: { 'parentID': 0, 'WithNone': true },
            
        //});
        $("#itemname").comboxtree({
            url: "/RIPSP/Base/Options/GetItemTreeOptionsByParent",
            data: { 'parentID': 0, 'WithNone': true }
        });
        $('#edit_fm').find("input[name=itemmark]").comboxtree({
            url: '/RIPSP/Base/Options/GetItemTreeOptionsByParent',
            data: { 'parentID': 0, 'WithNone': true }
            
        });
        $('#edit_fm').find("select[name=status]").combox({
            url: "/RIPSP/Base/Options/GetDicsOptionsByDicType",
            data: { 'dicType': 'ItemContentStatus', 'WithNone': true },
        });
        $.Datepicker_time();

        $('#thumbnail_file').fileinput({
            language: 'zh',
            uploadUrl: "/RIPSP/Base/GlobalFile/FileUpload?dir=pic",
            allowedFileTypes: ['image'],
            deleteUrl: null,
            showPreview: false,
        }).on({
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
    },
	  Search_Init:function(){
	      //如果有插叙面板，这里绑定查询面板中的选项
	      $.Table_Init();
	   },
	  Table_Init: function () {
		     $('#TableData').bootstrapTable({
		      url: '/RIPSP/Base/itemcontents',
			      toolbar: '#toolbar',                
			    uniqueId: 'seqid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit,
					          itemID: $("#itemname").val()
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'itemmark', title: '栏目名称'},
				 { field: 'title', title: '标题'},
				 { field: 'keywords', title: '关键词'},
				 { field: 'thumbnail', title: '图片'},
				 { field: 'sorts', title: '排序'},
				 { field: 'createdtime', title: '创建时间', formatter: "infoFormatter" },
			
			      ],
			      onClickCell: function (field, value, row, $this) {
			          if (field == 'sorts') {
			              $('#TableTree').ExtendSort({ idField: 'seqid', sortField: 'sorts', tableName: 'base_itemcontents', cellObj: $this, id: row.seqid });
			          }
			      }
		    });
	  },
	  Add: function () {
		   editurl = '/RIPSP/Base/itemcontents';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
		     var ItemSelid = $("#itemname").val();
		     $('#edit_fm').find("input[name=itemmark]").comboxtree('setvalue', ItemSelid);
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
		  editurl = '/RIPSP/Base/itemcontents/' + selrow[0].seqid;
		    editmethod = 'Put';
		    $.FormLoad(selrow[0]);
		    $('#edit_modal').modal();
	  },
	  FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单
		    //$.ajax({
		  //    url: '/RIPSP/Base/itemcontents/' + selrow[0].seqid,
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
	      $("input[name=thumbnail]").val($('#thumbnail_file').val().substring(12));
	      $("input[name=abstracts]").val($("textarea[name=abstractss]").val());
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
		      url: '/RIPSP/Base/itemcontents/' + selrow[0].seqid,
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
		       IdArray.push(selrows[i].seqid);
		    }
		    $.ajax({
		      url: '/RIPSP/Base/itemcontents/' ,
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
	          excluded: [':disabled'],
	          fields: {
	             
	              title: {
	                  message: 'the title is not valid!',
	                  validators: {
	                      stringLength: {
	                          max: 200,
	                          message: '标题长度过长！'
	                      },
	                      notEmpty: {
	                          message: '标题不能为空！',
	                      },
	                  }
	              },
	              keywords: {
	                  message: 'The keywords is not valid!',
	                  validators: {
	                      regexp: {
	                          regexp: /^[A-z]+$|^[\u4E00-\u9FA5]+$/,
	                          message: '关键词只能为中文或则英文'
	                      },
	                      stringLength: {
	                          max: 100,
	                          message: '关键词长度过长！'
	                      }
	                  }
	              },
	              abstracts: {
	                  validators: {
	                      stringLength: {
	                          max: 100,
	                          message: '摘要长度过长！'
	                      }
	                  }
	              },
	              aboutdate: {
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

    return value.split("T", 1);
}
