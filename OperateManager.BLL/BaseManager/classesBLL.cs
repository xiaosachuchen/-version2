using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 分类管理 classes BLL 
	 /// </summary>
	 public static class classesBLL 
	 {
		 /// <summary>
		 /// 分类管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_classes> GetPageList(out string errmsg)
		 {
            int total = 0;
            List<base_classes> list = classesDAL.GetPageList(new List<QueryModel>(), -1, 0, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || list == null)
                return new List<base_classes>();
            int outparentclassid = 0;
            foreach (var item in list)
            {
                item.parentclassname = BaseBLL.GlobalCommonBLL.GetClassName(int.TryParse(item.parentclassid.ToString(), out outparentclassid) ? int.Parse(item.parentclassid.ToString()) : outparentclassid);
            }
            base_classes rootclass = new base_classes() { classid = 0 };
            SetClassChildren(rootclass, list);
            return rootclass.children;
        }
        public static void SetClassChildren(base_classes parentclass,List<base_classes> list)
        {
            parentclass.children = new List<base_classes>();
            List<base_classes> ClassChildren = list.FindAll(
                delegate (base_classes c)
                {
                    return c.parentclassid == parentclass.classid;
                });
            foreach (base_classes item in ClassChildren)
            {
                parentclass.children.Add(item);
                SetClassChildren(item, list);
            }
        }

		 /// <summary>
		 /// 分类管理 详情查询 
		 /// </summary>
		 public static base_classes GetInfo(int classid, out string errmsg) 
		 {
			 base_classes info = classesDAL.GetInfo(classid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 分类管理 添加数据 
		 /// </summary>
		 /// <param name="base_classes">要添加的分类管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_classes info,out string errmsg) 
		 {
			 return classesDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 分类管理 更新数据 
		 /// </summary>
		 /// <param name="base_classes">要更新的分类管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_classes info,out string errmsg) 
		 {
			 return classesDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 分类管理 删除 
		 /// </summary>
		 public static bool Delete(int classid,out string errmsg) 
		 {
			 return classesDAL.Delete(classid, out errmsg); 
		 }

		 /// <summary>
		 /// 分类管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return classesDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
