using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 菜单管理 menus DAL 
	 /// </summary>
	 public static class menusDAL 
	 {
		 /// <summary>
		 /// 菜单管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_menus> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_menus where status=1 "; 
			 string sqlstr = "select * from base_menus where status=1 "; 
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
			 sqlstr += " order by parentmenuid asc,sorts asc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_menus>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_menus>(); 
		 }

		 /// <summary>
		 /// 菜单管理 详情查询 
		 /// </summary>
		 public static base_menus GetInfo(int menuid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_menus  where menuid=@menuid "; 
			 pars.Add(new DataParameter("menuid", menuid)); 
			 return MySqlHelper.GetDataInfo<base_menus>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 菜单管理 添加数据 
		 /// </summary>
		 /// <param name="base_menus">要添加的菜单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_menus info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("menuname", info.menuname));
			 pars.Add(new DataParameter("parentmenuid", info.parentmenuid));
			 pars.Add(new DataParameter("pagemark", info.pagemark));
			 pars.Add(new DataParameter("path", info.path));
			 pars.Add(new DataParameter("description", info.description));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("isoutlink", info.isoutlink));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into base_menus (menuname,parentmenuid,pagemark,path,description,sorts,isoutlink,status) values (@menuname,@parentmenuid,@pagemark,@path,@description,@sorts,@isoutlink,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 菜单管理 更新数据 
		 /// </summary>
		 /// <param name="base_menus">要更新的菜单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_menus info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("menuname", info.menuname));
			 pars.Add(new DataParameter("parentmenuid", info.parentmenuid));
			 pars.Add(new DataParameter("pagemark", info.pagemark));
			 pars.Add(new DataParameter("path", info.path));
			 pars.Add(new DataParameter("description", info.description));
			 pars.Add(new DataParameter("isoutlink", info.isoutlink));
            pars.Add(new DataParameter("sorts", info.sorts));
            pars.Add(new DataParameter("status", info.status));
            string sqlstr = "update base_menus set menuname=@menuname,parentmenuid=@parentmenuid,pagemark=@pagemark,path=@path,description=@description,isoutlink=@isoutlink,sorts=@sorts,status=@status where menuid=@menuid "; 
			 pars.Add(new DataParameter("menuid",info.menuid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 菜单管理 删除 
		 /// </summary>
		 public static bool Delete(int menuid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_menus where menuid=@menuid "; 
			 pars.Add(new DataParameter("menuid", menuid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 菜单管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_menus where menuid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
