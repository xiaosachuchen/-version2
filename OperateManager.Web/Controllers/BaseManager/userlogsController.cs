using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 用户行为日志管理 userlogs Controller 
	 /// </summary>
	 public class userlogsController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 用户行为日志管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<base_userlogs> list = userlogsBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 用户行为日志管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_userlogs info = userlogsBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 用户行为日志管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_userlogs info) 
		 {
			 bool success = userlogsBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 用户行为日志管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_userlogs info) 
		 {
			 info.logid = id; 	
			 bool success = userlogsBLL.Update(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 用户行为日志管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = userlogsBLL.Delete(id, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 用户行为日志管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = userlogsBLL.BatchDelete(IdArray.IdArray,out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
