$(document).ready(function () {
	  $.Search_Init();
	  $.Table_Init();
	  $.FormInit();
	  $.Validator_Init();
	  $.Model_Close();
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    FormInit: function (data) {  //表单初始化  选项绑定值等
        $('#edit_fm').find("input[name=parentclassid]").comboxtree({
            url: "/RIPSP/Base/Options/GetClassTreeOptionsByParent",
            data: { 'parentID': -1, 'WithNone': true }
        });
        $('#icon_file').fileinput({
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
	  Search_Init:function(){
		      //如果有插叙面板，这里绑定查询面板中的选项
	   },
	  Table_Init: function () {
		     $("#TableTree").supertreegrid({
		         url: '/RIPSP/Base/classes',
		         method: 'Get',
		         columns: [
                 { field: 'classname', title: '分类名称' },
				 { field: 'parentclassname', title: '父分类名称' },
				 { field: 'remarks', title: '备注' },
				 { field: 'icon', title: '图标' },
				 { field: 'sorts', title: '排序' },
				 { field: 'tag', title: '标签' },
		         ],
		         idfield: 'classid',
		         parentfield: 'parentclassid',
		         onClickCell: function (field, value, row, $this) {
		             if (field == 'sorts') {
		                 $('#TableTree').ExtendSort({ idField: 'classid', sortField: 'sorts', tableName: 'base_classes', cellObj: $this, id: row.classid });
		             }
		         }
		     });
	  },
	  Add: function () {
	      $("edit_fm input").val("");
		   editurl = '/RIPSP/Base/classes';
		     editmethod = 'Post';
		     $.FormInit();
		     $('#edit_modal').modal();

	  },
	  Edit: function () {
	      var selrow = $('#TableTree').data('selectedRow');
		    if (selrow==null) {
			       $.ShowMessage(11);
			       return;
		    }
		  editurl = '/RIPSP/Base/classes/' + selrow.classid;
		    editmethod = 'Put';
		    $.FormLoad(selrow);
		    $('#edit_modal').modal();
	  },
	  FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单
		    //$.ajax({
		  //    url: '/RIPSP/Base/classes/' + selrow[0].classid,
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
	        $("input[name=icon]").val($('#icon_file').val().substring(12));
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
					          $("#TableTree").supertreegrid('refresh');
					          $('#edit_fm').find("input[name=parentclassid]").comboxtree('refresh');
				       }
				       $('#edit_form_sbtn').button('reset');
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },
	  Delete: function (selrow) {
	      var selrow = $('#TableTree').data('selectedRow');
	      if (selrow == null) {
	          $.ShowMessage(11);
	          return;
	      }
		   $.ajax({
		      url: '/RIPSP/Base/classes/' + selrow.classid,
			      method: 'Delete',
			      success: function (result) {
				       if (result.Rcode == 1) {
				           $("#TableTree").supertreegrid('refresh');
				           $('#edit_fm').find("input[name=parentclassid]").comboxtree('refresh');
				       }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },
	
	  Validator_Init: function () {
	      $('#edit_fm').bootstrapValidator({
	          fields: {
	              classname: {
	                  message: '分类名称无效!',
	                  validators: {
	                      notEmpty: {
	                          message: '分类名称不能为空!',
	                      }
	                  }
	              },
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
 