using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.DAL.BaseManager;
using OperateManager.Models.Resourcedb;

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 用户管理 users BLL 
	 /// </summary>
	 public static class usersBLL 
	 {
		 /// <summary>
		 /// 用户管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_users> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_users> list = usersDAL.GetPageList(queryarr,offset,limit,out total,out errmsg);

             return list;
		 }
        public static List<base_users> GetListInfo(List<string> queryarr,out string errmsg)
        {
             List<base_users> list = usersDAL.GetListInfo(queryarr,out errmsg);
            return list;
        }
         /// <summary>
         /// 用户管理 详情查询 
         /// </summary>
        public static base_users GetInfo(int userid, out string errmsg) 
		 {
			 base_users info = usersDAL.GetInfo(userid, out errmsg); 
			 return info;
		 }
		 /// <summary>
		 /// 用户管理 添加数据 
		 /// </summary>
		 /// <param name="base_users">要添加的用户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_users info,out string errmsg) 
		 {
			 return usersDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 用户管理 更新数据 
		 /// </summary>
		 /// <param name="base_users">要更新的用户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_users info,out string errmsg) 
		 {
			 return usersDAL.Update(info, out errmsg);
		 }
            public static bool UpdateCus(int? customerid,int? userid,out string errmsg)
            {
                return usersDAL.UpdateCus(customerid, userid,out errmsg);
            }
        /// <summary>
        /// 用户管理 删除 
        /// </summary>
        public static bool Delete(int userid,out string errmsg) 
		 {
			 return usersDAL.Delete(userid, out errmsg); 
		 }

		 /// <summary>
		 /// 用户管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return usersDAL.BatchDelete(idArray, out errmsg);
		 }
        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_users selectusername(string codes, string type, out string errmsg)
        {
            return usersDAL.selectusername(codes, type,out errmsg);
        }
        /// <summary>
        /// 总用户数（个人）
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUsersCount(out string errmsg)
        {
            return usersDAL.GetUsersCount(out errmsg);
        }
        /// <summary>
        /// 新注册用户(个人)
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUsersNewCount(string starttime,string endtime,out string errmsg)
        {
            return usersDAL.GetNewUsersCount(starttime, endtime, out errmsg);
        }
        /// <summary>
        /// 活跃用户数(个人)
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetActCount(string starttime, string endtime, out string errmsg)
        {
            return usersDAL.GetActCount(starttime, endtime, out errmsg);
        }
        /// <summary>
        /// 用户流失人数（个人）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUserLoss(string starttime, string endtime, out string errmsg)
        {
            return usersDAL.GetUserLoss(starttime, endtime, out errmsg);
        }
        /// <summary>
        /// 用户留存数（个人）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUserRete(string starttime, string endtime, out string errmsg)
        {
            return usersDAL.GetUserRete(starttime, endtime, out errmsg);
        }
        /// <summary>
        /// 获取机构相关列表
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stype"></param>
        /// <param name="areacode"></param>
        /// <param name="industry"></param>
        /// <param name="max_price"></param>
        /// <param name="min_price"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<OrgUsers> GetOrgUsersList(string starttime, string endtime, string stype, string areacode, string industry, string max_price, string min_price, int offset, int limit, out int total, out string errmsg)
        {
            List<OrgUsers> list = usersDAL.GetOrgUsersList(starttime, endtime, stype, areacode, industry, max_price, min_price, offset, limit, out total, out errmsg);
            return list;
        }
        /// <summary>
        /// 机构总用户（机构）
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUserOrgCount(out string errmsg)
        {
            return usersDAL.GetUserOrgCount(out errmsg);
        }
        /// <summary>
        /// 活跃用户数(机构)
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetOrgActCount(string starttime, string endtime, out string errmsg)
        {
            return usersDAL.GetOrgActCount(starttime, endtime, out errmsg);
        }
        public static List<base_users> GetUsersListofName(out string errmsg)
        {
            return usersDAL.GetUsersListofName(out errmsg);
        }
    }
}
