using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 菜单管理 menus BLL 
	 /// </summary>
	 public static class menusBLL 
	 {
        /// <summary>
        /// 菜单管理 列表查询 
        /// </summary>
        /// <param name="queryarr">查询条件</param>
        /// <param name="offset">起始条数</param>
        /// <param name="limit">获取条数</param>
        /// <param name="total">返回总记录数</param>
        public static List<base_menus> GetPageList(out string errmsg)
        {
            int total = 0;
            List<base_menus> list = menusDAL.GetPageList(new List<QueryModel>(), -1, 0, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || list == null)
                return new List<base_menus>();
            foreach(base_menus menu in list)
            {
                menu.isoutlinkname = BaseBLL.GlobalCommonBLL.GetDicName("YesOrNo", menu.isoutlink.ToString());
               // menu.statusname = BaseBLL.GlobalCommonBLL.GetDicName("EnableStatus", menu.status.ToString());
            }
            base_menus rootmenu = new base_menus() { menuid = 0};
            SetMenuChildren(rootmenu, list);
            return rootmenu.children;
        }
        private static void SetMenuChildren(base_menus parentmenu, List<base_menus> list)
        {
            parentmenu.children = new List<base_menus>();
            List<base_menus> menuChildren = list.FindAll(
                delegate (base_menus c)
                {
                    return c.parentmenuid == parentmenu.menuid;
                });
            foreach (base_menus menu in menuChildren)
            {
                parentmenu.children.Add(menu);
                SetMenuChildren(menu, list);
            }
        }

        /// <summary>
        /// 菜单管理 详情查询 
        /// </summary>
        public static base_menus GetInfo(int menuid, out string errmsg) 
		 {
			 base_menus info = menusDAL.GetInfo(menuid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 菜单管理 添加数据 
		 /// </summary>
		 /// <param name="base_menus">要添加的菜单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_menus info,out string errmsg) 
		 {
			 return menusDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 菜单管理 更新数据 
		 /// </summary>
		 /// <param name="base_menus">要更新的菜单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_menus info,out string errmsg) 
		 {
			 return menusDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 菜单管理 删除 
		 /// </summary>
		 public static bool Delete(int menuid,out string errmsg) 
		 {
			 return menusDAL.Delete(menuid, out errmsg); 
		 }

		 /// <summary>
		 /// 菜单管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return menusDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
