using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 客户管理 customers BLL 
	 /// </summary>
	 public static class customersBLL 
	 {
		 /// <summary>
		 /// 客户管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_customers> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_customers> list = customersDAL.GetPageList(queryarr,offset,limit,out total,out errmsg);
            foreach (base_customers customers in list) {
                customers.areacodename = BaseBLL.GlobalCommonBLL.GetDicName("Areacode", customers.areacode.ToString());
                customers.countryname = BaseBLL.GlobalCommonBLL.GetDicName("Country", customers.country.ToString());
                customers.industryname = BaseBLL.GlobalCommonBLL.GetDicName("Industry", customers.industry.ToString());

            }
			 return list;
		 }
        /// <summary>
        /// 客户管理 列表客户名称查询 
        /// </summary>
        /// <param name="listarr"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_customers> GetInfoList(List<string> listarr,out string errmsg)
        {
            List<base_customers> list = customersDAL.GetInfoList(listarr,out errmsg);
            return list;

        }
		 /// <summary>
		 /// 客户管理 详情查询 
		 /// </summary>
		 public static base_customers GetInfo(int customerid, out string errmsg) 
		 {
			 base_customers info = customersDAL.GetInfo(customerid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 客户管理 添加数据 
		 /// </summary>
		 /// <param name="base_customers">要添加的客户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_customers info,out string errmsg) 
		 {
			 return customersDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 客户管理 更新数据 
		 /// </summary>
		 /// <param name="base_customers">要更新的客户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_customers info,out string errmsg) 
		 {
			 return customersDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 客户管理 删除 
		 /// </summary>
		 public static bool Delete(int customerid,out string errmsg) 
		 {
			 return customersDAL.Delete(customerid, out errmsg); 
		 }

		 /// <summary>
		 /// 客户管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return customersDAL.BatchDelete(idArray, out errmsg);
		 }
        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_customers selectcusername(string codes, string type, out string errmsg)
        {
            return customersDAL.selectcusername(codes, type, out errmsg);
        }
    }
}
