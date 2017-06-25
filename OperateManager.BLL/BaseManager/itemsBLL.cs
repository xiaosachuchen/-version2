using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 栏目管理 items BLL 
	 /// </summary>
	 public static class itemsBLL 
	 {
		 /// <summary>
		 /// 栏目管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 //public static List<base_items> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 //{
			// List<base_items> list = itemsDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			// return list;
		 //}
        public static List<base_items> GetPageList(out string errmsg)
        {
            int total = 0;
            List<base_items> list = itemsDAL.GetPageList(new List<QueryModel>(),-1,0,out total,out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || list == null)
                return new List<base_items>();
            int outparentitemId = 0;
            foreach (var item in list)
            {
                item.navigationname = BaseBLL.GlobalCommonBLL.GetDicName("YesOrNo", item.navigation.ToString());
                item.sourcetypename = BaseBLL.GlobalCommonBLL.GetDicName("ItemSourceType", item.sourcetype.ToString());
                item.statusname = BaseBLL.GlobalCommonBLL.GetDicName("EnableStatus", item.status.ToString());
            }
            base_items rootitem = new base_items() { itemid = 0 };
            SetItemChildren(rootitem,list);
            return rootitem.children;
        }
        private static void SetItemChildren(base_items parentItem, List<base_items> list)
        {
            parentItem.children = new List<base_items>();
            List<base_items> ItemChildren = list.FindAll(
                delegate (base_items c)
                {
                    return c.parentid == parentItem.itemid;
                });
            foreach (base_items item in ItemChildren)
            {
                parentItem.children.Add(item);
                SetItemChildren(item, list);
            }
        }
        /// <summary>
        /// 栏目管理 详情查询 
        /// </summary>
        public static base_items GetInfo(int itemid, out string errmsg) 
		 {
			 base_items info = itemsDAL.GetInfo(itemid, out errmsg); 
			 return info;
		 }
        public static List<base_items> GetInfoByID(int itemid, out string errmsg)
        {
            List<base_items> infos = new List<base_items>();
            infos = itemsDAL.GetInfobyId(itemid, out errmsg);
            return infos;
        }
		 /// <summary>
		 /// 栏目管理 添加数据 
		 /// </summary>
		 /// <param name="base_items">要添加的栏目管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_items info,out string errmsg) 
		 {
			 return itemsDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 栏目管理 更新数据 
		 /// </summary>
		 /// <param name="base_items">要更新的栏目管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_items info,out string errmsg) 
		 {
			 return itemsDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 栏目管理 删除 
		 /// </summary>
		 public static bool Delete(int itemid,out string errmsg) 
		 {
			 return itemsDAL.Delete(itemid, out errmsg); 
		 }

		 /// <summary>
		 /// 栏目管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return itemsDAL.BatchDelete(idArray, out errmsg);
		 }
        /// <summary>
        /// 检测栏目标识是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_items selectitemmark(string codes, string type, out string errmsg)
        {
            return itemsDAL.selectitemmark(codes, type, out errmsg);
        }
    }
}
