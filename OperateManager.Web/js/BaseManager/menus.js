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
            $('#edit_fm').find("input[name=pagemark]").iconPicker();
            $('#edit_fm').find("select[name=isoutlink]").combox({
                url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
                data: { 'dicType': 'YesOrNo'}
            });
            $('#edit_fm').find("input[name=parentmenuid]").comboxtree({
                url: '/RIPSP/Base/Options/GetMenuTreeOptionsByRole',
                data: { 'userid': -1, 'roleId': -1 },
                isroot: true
            });
            $('#edit_fm').find("select[name=status]").combox({
                url: '/RIPSP/Base/Options/GetDicsOptionsByDicType',
                data: { 'dicType': 'EnableStatus' }
            });
      },
	  Search_Init:function(){
	   },
	  Table_Init: function () {
	      $("#TableTree").supertreegrid({	          
	          url: '/RIPSP/Base/menus',
	          method: 'Get',
	          columns: [
                { field: 'menuname', title: '菜单名称' },
		    	{ field: 'pagemark', title: '菜单图标' },
		    	{ field: 'path', title: '菜单路径' },
                {
                    field: 'isoutlink', title: '外链', formatter: function (val, row) {
                       return  val == 1 ? '√' : '×';
                    }
                },
		    	{ field: 'description', title: '菜单描述' },
                { field: 'sorts', title: '菜单排序' },               
	          ],
	          idfield: 'menuid',
	          parentfield: 'parentmenuid',
	          onClickCell: function (field, value, row, $this) {
	              if (field == 'sorts') {	                 
	                  $('#TableTree').ExtendSort({ idField: 'menuid', sortField: 'sorts', tableName: 'base_menus', cellObj: $this, id: row.menuid });
	              }
	          }
	      });
	  },	 
	  Add: function () {
	      editurl = '/RIPSP/Base/menus';
	      editmethod = 'Post';
	      $.FormLoad();
	      $('#edit_modal').modal();
	  },
	  Edit:function(){
	      var selrow = $('#TableTree').data('selectedRow');
	      if (selrow == null) {
	          $.ShowMessage(11);
	          return;
	      }
	      editurl = '/RIPSP/Base/menus/' + selrow.menuid;
	      editmethod = 'Put';
	      $.FormLoad(selrow);
	      $('#edit_modal').modal();
	  },
	  FormLoad: function (selrow) {
	      var data = selrow;
	      $('#edit_fm').webform('clear');
	      if (!!data) {
	          $('#edit_fm').webform('load', data);
	      }
	  },
	 
	  Save: function (arg) {
	      //判断表单验证是否全部通过
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
					          $("#TableTree").supertreegrid('refresh');
					          $('#edit_fm').find("input[name=parentmenuid]").comboxtree('refresh');					         
				       }
				       $('#edit_form_sbtn').button('reset')
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
		      url: '/RIPSP/Base/menus/' + selrow.menuid,
			      method: 'Delete',
			      success: function (result) {
				       if (result.Rcode == 1) {
				           $("#TableTree").supertreegrid('refresh');
				           $('#edit_fm').find("input[name=parentmenuid]").comboxtree('refresh');
				       }
				       $.ShowMessage(result.Rcode, result.Rmsg);
			      }
		    });
	  },
	  Validator_Init: function () {
	      $('#edit_fm').bootstrapValidator({
	          fields: {
	              menuname: {
	                  message: '菜单名称无效!',
	                  validators: {
	                      notEmpty: {
	                          message: '菜单名称不能为空!',
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
