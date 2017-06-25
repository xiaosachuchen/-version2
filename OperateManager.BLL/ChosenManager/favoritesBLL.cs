using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.ChosenManager; 

namespace Microsoft.RIPSP.BLL.ChosenManager 
{
	 /// <summary>
	 /// 收藏管理 favorites BLL 
	 /// </summary>
	 public static class favoritesBLL 
	 {
		 /// <summary>
		 /// 收藏管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_favorites> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<chosen_favorites> list = favoritesDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 收藏管理 详情查询 
		 /// </summary>
		 public static chosen_favorites GetInfo(int seqid, out string errmsg) 
		 {
			 chosen_favorites info = favoritesDAL.GetInfo(seqid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 收藏管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_favorites">要添加的收藏管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_favorites info,out string errmsg) 
		 {
			 return favoritesDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 收藏管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_favorites">要更新的收藏管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_favorites info,out string errmsg) 
		 {
			 return favoritesDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 收藏管理 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 return favoritesDAL.Delete(seqid, out errmsg); 
		 }

		 /// <summary>
		 /// 收藏管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return favoritesDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
