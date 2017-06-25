using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 分类管理 classes DAL 
	 /// </summary>
	 public static class classesDAL 
	 {
		 /// <summary>
		 /// 分类管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_classes> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_classes where 1=1 "; 
			 string sqlstr = "select * from base_classes where 1=1 "; 
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
			 sqlstr += " order by classid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_classes>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_classes>(); 
		 }

		 /// <summary>
		 /// 分类管理 详情查询 
		 /// </summary>
		 public static base_classes GetInfo(int classid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_classes  where classid=@classid "; 
			 pars.Add(new DataParameter("classid", classid)); 
			 return MySqlHelper.GetDataInfo<base_classes>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 分类管理 添加数据 
		 /// </summary>
		 /// <param name="base_classes">要添加的分类管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_classes info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("classname", info.classname));
			 pars.Add(new DataParameter("parentclassid", info.parentclassid));
			 pars.Add(new DataParameter("remarks", info.remarks));
			 pars.Add(new DataParameter("icon", info.icon));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("tag", info.tag));
			 string sqlstr = "insert into base_classes (classname,parentclassid,remarks,icon,sorts,tag) values (@classname,@parentclassid,@remarks,@icon,@sorts,@tag) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 分类管理 更新数据 
		 /// </summary>
		 /// <param name="base_classes">要更新的分类管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_classes info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("classname", info.classname));
			 pars.Add(new DataParameter("parentclassid", info.parentclassid));
			 pars.Add(new DataParameter("remarks", info.remarks));
			 pars.Add(new DataParameter("icon", info.icon));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("tag", info.tag));
			 string sqlstr = "update base_classes set classname=@classname,parentclassid=@parentclassid,remarks=@remarks,icon=@icon,sorts=@sorts,tag=@tag where classid=@classid "; 
			 pars.Add(new DataParameter("classid",info.classid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 分类管理 删除 
		 /// </summary>
		 public static bool Delete(int classid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_classes where classid=@classid "; 
			 pars.Add(new DataParameter("classid", classid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 分类管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_classes where classid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
