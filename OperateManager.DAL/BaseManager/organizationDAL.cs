using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 机构管理 organization DAL 
	 /// </summary>
	 public static class organizationDAL 
	 {
		 /// <summary>
		 /// 机构管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_organization> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_organization where 1=1 "; 
			 string sqlstr = "select * from base_organization where 1=1 "; 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
						 sqlcount = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
						sqlstr = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
				 }
			 }
			 sqlstr += " order by orgid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_organization>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_organization>(); 
		 }

		 /// <summary>
		 /// 机构管理 详情查询 
		 /// </summary>
		 public static base_organization GetInfo(int orgid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_organization  where orgid=@orgid "; 
			 pars.Add(new DataParameter("orgid", orgid)); 
			 return MySqlHelper.GetDataInfo<base_organization>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 机构管理 添加数据 
		 /// </summary>
		 /// <param name="base_organization">要添加的机构管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_organization info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("orgname", info.orgname));
			 pars.Add(new DataParameter("industry", info.industry));
			 pars.Add(new DataParameter("logo", info.logo));
			 pars.Add(new DataParameter("propaganda", info.propaganda));
			 pars.Add(new DataParameter("country", info.country));
			 pars.Add(new DataParameter("areacode", info.areacode));
			 pars.Add(new DataParameter("address", info.address));
			 pars.Add(new DataParameter("contacts", info.contacts));
			 pars.Add(new DataParameter("telphone", info.telphone));
			 pars.Add(new DataParameter("fax", info.fax));
			 pars.Add(new DataParameter("website", info.website));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("postcode", info.postcode));
			 pars.Add(new DataParameter("createddate", info.createddate));
			 pars.Add(new DataParameter("corporate", info.corporate));
			 pars.Add(new DataParameter("subjectclass", info.subjectclass));
			 pars.Add(new DataParameter("introduce", info.introduce));
			 pars.Add(new DataParameter("hits", info.hits));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into base_organization (orgname,industry,logo,propaganda,country,areacode,address,contacts,telphone,fax,website,email,postcode,createddate,corporate,subjectclass,introduce,hits,createdtime,creator,status) values (@orgname,@industry,@logo,@propaganda,@country,@areacode,@address,@contacts,@telphone,@fax,@website,@email,@postcode,@createddate,@corporate,@subjectclass,@introduce,@hits,@createdtime,@creator,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 机构管理 更新数据 
		 /// </summary>
		 /// <param name="base_organization">要更新的机构管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_organization info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("orgname", info.orgname));
			 pars.Add(new DataParameter("industry", info.industry));
			 pars.Add(new DataParameter("logo", info.logo));
			 pars.Add(new DataParameter("propaganda", info.propaganda));
			 pars.Add(new DataParameter("country", info.country));
			 pars.Add(new DataParameter("areacode", info.areacode));
			 pars.Add(new DataParameter("address", info.address));
			 pars.Add(new DataParameter("contacts", info.contacts));
			 pars.Add(new DataParameter("telphone", info.telphone));
			 pars.Add(new DataParameter("fax", info.fax));
			 pars.Add(new DataParameter("website", info.website));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("postcode", info.postcode));
			 pars.Add(new DataParameter("createddate", info.createddate));
			 pars.Add(new DataParameter("corporate", info.corporate));
			 pars.Add(new DataParameter("subjectclass", info.subjectclass));
			 pars.Add(new DataParameter("introduce", info.introduce));
			 pars.Add(new DataParameter("hits", info.hits));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "update base_organization set orgname=@orgname,industry=@industry,logo=@logo,propaganda=@propaganda,country=@country,areacode=@areacode,address=@address,contacts=@contacts,telphone=@telphone,fax=@fax,website=@website,email=@email,postcode=@postcode,createddate=@createddate,corporate=@corporate,subjectclass=@subjectclass,introduce=@introduce,hits=@hits,createdtime=@createdtime,creator=@creator,status=@status where orgid=@orgid "; 
			 pars.Add(new DataParameter("orgid",info.orgid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 机构管理 删除 
		 /// </summary>
		 public static bool Delete(int orgid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_organization where orgid=@orgid "; 
			 pars.Add(new DataParameter("orgid", orgid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 机构管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_organization where orgid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }
        /// <summary>
        /// 检测数据是否重复
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_organization selectbyType(string codes, string type, out string errmsg)
        {

            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from base_organization  where " + type + "='" + codes + "'";
            return MySqlHelper.GetDataInfo<base_organization>(sqlstr, out errmsg);
        }
        
    }
}
