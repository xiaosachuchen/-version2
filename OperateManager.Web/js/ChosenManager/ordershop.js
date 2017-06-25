$(document).ready(function () {
    $.Table_Init();
    $('#search').on('click', function () {
        $("#TableData").bootstrapTable('destroy');
        $.Table_Init();
    });
})
var editurl = '';
var editmethod = 'Post';
jQuery.extend({
    Search_Init: function () {
    },
    Table_Init: function (shopname, shopno) {
        var shopname = $("input[name=shopname]").val();
        var shopno = $("input[name=shopno]").val();
        if (typeof (shopname) == 'undefined') {
            shopname = "-1";
        }
        if (typeof (shopno) == 'undefined') {
            shopno = "-1";
        }
		     $('#TableData').bootstrapTable({
		         url: '/RIPSP/Base/ordershop',
		         method: 'Get',
			     toolbar: '#toolbar',                
			     uniqueId: 'orderid',
			      queryParams:function(parm){   //如果无查询面板，不设置这个参数
				         var queryParams = {
					          offset: parm.offset,
					          limit: parm.limit,
					          shopname: shopname,
					          shopno: shopno
				         };
				        return queryParams;
			      },
			      columns: [{ checkbox: true },
				 { field: 'shopname', title: '商品名称'},
				 { field: 'orderno', title: '订单编号'},
				 { field: 'restype', title: '资源类型'},
				 { field: 'rescode', title: '资源编号'},
				 { field: 'thumbnail', title: '缩略图'},
				 { field: 'prices', title: '价格'},
				 { field: 'shopnum', title: '商品数量'},
			      ]
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
		       url: '/RIPSP/Base/ordershop/' + selrow[0].orderid,
			      method: 'Delete',
			      success: function (result) {
				       if (result.Rcode == 1) {
					          $('#TableData').bootstrapTable('refresh');
				       }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },
	  
});
