using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 栏目模板管理 itemtemplate Controller 
	 /// </summary>
	 public class itemtemplateController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 栏目模板管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<base_itemtemplate> list = itemtemplateBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 栏目模板管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_itemtemplate info = itemtemplateBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 栏目模板管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_itemtemplate info) 
		 {
			 bool success = itemtemplateBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加标题为{0}的栏目模板，返回{1} {2}", info.templatename, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 栏目模板管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_itemtemplate info) 
		 {
			 info.templateid = id; 	
			 bool success = itemtemplateBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新栏目模板id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 栏目模板管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = itemtemplateBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除客栏目模板id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 栏目模板管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = itemtemplateBLL.BatchDelete(IdArray.IdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除栏目模板id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
