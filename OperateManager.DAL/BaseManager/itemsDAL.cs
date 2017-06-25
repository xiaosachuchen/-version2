using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 栏目管理 items DAL 
	 /// </summary>
	 public static class itemsDAL 
	 {
		 /// <summary>
		 /// 栏目管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_items> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_items where 1=1 "; 
			 string sqlstr = "select * from base_items where 1=1 "; 
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
			 sqlstr += " order by parentid asc,sorts asc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_items>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_items>(); 
		 }

		 /// <summary>
		 /// 栏目管理 详情查询 
		 /// </summary>
		 public static base_items GetInfo(int itemid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_items  where itemid=@itemid "; 
			 pars.Add(new DataParameter("itemid", itemid)); 
			 return MySqlHelper.GetDataInfo<base_items>(sqlstr, out errmsg,pars.ToArray()); 
		 }
        public static List<base_items> GetInfobyId(int itemid,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select s.itemid,s.parentid,s.itemmark FROM base_items s where s.itemid=@itemid UNION ALL ");
            sqlstr.Append("select s.itemid ,s.parentid,s.itemmark  FROM  base_items  s WHERE s.parentid=@itemid union ALL ");
            sqlstr.Append(" SELECT s.itemid ,s.parentid,s.itemmark  FROM base_items s JOIN (select * from  base_items s where s.parentid=@itemid) as b ON s.parentid=b.itemid");
            //string sqlstr = "select s.itemid,s.parentid FROM base_items s where s.itemid=@itemid UNION ALL ";
            pars.Add(new DataParameter("itemid", itemid));
           
            return MySqlHelper.GetDataList<base_items>(sqlstr.ToString(), out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 栏目管理 添加数据 
        /// </summary>
        /// <param name="base_items">要添加的栏目管理对象</param> 
        /// <param name="errmsg">错误信息</param>
        /// <returns>返回对象</returns>
        public static bool Add(base_items info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("parentid", info.parentid));
			 pars.Add(new DataParameter("itemname", info.itemname));
			 pars.Add(new DataParameter("itemmark", info.itemmark));
			 pars.Add(new DataParameter("navigation", info.navigation));
			 pars.Add(new DataParameter("sourcevalue", info.sourcevalue));
			 pars.Add(new DataParameter("sourcetype", info.sourcetype));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into base_items (parentid,itemname,itemmark,navigation,sourcevalue,sourcetype,sorts,status) values (@parentid,@itemname,@itemmark,@navigation,@sourcevalue,@sourcetype,@sorts,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目管理 更新数据 
		 /// </summary>
		 /// <param name="base_items">要更新的栏目管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_items info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("parentid", info.parentid));
			 pars.Add(new DataParameter("itemname", info.itemname));
			 pars.Add(new DataParameter("itemmark", info.itemmark));
			 pars.Add(new DataParameter("navigation", info.navigation));
			 pars.Add(new DataParameter("sourcevalue", info.sourcevalue));
			 pars.Add(new DataParameter("sourcetype", info.sourcetype));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "update base_items set parentid=@parentid,itemname=@itemname,itemmark=@itemmark,navigation=@navigation,sourcevalue=@sourcevalue,sourcetype=@sourcetype,sorts=@sorts,status=@status where itemid=@itemid "; 
			 pars.Add(new DataParameter("itemid",info.itemid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目管理 删除 
		 /// </summary>
		 public static bool Delete(int itemid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_items where itemid=@itemid "; 
			 pars.Add(new DataParameter("itemid", itemid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_items where itemid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }
        /// <summary>
        /// 检测栏目是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_items selectitemmark(string codes, string type, out string errmsg)
        {

            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from base_items  where " + type + "='" + codes + "'";
            
            return MySqlHelper.GetDataInfo<base_items>(sqlstr, out errmsg);
        }
    }
}
