$(document).ready(function () {
	  $.Search_Init();
	  $.Table_Init();   
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
	  Search_Init:function(){
		      //如果有插叙面板，这里绑定查询面板中的选项
	   },
	  Table_Init: function () {
		     $('#TableData').bootstrapTable({
		      url: '/RIPSP/Base/userlogs',
			      toolbar: '#toolbar',                
			    uniqueId: 'logid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'logtype', title: '日志类型1登录2浏览3阅读4下载5 分享'},
				 { field: 'restype', title: '资源类型'},
				 { field: 'rescode', title: '资源编号'},
				 { field: 'logcontent', title: '日志内容'},
				 { field: 'creator', title: '创建人'},
				 { field: 'createdtime', title: '创建时间'},
			      ]
		    });
	  },
	  Add: function () {
		   editurl = '/RIPSP/Base/userlogs';
		     editmethod = 'Post';
		     $.FormInit();
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
		  editurl = '/RIPSP/Base/userlogs/' + selrow[0].logid;
		    editmethod = 'Put';
		    $.FormLoad(selrow);
		    $('#edit_modal').modal();
	  },
	  FormLoad:function(selrow){
		    //如果数据表包含大文本字段，列表不允许返回大文本，只返回需要在列表显示的字段
		    //修改时，在这里获取详情，再绑定表单
		    //$.ajax({
		  //    url: '/RIPSP/Base/userlogs/' + selrow[0].logid,
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
		    $.FormInit(selrow[0]);
	  },
	    FormInit: function (data) {  //表单初始化  选项绑定值等

		    $('#edit_fm').webform('clear');
		    if (!!data) {
			      $('#edit_fm').webform('load', data);
		    }
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
		      url: '/RIPSP/Base/userlogs/' + selrow[0].logid,
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
		      url: '/RIPSP/Base/userlogs/' ,
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
