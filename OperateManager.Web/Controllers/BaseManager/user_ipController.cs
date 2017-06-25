using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 用户认证ip管理 user_ip Controller 
	 /// </summary>
	 public class user_ipController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 用户认证ip管理 列表查询 
		 /// </summary>
		 public object Get(int userid, int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<base_user_ip> list = user_ipBLL.GetPageList(userid,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 用户认证ip管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_user_ip info = user_ipBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 用户认证ip管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_user_ip info) 
		 {
			 bool success = user_ipBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 用户认证ip管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_user_ip info) 
		 {
			 info.seqid = id; 	
			 bool success = user_ipBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新用户认证ip管理 id为 {0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 用户认证ip管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = user_ipBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除用户认证ip管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 用户认证ip管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = user_ipBLL.BatchDelete(IdArray.IdArray,out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除用户认证ip管理 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
