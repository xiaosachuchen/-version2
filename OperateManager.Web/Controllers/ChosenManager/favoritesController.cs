using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.ChosenManager; 

namespace Microsoft.RIPSP.Controller.ChosenManager 
{
	 /// <summary>
	 /// 收藏管理 favorites Controller 
	 /// </summary>
	 public class favoritesController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 收藏管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<chosen_favorites> list = favoritesBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 收藏管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 chosen_favorites info = favoritesBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 收藏管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] chosen_favorites info) 
		 {
			 bool success = favoritesBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 收藏管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] chosen_favorites info) 
		 {
			 info.seqid = id; 	
			 bool success = favoritesBLL.Update(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 收藏管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = favoritesBLL.Delete(id, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 收藏管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = favoritesBLL.BatchDelete(IdArray.IdArray,out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
