using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 分类管理 classes Controller 
	 /// </summary>
	 public class classesController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 分类管理 列表查询 
		 /// </summary>
		 public object Get()
		 {
			 List<base_classes> list = classesBLL.GetPageList(out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            return new { rows = list};
		 }

		 /// <summary>
		 /// 分类管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_classes info = classesBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 分类管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_classes info) 
		 {
             if(string.IsNullOrEmpty(info.parentclassid.ToString()))
            {
                info.parentclassid = 0;
            }
			 bool success = classesBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加分类{0},返回{1} {2}", info.classname,success,errmsg));
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_ClassList");
            return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 分类管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_classes info) 
		 {
			 info.classid = id; 	
			 bool success = classesBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新分类{0},返回{1} {2}", info.classname, success, errmsg)); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_ClassList");
            return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 分类管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = classesBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除分类 id为{0},返回{1} {2}",id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_ClassList");
            return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 分类管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = classesBLL.BatchDelete(IdArray.IdArray,out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除分类 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_ClassList");
            return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
