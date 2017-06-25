using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.ChosenManager;
using System.Data; 

namespace Microsoft.RIPSP.BLL.ChosenManager 
{
	 /// <summary>
	 /// 订单管理 orderinfo BLL 
	 /// </summary>
	 public static class orderinfoBLL 
	 {
		 /// <summary>
		 /// 订单管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_orderinfo> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<chosen_orderinfo> list = orderinfoDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 订单管理 详情查询 
		 /// </summary>
		 public static chosen_orderinfo GetInfo(string orderno, out string errmsg) 
		 {
			 chosen_orderinfo info = orderinfoDAL.GetInfo(orderno, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 订单管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_orderinfo">要添加的订单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_orderinfo info,out string errmsg) 
		 {
			 return orderinfoDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 订单管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_orderinfo">要更新的订单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_orderinfo info,out string errmsg) 
		 {
			 return orderinfoDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 订单管理 删除 
		 /// </summary>
		 public static bool Delete(string orderno,out string errmsg) 
		 {
			 return orderinfoDAL.Delete(orderno, out errmsg); 
		 }

		 /// <summary>
		 /// 订单管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(string[] idArray,out string errmsg) 
		 {
			 return orderinfoDAL.BatchDelete(idArray, out errmsg);
		 }
        #region  交易管理 个人
        public static DataTable GetInfoEchar_user(string timetype, string starDate, string endDate, out string errmsg)
        {
            DataTable dt = orderinfoDAL.GetInfoEchar_user(timetype, starDate, endDate, out errmsg);
            return dt;
        }
        #endregion
        #region 机构
        public static DataTable GetInfoEchar_org(string timetype, string starDate, string endDate, out string errmsg)
        {
            DataTable dt = orderinfoDAL.GetInfoEchar_org(timetype, starDate, endDate, out errmsg);
            return dt;
        }
        #endregion
        #region 资源管理 收藏量

        public static DataTable GetResFractional(int type,out string errmsg)
        {
            DataTable dt;
            //收藏
            if (type == 1)
            {
                dt = orderinfoDAL.GetResFractionalX(out errmsg);
            }//浏览
            else if (type == 2)
            {
                dt = orderinfoDAL.GetPageViewX(out errmsg);
            }//购买
            else if (type == 3)
            {
                dt = orderinfoDAL.GetPurchX(out errmsg);
            }//分享
            else {
                dt = orderinfoDAL.GetSharX(out errmsg);
            }
            return dt;
        }
        public static DataTable GetResFractionalY(int type,int restype, int classid,string starTime,string endTime, out string errmsg)
        {
            DataTable dt;
            List<base_classes> list = new List<base_classes>();
            list = orderinfoDAL.GetChildrenByParentid(classid,out errmsg);
            string id="";
            foreach (base_classes row in list) {
                id += row.classid.ToString() + ",";
            }
            id = id.TrimEnd(',');
            string tablename = GetTableNameByResType(restype);
            //收藏
            if (type == 1)
            {
                dt = orderinfoDAL.GetResFractionalY(tablename, restype, id, starTime, endTime, out errmsg);
            }//浏览
            else if (type == 2)
            {
                dt = orderinfoDAL.GetPageViewY(tablename, restype, id, starTime, endTime, out errmsg);
            }//购买
            else if (type == 3)
            {
                dt = orderinfoDAL.GetPurchY(tablename, restype, id, starTime, endTime, out errmsg);
            }//分享
            else
            {
                dt = orderinfoDAL.GetSharY(tablename, restype, id, starTime, endTime, out errmsg);
            }

            return dt;
        }
        public static DataTable GetInfoFractional(out string errmsg)
        {
            DataTable dt = orderinfoDAL.GetInfoFractional(out errmsg);
            return dt;
        }

        #endregion
        public static List<transTion> GetFracList(int offset, int limit, out int total, out string errmsg)
        {
           
            List<transTion> list = orderinfoDAL.GetFracList(offset, limit,out total, out errmsg);
            return list;
        }
        /// <summary>
        /// 根据资源类型获取资源库表名称
        /// </summary>
        /// <param name="resType"></param>
        /// <returns></returns>
        public static string GetTableNameByResType(int resType)
        {
            if (resType < 1) return null;
            string errmsg = null;
            List<db_datalibrarys> librarylist = orderinfoDAL.GetResTypeList(out errmsg);
            if (string.IsNullOrEmpty(errmsg) && librarylist != null && librarylist.Count > 0)
            {
                db_datalibrarys library = librarylist.Find(o => o.databaseid == resType);
                if (library != null)
                    return library.databasename;

            }
            return null;
        }
        public static List<db_datalibrarys> GetResTypeList()
        {
           
            string errmsg = null;
            List<db_datalibrarys> librarylist = orderinfoDAL.GetResTypeList(out errmsg);
        
            return librarylist;
        }
    }
}
