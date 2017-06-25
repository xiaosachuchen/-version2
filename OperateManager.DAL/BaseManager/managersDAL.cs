using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text;

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 管理员管理 managers DAL 
	 /// </summary>
	 public static class managersDAL 
	 {
		 /// <summary>
		 /// 管理员管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_managers> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlcount = new StringBuilder();
            StringBuilder sqlstr = new StringBuilder();

             sqlcount.Append("select count(1) from base_managers where 1=1 "); 
			  sqlstr.Append("select * from base_managers where 1=1 "); 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
						 sqlcount.AppendFormat("and {0} like '%{1}%'", query.name, query.value);
						sqlstr.AppendFormat("and {0} like '%{1}%'", query.name, query.value);
					 }
				 }
			 }
			 sqlstr.Append(" order by userid desc "); 
			 if (offset > -1)
			 {
				 sqlstr.Append(" limit " + offset + "," + limit);
				 total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_managers>(sqlstr.ToString(), out errmsg, pars.ToArray()); 
			 return new List<base_managers>(); 
		 }
       
        /// <summary>
        /// 管理员管理 详情查询 
        /// </summary>
        public static base_managers GetInfo(int userid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_managers  where userid=@userid "; 
			 pars.Add(new DataParameter("userid", userid)); 
			 return MySqlHelper.GetDataInfo<base_managers>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 管理员管理 添加数据 
		 /// </summary>
		 /// <param name="base_managers">要添加的管理员管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_managers info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("username", info.username));
			 pars.Add(new DataParameter("passwd", info.passwd));
			 pars.Add(new DataParameter("realname", info.realname));
			 pars.Add(new DataParameter("phone", info.phone));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("orgid", info.orgid));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into base_managers (username,passwd,realname,phone,email,creator,createdtime,orgid,status) values (@username,@passwd,@realname,@phone,@email,@creator,@createdtime,@orgid,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 管理员管理 更新数据 
		 /// </summary>
		 /// <param name="base_managers">要更新的管理员管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_managers info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("username", info.username));
			 pars.Add(new DataParameter("realname", info.realname));
			 pars.Add(new DataParameter("phone", info.phone));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("orgid", info.orgid));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "update base_managers set username=@username,realname=@realname,phone=@phone,email=@email,orgid=@orgid,status=@status where userid=@userid "; 
			 pars.Add(new DataParameter("userid",info.userid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }
        /// <summary>
        /// 管理员管理 更新数据机构id
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool UpdateOrg(int? orgid, int? userid, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("orgid", orgid));
            string sqlstr = "update base_managers set orgid=@orgid where userid=@userid ";
            pars.Add(new DataParameter("userid", userid));
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
        }
         /// <summary>
         /// 管理员管理 删除 
         /// </summary>
        public static bool Delete(int userid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_managers where userid=@userid "; 
			 pars.Add(new DataParameter("userid", userid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 管理员管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_managers where userid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_managers GetInfoVerify(string parameter,string type, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();

            string sqlstr = string.Format("select userid,{0} from base_managers  where {0}=@{0} ", type);
            pars.Add(new DataParameter(string.Format("{0}", type), parameter));
            return MySqlHelper.GetDataInfo<base_managers>(sqlstr, out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 查询配置资源
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<db_datalibrarys> GetDatalibraryslist(out string errmsg)
        {
            string sqlstr = "SELECT databaseid,databasename,databasecname FROM db_datalibrarys";
            return MySqlHelper.GetDataList<db_datalibrarys>(sqlstr, out errmsg);

        }
        /// <summary>
        /// 查询配置资源库
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_mgdb> Getmgdblist(int userid,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "SELECT * FROM base_mgdb WHERE userid=@userid ";
            pars.Add(new DataParameter("userid", userid));
            return MySqlHelper.GetDataList<base_mgdb>(sqlstr.ToString(), out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 管理员配置菜单编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idArray"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool AddMgdbmenu(int id, int[] idArray, out string errmsg)
        {
            bool result = false;
            List<DataParameter> pars = new List<DataParameter>();
            string sqldel = "DELETE FROM base_mgdb WHERE userid=@userid";
            pars.Add(new DataParameter("userid", id));
            result = MySqlHelper.ExecuteCommand(sqldel, out errmsg, pars) > 0;
            if (!result && !string.IsNullOrEmpty(errmsg))
                return false;
            StringBuilder strber = new StringBuilder();
            strber.Append("INSERT INTO base_mgdb (userid,databaseid) VALUES ");
            for (int i = 0; i < idArray.Length; i++)
            {
                strber.Append("(" + id + "," + idArray[i] + "),");
            }
            strber.Remove(strber.ToString().LastIndexOf(','), 1);
            return MySqlHelper.ExecuteCommand(strber.ToString(), out errmsg) > 0;
        }
        /// <summary>
        /// 查询角色配置资源
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_mgrole> Getmgdbrole(int userid,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "SELECT * FROM base_mgrole WHERE userid=@userid ";
            pars.Add(new DataParameter("userid", userid));
            return MySqlHelper.GetDataList<base_mgrole>(sqlstr.ToString(), out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 管理员配置角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idArray"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool AddMgdbroles(int id, IdArrayModel ArrayModel, out string errmsg)
        {
            bool result = false;
            List<DataParameter> pars = new List<DataParameter>();
            string sqldel = "DELETE FROM base_mgrole WHERE userid=@userid";
            pars.Add(new DataParameter("userid", id));
            result = MySqlHelper.ExecuteCommand(sqldel, out errmsg, pars) > 0;
            if (errmsg != null)
                return false;
            if (ArrayModel.IdArray[0]==0)
            {
                return true;
            }
                StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO base_mgrole (userid,roleid) VALUES ");
            for (int i = 0; i < ArrayModel.IdArray.Length; i++)
            {
                
                str.Append("(" + id + "," + ArrayModel.IdArray[i] + "),");
            }
            str.Remove(str.ToString().LastIndexOf(','), 1);
            return MySqlHelper.ExecuteCommand(str.ToString(), out errmsg) > 0;
        }

    }
}
