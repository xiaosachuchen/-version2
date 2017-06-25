using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 客户管理 customers DAL 
	 /// </summary>
	 public static class customersDAL 
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
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_customers where 1=1 "; 
			 string sqlstr = "select * from base_customers where 1=1 "; 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
						 sqlcount = string.Format("{0} and {1} {2} @{1}", sqlcount, query.name, query.exp);
						sqlstr = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
				 }
			 }
			 sqlstr += " order by customerid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_customers>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_customers>(); 
		 }
        /// <summary>
        /// 客户管理 列表客户名称查询 
        /// </summary>
        /// <param name="listarr"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_customers> GetInfoList(List<string> listarr,out string errmsg)
        {
            errmsg = null;
            string idstr = string.Join(",", listarr);
            string sqlstr = "select customerid,customername from base_customers where 1=1  and customerid in(" + idstr + ")";
            return MySqlHelper.GetDataList<base_customers>(sqlstr, out errmsg);
        }
		 /// <summary>
		 /// 客户管理 详情查询 
		 /// </summary>
		 public static base_customers GetInfo(int customerid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_customers  where customerid=@customerid "; 
			 pars.Add(new DataParameter("customerid", customerid)); 
			 return MySqlHelper.GetDataInfo<base_customers>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 客户管理 添加数据 
		 /// </summary>
		 /// <param name="base_customers">要添加的客户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_customers info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("customername", info.customername));
			 pars.Add(new DataParameter("contacts", info.contacts));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("phone", info.phone));
			 pars.Add(new DataParameter("fax", info.fax));
			 pars.Add(new DataParameter("country", info.country));
			 pars.Add(new DataParameter("areacode", info.areacode));
			 pars.Add(new DataParameter("postcode", info.postcode));
			 pars.Add(new DataParameter("industry", info.industry));
			 pars.Add(new DataParameter("website", info.website));
			 pars.Add(new DataParameter("logourl", info.logourl));
			 pars.Add(new DataParameter("introduction", info.introduction));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "insert into base_customers (customername,contacts,email,phone,fax,country,areacode,postcode,industry,website,logourl,introduction,creator,createdtime) values (@customername,@contacts,@email,@phone,@fax,@country,@areacode,@postcode,@industry,@website,@logourl,@introduction,@creator,@createdtime) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 客户管理 更新数据 
		 /// </summary>
		 /// <param name="base_customers">要更新的客户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_customers info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("customername", info.customername));
			 pars.Add(new DataParameter("contacts", info.contacts));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("phone", info.phone));
			 pars.Add(new DataParameter("fax", info.fax));
			 pars.Add(new DataParameter("country", info.country));
			 pars.Add(new DataParameter("areacode", info.areacode));
			 pars.Add(new DataParameter("postcode", info.postcode));
			 pars.Add(new DataParameter("industry", info.industry));
			 pars.Add(new DataParameter("website", info.website));
			 pars.Add(new DataParameter("logourl", info.logourl));
			 pars.Add(new DataParameter("introduction", info.introduction));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "update base_customers set customername=@customername,contacts=@contacts,email=@email,phone=@phone,fax=@fax,country=@country,areacode=@areacode,postcode=@postcode,industry=@industry,website=@website,logourl=@logourl,introduction=@introduction,creator=@creator,createdtime=@createdtime where customerid=@customerid "; 
			 pars.Add(new DataParameter("customerid",info.customerid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 客户管理 删除 
		 /// </summary>
		 public static bool Delete(int customerid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_customers where customerid=@customerid "; 
			 pars.Add(new DataParameter("customerid", customerid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 客户管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_customers where customerid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }
        /// <summary>
        /// 检测数据是否重复
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_customers selectcusername(string codes, string type, out string errmsg)
        {

            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from base_customers  where " + type + "='" + codes + "'";
            return MySqlHelper.GetDataInfo<base_customers>(sqlstr, out errmsg);
        }

    }
}
