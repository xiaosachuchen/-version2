using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 菜单管理 menus Controller 
	 /// </summary>
	 public class menusController : BaseController.BaseController 
	 {
		 /// <summary>
		 /// 菜单管理 列表查询 
		 /// </summary>
		 public object Get()
		 {

             List<base_menus> list = menusBLL.GetPageList(out errmsg);
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list };
		 }

		 /// <summary>
		 /// 菜单管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
            base_menus info = menusBLL.GetInfo(id, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 菜单管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_menus info) 
		 {
            info.status = 1;
			 bool success = menusBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 菜单管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_menus info) 
		 {
			 info.menuid = id; 	
			 bool success = menusBLL.Update(info, out errmsg);
             WriteSysLog(1, string.Format("更新菜单管理 id为 {0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 菜单管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = menusBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除菜单管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 菜单管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = menusBLL.BatchDelete(IdArray.IdArray,out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除菜单管理 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
