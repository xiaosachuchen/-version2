using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 字典管理 dics DAL 
	 /// </summary>
	 public static class dicsDAL
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
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_dics where 1=1 "; 
			 string sqlstr = "select * from base_dics where 1=1 "; 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
						 sqlcount = string.Format("{0} and dictype=@dictype and dictypename IS NULL", sqlcount);
						sqlstr = string.Format("{0} and dictype=@dictype and dictypename IS NULL", sqlstr);
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
				 }
			 }
			 sqlstr += " order by dicid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_dics>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_dics>(); 
		 }
        /// <summary>
        /// 验证字典
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="type"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_dics GetInfoVerify(string parameter, string type,string andsql, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            
            string sqlstr = string.Format("select dicid,{0} from base_dics  where {0}=@{0} ", type);
            sqlstr += andsql;
            
            pars.Add(new DataParameter(string.Format("{0}", type), parameter));
            return MySqlHelper.GetDataInfo<base_dics>(sqlstr, out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 字典管理 详情查询 
        /// </summary>
        public static base_dics GetInfo(int dicid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_dics  where dicid=@dicid "; 
			 pars.Add(new DataParameter("dicid", dicid)); 
			 return MySqlHelper.GetDataInfo<base_dics>(sqlstr, out errmsg,pars.ToArray()); 
		 }
      
         /// <summary>
         /// 字典管理 添加数据 
         /// </summary>
         /// <param name="base_dics">要添加的字典管理对象</param> 
         /// <param name="errmsg">错误信息</param>
         /// <returns>返回对象</returns>
        public static bool Add(base_dics info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("dicname", info.dicname));
			 pars.Add(new DataParameter("dicvalue", info.dicvalue));
			 pars.Add(new DataParameter("dictype", info.dictype));
			 pars.Add(new DataParameter("dictypename", info.dictypename));
			 pars.Add(new DataParameter("isdictype", info.isdictype));
			 pars.Add(new DataParameter("icon", info.icon));
			 pars.Add(new DataParameter("tag", info.tag));
			 pars.Add(new DataParameter("remarks", info.remarks));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 string sqlstr = "insert into base_dics (dicname,dicvalue,dictype,dictypename,isdictype,icon,tag,remarks,sorts) values (@dicname,@dicvalue,@dictype,@dictypename,@isdictype,@icon,@tag,@remarks,@sorts) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 字典管理 更新数据 
		 /// </summary>
		 /// <param name="base_dics">要更新的字典管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_dics info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("dicname", info.dicname));
			 pars.Add(new DataParameter("dicvalue", info.dicvalue));
			 pars.Add(new DataParameter("dictype", info.dictype));
			 pars.Add(new DataParameter("dictypename", info.dictypename));
			 pars.Add(new DataParameter("isdictype", info.isdictype));
			 pars.Add(new DataParameter("icon", info.icon));
			 pars.Add(new DataParameter("tag", info.tag));
			 pars.Add(new DataParameter("remarks", info.remarks));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 string sqlstr = "update base_dics set dicname=@dicname,dicvalue=@dicvalue,dictype=@dictype,dictypename=@dictypename,isdictype=@isdictype,icon=@icon,tag=@tag,remarks=@remarks,sorts=@sorts where dicid=@dicid "; 
			 pars.Add(new DataParameter("dicid",info.dicid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 字典管理 删除 
		 /// </summary>
		 public static bool Delete(int dicid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_dics where dicid=@dicid "; 
			 pars.Add(new DataParameter("dicid", dicid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

        /// <summary>
        /// 字典管理 删除字典类型 
        /// </summary>
        public static bool DeleteByDicType(string[] dictype,out string errmsg) 
		 {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "DELETE FROM base_dics where dictype=@dictype";
            pars.Add(new DataParameter("dictype", dictype[0]));
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

	 }
}
