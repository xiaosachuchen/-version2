using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text;

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 栏目内容管理 itemcontents DAL 
	 /// </summary>
	 public static class itemcontentsDAL 
	 {
		 /// <summary>
		 /// 栏目内容管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_itemcontents> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlcount = new StringBuilder();
            StringBuilder sqlstr = new StringBuilder();
            sqlcount.Append("select count(1) from base_itemcontents where 1=1 "); 
			sqlstr.Append("select * from base_itemcontents where 1=1"); 
			 if (queryarr.Count > 0)
			 {
                sqlcount.Append(" AND itemmark  in(");
                sqlstr.Append(" AND itemmark  in(");
                foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
                        
						 sqlcount.AppendFormat( "'{0}',",query.value);
                        sqlstr.AppendFormat("'{0}',", query.value);
					 }
				 }
                sqlcount.Remove(sqlcount.ToString().LastIndexOf(','), 1);
                sqlstr.Remove(sqlstr.ToString().LastIndexOf(','), 1);
                sqlcount.Append(")");
                sqlstr.Append(") order by seqid desc ");
            }
           
			 if (offset > -1)
			 {
				 sqlstr.Append(" limit " + offset + "," + limit);
				 total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_itemcontents>(sqlstr.ToString(), out errmsg, pars.ToArray()); 
			 return new List<base_itemcontents>(); 
		 }

		 /// <summary>
		 /// 栏目内容管理 详情查询 
		 /// </summary>
		 public static base_itemcontents GetInfo(int seqid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_itemcontents  where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.GetDataInfo<base_itemcontents>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 栏目内容管理 添加数据 
		 /// </summary>
		 /// <param name="base_itemcontents">要添加的栏目内容管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_itemcontents info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("itemmark", info.itemmark));
			 pars.Add(new DataParameter("title", info.title));
			 pars.Add(new DataParameter("keywords", info.keywords));
			 pars.Add(new DataParameter("abstracts", info.abstracts));
			 pars.Add(new DataParameter("thumbnail", info.thumbnail));
			 pars.Add(new DataParameter("classname", info.classname));
			 pars.Add(new DataParameter("author", info.author));
			 pars.Add(new DataParameter("aboutdate", info.aboutdate));
			 pars.Add(new DataParameter("linkurl", info.linkurl));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("status", info.status));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 string sqlstr = "insert into base_itemcontents (itemmark,title,keywords,abstracts,thumbnail,classname,author,aboutdate,linkurl,sorts,createdtime,creator,status,restype,rescode) values (@itemmark,@title,@keywords,@abstracts,@thumbnail,@classname,@author,@aboutdate,@linkurl,@sorts,@createdtime,@creator,@status,@restype,@rescode) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目内容管理 更新数据 
		 /// </summary>
		 /// <param name="base_itemcontents">要更新的栏目内容管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_itemcontents info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("itemmark", info.itemmark));
			 pars.Add(new DataParameter("title", info.title));
			 pars.Add(new DataParameter("keywords", info.keywords));
			 pars.Add(new DataParameter("abstracts", info.abstracts));
			 pars.Add(new DataParameter("thumbnail", info.thumbnail));
			 pars.Add(new DataParameter("classname", info.classname));
			 pars.Add(new DataParameter("author", info.author));
			 pars.Add(new DataParameter("aboutdate", info.aboutdate));
			 pars.Add(new DataParameter("linkurl", info.linkurl));
			 pars.Add(new DataParameter("sorts", info.sorts));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("status", info.status));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 string sqlstr = "update base_itemcontents set itemmark=@itemmark,title=@title,keywords=@keywords,abstracts=@abstracts,thumbnail=@thumbnail,classname=@classname,author=@author,aboutdate=@aboutdate,linkurl=@linkurl,sorts=@sorts,createdtime=@createdtime,creator=@creator,status=@status,restype=@restype,rescode=@rescode where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid",info.seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目内容管理 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_itemcontents where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 栏目内容管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_itemcontents where seqid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
