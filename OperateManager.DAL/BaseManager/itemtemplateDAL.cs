using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 栏目模板管理 itemtemplate DAL 
	 /// </summary>
	 public static class itemtemplateDAL 
	 {
		 /// <summary>
		 /// 栏目模板管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_itemtemplate> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_itemtemplate where 1=1 "; 
			 string sqlstr = "select * from base_itemtemplate where 1=1 "; 
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
			 sqlstr += " order by templateid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_itemtemplate>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_itemtemplate>(); 
		 }

		 /// <summary>
		 /// 栏目模板管理 详情查询 
		 /// </summary>
		 public static base_itemtemplate GetInfo(int templateid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_itemtemplate  where templateid=@templateid "; 
			 pars.Add(new DataParameter("templateid", templateid)); 
			 return MySqlHelper.GetDataInfo<base_itemtemplate>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 栏目模板管理 添加数据 
		 /// </summary>
		 /// <param name="base_itemtemplate">要添加的栏目模板管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_itemtemplate info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("themesid", info.themesid));
			 pars.Add(new DataParameter("templatename", info.templatename));
			 pars.Add(new DataParameter("itemmark", info.itemmark));
			 pars.Add(new DataParameter("templatetype", info.templatetype));
			 pars.Add(new DataParameter("fileurl", info.fileurl));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into base_itemtemplate (themesid,templatename,itemmark,templatetype,fileurl,status) values (@themesid,@templatename,@itemmark,@templatetype,@fileurl,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目模板管理 更新数据 
		 /// </summary>
		 /// <param name="base_itemtemplate">要更新的栏目模板管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_itemtemplate info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("themesid", info.themesid));
			 pars.Add(new DataParameter("templatename", info.templatename));
			 pars.Add(new DataParameter("itemmark", info.itemmark));
			 pars.Add(new DataParameter("templatetype", info.templatetype));
			 pars.Add(new DataParameter("fileurl", info.fileurl));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "update base_itemtemplate set themesid=@themesid,templatename=@templatename,itemmark=@itemmark,templatetype=@templatetype,fileurl=@fileurl,status=@status where templateid=@templateid "; 
			 pars.Add(new DataParameter("templateid",info.templateid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目模板管理 删除 
		 /// </summary>
		 public static bool Delete(int templateid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_itemtemplate where templateid=@templateid "; 
			 pars.Add(new DataParameter("templateid", templateid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目模板管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_itemtemplate where templateid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
