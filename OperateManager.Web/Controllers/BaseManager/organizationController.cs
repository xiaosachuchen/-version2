using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 机构管理 organization Controller 
	 /// </summary>
	 public class organizationController : BaseController.BaseController 
	 {

		 /// <summary>
		 /// 机构管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<base_organization> list = organizationBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 机构管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_organization info = organizationBLL.GetInfo(id,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }
        /// <summary>
        ///检测数据是否重复
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Get(string codes, string type, string flig, string orgid)
        {
            if (!string.IsNullOrEmpty(type)) {
                base_organization info = organizationBLL.selectbyType(codes, type, out errmsg);
                if (string.Equals("修改", flig)) {
                    if (info != null)
                    {
                        return new { valid = (string.Equals(info.orgid.ToString(), orgid)) ? true : false };
                    }
                    return new { valid = true };
                }
                return new { valid = (info == null) ? true : false };
            }

            return new { valid = false };
        }
        /// <summary>
        /// 机构管理 添加数据 
        /// </summary>
        public object Post([FromBody] base_organization info) 
		 {
			 bool success = organizationBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加机构{0}，返回{1} {2}", info.orgname, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 机构管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_organization info) 
		 {
			 info.orgid = id; 	
			 bool success = organizationBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新机构id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 机构管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = organizationBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除机构id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 机构管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = organizationBLL.BatchDelete(IdArray.IdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除机构id为({0})，返回{1} {2}", idstr, success, errmsg));

             if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
