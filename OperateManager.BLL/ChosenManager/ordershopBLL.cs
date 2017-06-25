using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.ChosenManager; 

namespace Microsoft.RIPSP.BLL.ChosenManager 
{
	 /// <summary>
	 /// 订单商品管理 ordershop BLL 
	 /// </summary>
	 public static class ordershopBLL 
	 {
		 /// <summary>
		 /// 订单商品管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_ordershop> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<chosen_ordershop> list = ordershopDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 订单商品管理 详情查询 
		 /// </summary>
		 public static chosen_ordershop GetInfo(int orderid, out string errmsg) 
		 {
			 chosen_ordershop info = ordershopDAL.GetInfo(orderid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 订单商品管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_ordershop">要添加的订单商品管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_ordershop info,out string errmsg) 
		 {
			 return ordershopDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 订单商品管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_ordershop">要更新的订单商品管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_ordershop info,out string errmsg) 
		 {
			 return ordershopDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 订单商品管理 删除 
		 /// </summary>
		 public static bool Delete(int orderid,out string errmsg) 
		 {
			 return ordershopDAL.Delete(orderid, out errmsg); 
		 }

		 /// <summary>
		 /// 订单商品管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return ordershopDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
