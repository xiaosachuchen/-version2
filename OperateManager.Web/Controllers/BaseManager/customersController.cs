using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 客户管理 customers Controller 
	 /// </summary>
	 public class customersController : BaseController.BaseController 
	 {
		 

        /// <summary>
        /// 客户管理 列表查询 
        /// </summary>,string search
        [HttpGet]
		 public object Get(int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<base_customers> list = customersBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 客户管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_customers info = customersBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
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
        public object Get(string codes, string type,string flig,string customerid)
        {
            if (!string.IsNullOrEmpty(type))
            {
                base_customers info = customersBLL.selectcusername(codes, type, out errmsg);
                if (string.Equals("修改", flig))
                {
                    if (info != null)
                    {
                        return new { valid = (string.Equals(info.customerid.ToString(), customerid)) ? true : false };
                    }
                    return new { valid = true };
                }
                return new { valid = (info == null) ? true : false };
            }
                return new { valid = false };
        }
        /// <summary>
        /// 客户管理 添加数据 
        /// </summary>
        public object Post([FromBody] base_customers info) 
		 {
            info.creator = LoginUserData.UserID;
            info.createdtime = DateTime.Now;
			 bool success = customersBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加客户{0}，返回{1} {2}", info.customername, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 客户管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_customers info) 
		 {
			 info.customerid = id;
             info.creator = LoginUserData.UserID;
             info.createdtime = DateTime.Now;
            bool success = customersBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新客户id为{0}，返回{1} {2}",id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 客户管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = customersBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除客户id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 客户管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = customersBLL.BatchDelete(IdArray.IdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除客户id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
