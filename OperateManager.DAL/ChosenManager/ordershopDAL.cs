using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text;

namespace Microsoft.RIPSP.DAL.ChosenManager 
{
	 /// <summary>
	 /// 订单商品管理 ordershop DAL 
	 /// </summary>
	 public static class ordershopDAL 
	 {
		 /// <summary>
		 /// 订单商品管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_ordershop> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlcount = new StringBuilder();
            StringBuilder sqlstr = new StringBuilder();
             sqlcount.Append("select count(1) from chosen_ordershop where 1=1 "); 
			  sqlstr.Append("select * from chosen_ordershop where 1=1 "); 
			 if (queryarr.Count > 0)
			 {
                foreach (QueryModel query in queryarr)
				 {
                    if (!string.IsNullOrEmpty(query.exp))
                    {
                        if (string.Equals(query.exp, "商品名称"))
                        {
                            sqlcount.AppendFormat(" and shopname LIKE '%{0}%'",query.value);
                            sqlstr.AppendFormat(" and shopname LIKE '%{0}%'", query.value);
                            //pars.Add(new DataParameter("shopname", query.value));
                        }
                        if (string.Equals(query.exp, "订单号"))
                        {
                            sqlcount.Append(" and orderno =@orderno");
                            sqlstr.Append(" and orderno =@orderno");
                            pars.Add(new DataParameter("orderno", query.value));
                        }
                    }
                }
			 }
			 sqlstr.Append(" order by orderid desc "); 
			 if (offset > -1)
			 {
                sqlstr.Append(" limit " + offset + "," + limit);
				 total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<chosen_ordershop>(sqlstr.ToString(), out errmsg, pars.ToArray());
             return MySqlHelper.GetDataList<chosen_ordershop>(sqlstr.ToString(), out errmsg, pars.ToArray());
             //return new List<chosen_ordershop>(); 
		 }

		 /// <summary>
		 /// 订单商品管理 详情查询 
		 /// </summary>
		 public static chosen_ordershop GetInfo(int orderid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from chosen_ordershop  where orderid=@orderid "; 
			 pars.Add(new DataParameter("orderid", orderid)); 
			 return MySqlHelper.GetDataInfo<chosen_ordershop>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 订单商品管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_ordershop">要添加的订单商品管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_ordershop info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("shopname", info.shopname));
			 pars.Add(new DataParameter("orderno", info.orderno));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 pars.Add(new DataParameter("thumbnail", info.thumbnail));
			 pars.Add(new DataParameter("prices", info.prices));
			 pars.Add(new DataParameter("shopnum", info.shopnum));
			 string sqlstr = "insert into chosen_ordershop (shopname,orderno,restype,rescode,thumbnail,prices,shopnum) values (@shopname,@orderno,@restype,@rescode,@thumbnail,@prices,@shopnum) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 订单商品管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_ordershop">要更新的订单商品管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_ordershop info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("shopname", info.shopname));
			 pars.Add(new DataParameter("orderno", info.orderno));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 pars.Add(new DataParameter("thumbnail", info.thumbnail));
			 pars.Add(new DataParameter("prices", info.prices));
			 pars.Add(new DataParameter("shopnum", info.shopnum));
			 string sqlstr = "update chosen_ordershop set shopname=@shopname,orderno=@orderno,restype=@restype,rescode=@rescode,thumbnail=@thumbnail,prices=@prices,shopnum=@shopnum where orderid=@orderid "; 
			 pars.Add(new DataParameter("orderid",info.orderid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 订单商品管理 删除 
		 /// </summary>
		 public static bool Delete(int orderid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from chosen_ordershop where orderid=@orderid "; 
			 pars.Add(new DataParameter("orderid", orderid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 订单商品管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from chosen_ordershop where orderid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
