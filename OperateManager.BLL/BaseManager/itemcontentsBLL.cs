using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 栏目内容管理 itemcontents BLL 
	 /// </summary>
	 public static class itemcontentsBLL 
	 {
		 /// <summary>
		 /// 栏目内容管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_itemcontents> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_itemcontents> list = itemcontentsDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 栏目内容管理 详情查询 
		 /// </summary>
		 public static base_itemcontents GetInfo(int seqid, out string errmsg) 
		 {
			 base_itemcontents info = itemcontentsDAL.GetInfo(seqid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 栏目内容管理 添加数据 
		 /// </summary>
		 /// <param name="base_itemcontents">要添加的栏目内容管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_itemcontents info,out string errmsg) 
		 {
			 return itemcontentsDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 栏目内容管理 更新数据 
		 /// </summary>
		 /// <param name="base_itemcontents">要更新的栏目内容管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_itemcontents info,out string errmsg) 
		 {
			 return itemcontentsDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 栏目内容管理 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 return itemcontentsDAL.Delete(seqid, out errmsg); 
		 }

		 /// <summary>
		 /// 栏目内容管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return itemcontentsDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
