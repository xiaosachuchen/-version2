using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.ChosenManager; 

namespace Microsoft.RIPSP.BLL.ChosenManager 
{
	 /// <summary>
	 /// 服务内容表 servicecont BLL 
	 /// </summary>
	 public static class servicecontBLL 
	 {
		 /// <summary>
		 /// 服务内容表 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_servicecont> GetPageList(string serviceno, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<chosen_servicecont> list = servicecontDAL.GetPageList(serviceno, offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 服务内容表 详情查询 
		 /// </summary>
		 public static chosen_servicecont GetInfo(int seqid, out string errmsg) 
		 {
			 chosen_servicecont info = servicecontDAL.GetInfo(seqid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 服务内容表 添加数据 
		 /// </summary>
		 /// <param name="chosen_servicecont">要添加的服务内容表对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_servicecont info,out string errmsg) 
		 {
			 return servicecontDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 服务内容表 更新数据 
		 /// </summary>
		 /// <param name="chosen_servicecont">要更新的服务内容表对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_servicecont info,out string errmsg) 
		 {
			 return servicecontDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 服务内容表 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 return servicecontDAL.Delete(seqid, out errmsg); 
		 }

		 /// <summary>
		 /// 服务内容表 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return servicecontDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
