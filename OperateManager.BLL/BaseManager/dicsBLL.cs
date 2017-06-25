using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 字典管理 dics BLL 
	 /// </summary>
	 public static class dicsBLL 
	 {
		 /// <summary>
		 /// 字典管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_dics> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_dics> list = dicsDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">字典值</param>
        /// <param name="type"></param>
        /// <param name="dictype">字典类型</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_dics GetInfoVerify(string parameter, string type,string andsql, out string errmsg)
        {
            return dicsDAL.GetInfoVerify(parameter, type, andsql, out errmsg);
        }
        /// <summary>
        /// 字典管理 详情查询 
        /// </summary>
        public static base_dics GetInfo(int dicid, out string errmsg) 
		 {
			 base_dics info = dicsDAL.GetInfo(dicid, out errmsg); 
			 return info;
		 }
     
         /// <summary>
         /// 字典管理 添加数据 
         /// </summary>
         /// <param name="base_dics">要添加的字典管理对象</param> 
         /// <param name="errmsg">错误信息</param>
         /// <returns>返回对象</returns>
        public static bool Add(base_dics info,out string errmsg) 
		 {
			 return dicsDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 字典管理 更新数据 
		 /// </summary>
		 /// <param name="base_dics">要更新的字典管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_dics info,out string errmsg) 
		 {
			 return dicsDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 字典管理 删除 
		 /// </summary>
		 public static bool Delete(int dicid,out string errmsg) 
		 {
			 return dicsDAL.Delete(dicid, out errmsg); 
		 }

		 /// <summary>
		 /// 字典管理 删除字典类型
		 /// </summary>
		 public static bool DeleteByDicType(string[] dictype, out string errmsg) 
		 {
			 return dicsDAL.DeleteByDicType(dictype, out errmsg);
		 }

	 }
}
