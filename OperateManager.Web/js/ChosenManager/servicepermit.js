$(document).ready(function () {
      $.FormInit();
	  $.Search_Init();
	  $.Table_Init();   
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    FormInit: function (data) {  //表单初始化  选项绑定值等
        $('#edit_fm').find("select[name=customerid]").combox({
            url: '/RIPSP/Base/Options/GetCustomerOptions',
            data: { 'page': 0, 'rows': 0, 'WithNone': true }
        });
    },
    FormLoad: function (selrow) {
        var data = selrow;
        $("#edit_fm").webform('clear');
        if (!!data) {
            $("#edit_fm").webform('load', data);
        }
    },
	  Search_Init:function(){
		      //如果有插叙面板，这里绑定查询面板中的选项
	   },
	  Table_Init: function () {
		     $('#TableData').bootstrapTable({
		         url: '/RIPSP/Base/servicepermit',
			      toolbar: '#toolbar',                
			    uniqueId: 'seqid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'seqid', title: '编号'},
				 { field: 'serviceno', title: '服务编号'},
				 { field: 'customerid', title: '客户编号'},
				 { field: 'userid', title: '用户编号'},
				 { field: 'creator', title: '创建人'},
				 { field: 'createdtime', title: '创建时间'},
			      ]
		    });
	  },
	  Add: function () {
	      editurl = '/RIPSP/Base/servicepermit';
		     editmethod = 'Post';
		     $.FormLoad();
		     $('#edit_modal').modal();
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
		    editurl = '/RIPSP/Base/servicepermit/' + selrow[0].seqid;
		    editmethod = 'Put';
		    $.FormLoad(selrow[0]);
		    $('#edit_modal').modal();
	  },
	  Save: function () {
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
		       url: '/RIPSP/Base/servicepermit/' + selrow[0].seqid,
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
		      url: '/RIPSP/Chosen/servicepermit/' ,
			      method: 'Delete',
			      data: {IdArray: IdArray},
			      success: function (result) {
				       if (result.Rcode == 1) {
					         $('#TableData').bootstrapTable('refresh');
				       }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		   });
	  }
});
