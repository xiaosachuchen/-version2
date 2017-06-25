using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 栏目内容管理 itemcontents Controller 
	 /// </summary>
	 public class itemcontentsController : BaseController.BaseController 
	 {
		 

        /// <summary>
        /// 栏目内容管理 列表查询 
        /// </summary>
        public object Get(int offset, int limit, string itemID)
        {
            int total;
            List<base_items> info = new List<base_items>();
            List<QueryModel> queryarr = new List<QueryModel>();
            QueryModel model = null;
            if (!string.IsNullOrEmpty(itemID) && !string.Equals(itemID, "-1"))
            {
                int itemIDs = Convert.ToInt32(itemID);
                info = itemsBLL.GetInfoByID(itemIDs, out errmsg);
                if (info != null)
                {
                    foreach (base_items items in info)
                    {
                        model = new QueryModel();
                        model.exp = "标识";
                        model.name = "itemmark";
                        model.value = items.itemmark.ToString();
                        queryarr.Add(model);

                    }
                }
            }
             List<base_itemcontents> list = itemcontentsBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 栏目内容管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_itemcontents info = itemcontentsBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 栏目内容管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_itemcontents info) 
		 {
            base_items items = new base_items();
            items = itemsBLL.GetInfo(Convert.ToInt32(info.itemmark),out errmsg);
            info.itemmark = items.itemmark;
            info.createdtime = DateTime.Now;
            info.creator = LoginUserData.UserID;
            info.status = 1;
			 bool success = itemcontentsBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加栏目{0}的内容,返回{2}{3}",info.title,success,errmsg));
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 栏目内容管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_itemcontents info) 
		 {
			 info.seqid = id; 	
			 bool success = itemcontentsBLL.Update(info, out errmsg);
             WriteSysLog(1,string.Format("更新栏目内容{0}，返回{1}{2}", info.classname, success, errmsg));
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 栏目内容管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = itemcontentsBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除栏目内容id为{0}，返回{1} {2}",id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 栏目内容管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = itemcontentsBLL.BatchDelete(IdArray.IdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除栏目内容id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
