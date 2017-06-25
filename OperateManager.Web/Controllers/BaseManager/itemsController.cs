using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 栏目管理 items Controller 
	 /// </summary>
	 public class itemsController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 栏目管理 列表查询 
		 /// </summary>
		 public object Get()
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<base_items> list = itemsBLL.GetPageList(out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            return new { rows = list};
		 }
        /// <summary>
        ///检测栏目标识是否存在 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public object Get(string codes, string type,string flag,string itemid)
        {
            if (!string.IsNullOrEmpty(type)) {
                base_items info = itemsBLL.selectitemmark(codes, type, out errmsg);
                if (string.Equals("编辑", flag)) {
                    if (info != null) { 
                    return new { valid = (string.Equals(info.itemid.ToString(), itemid)) ? true : false };
                    }
                    return new { valid = true };
                }

                return new { valid =(info==null)?true:false };    
            }
            return new { valid = false };
        }
        /// <summary>
        /// 栏目管理 详情查询 
        /// </summary>
        public object Get(int id) 
		 {
			 base_items info = itemsBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 栏目管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_items info) 
		 {
            if (info.parentid==null||info.parentid==-1)
            {
                info.parentid = 0;
            }
			 bool success = itemsBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加栏目标识为{0}，返回{1} {2}", info.itemname, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 栏目管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_items info) 
		 {
			 info.itemid = id; 	
			 bool success = itemsBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新客户id为{0}，返回{1} {2}",id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 栏目管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = itemsBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除栏目id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 栏目管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = itemsBLL.BatchDelete(IdArray.IdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除栏目id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
