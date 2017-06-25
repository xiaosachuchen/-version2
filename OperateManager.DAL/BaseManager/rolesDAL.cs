using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text;

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 角色管理 roles DAL 
	 /// </summary>
	 public static class rolesDAL 
	 {
		 /// <summary>
		 /// 角色管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_roles> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_roles where 1=1 "; 
			 string sqlstr = "select * from base_roles where 1=1 "; 
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
			 sqlstr += " order by roleid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_roles>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_roles>(); 
		 }

		 /// <summary>
		 /// 角色管理 详情查询 
		 /// </summary>
		 public static base_roles GetInfo(int roleid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_roles  where roleid=@roleid "; 
			 pars.Add(new DataParameter("roleid", roleid)); 
			 return MySqlHelper.GetDataInfo<base_roles>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 角色管理 添加数据 
		 /// </summary>
		 /// <param name="base_roles">要添加的角色管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_roles info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("rolename", info.rolename));
			 pars.Add(new DataParameter("roletype", info.roletype));
			 pars.Add(new DataParameter("description", info.description));
			 string sqlstr = "insert into base_roles (rolename,roletype,description) values (@rolename,@roletype,@description) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 角色管理 更新数据 
		 /// </summary>
		 /// <param name="base_roles">要更新的角色管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_roles info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("rolename", info.rolename));
			 pars.Add(new DataParameter("roletype", info.roletype));
			 pars.Add(new DataParameter("description", info.description));
			 string sqlstr = "update base_roles set rolename=@rolename,roletype=@roletype,description=@description where roleid=@roleid "; 
			 pars.Add(new DataParameter("roleid",info.roleid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 角色管理 删除 
		 /// </summary>
		 public static bool Delete(int roleid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_roles where roleid=@roleid ";
            string sqlrolsmenu = "DELETE FROM base_rolemenu WHERE roleid=@roleid";
			 pars.Add(new DataParameter("roleid", roleid));
            bool result = MySqlHelper.ExecuteCommand(sqlrolsmenu, out errmsg, pars) > 0;
            if (!result && !string.IsNullOrEmpty(errmsg))
                return false;
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 角色管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_roles where roleid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }
        /// <summary>
        /// 角色菜单配权管理 添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idArray"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool AddRoleMenu(int id, int[] idArray, out string errmsg)
        {
            bool result = false;
            List<DataParameter> pars = new List<DataParameter>();
            string sqldel = "DELETE FROM base_rolemenu WHERE roleid=@roleid";
            pars.Add(new DataParameter("roleid", id));
            result = MySqlHelper.ExecuteCommand(sqldel, out errmsg, pars) > 0;
            if (!result && !string.IsNullOrEmpty(errmsg))
                return false;
            StringBuilder strber = new StringBuilder();
            strber.Append("INSERT INTO base_rolemenu (roleid,menuid) VALUES ");
            for (int i = 0; i < idArray.Length; i++)
            {
                strber.Append("(" + id + "," + idArray[i] + "),");
            }
            strber.Remove(strber.ToString().LastIndexOf(','), 1);
            return MySqlHelper.ExecuteCommand(strber.ToString(), out errmsg) > 0;
        }
        /// <summary>
        /// 管理员配置角色获取角色
        /// </summary>
        /// <returns></returns>
        public static List<base_roles> GetRoles()
        {
            string msg = null;
            string sql = "SELECT roleid,rolename FROM base_roles";
            return MySqlHelper.GetDataList<base_roles>(sql, out msg);
        }

    }
}
