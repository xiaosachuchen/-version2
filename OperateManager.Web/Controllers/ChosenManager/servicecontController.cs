using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.ChosenManager; 

namespace Microsoft.RIPSP.Controller.ChosenManager 
{
	 /// <summary>
	 /// 服务内容表 servicecont Controller 
	 /// </summary>
	 public class servicecontController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 服务内容表 列表查询 
		 /// </summary>
		 public object Get(string serviceno,int offset,int limit)
		 {
			 int total;
			 List<chosen_servicecont> list = servicecontBLL.GetPageList(serviceno, offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 服务内容表 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 chosen_servicecont info = servicecontBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 服务内容表 添加数据 
		 /// </summary>
		 //public object Post([FromBody] chosen_servicecont info) 
		 //{
   //         info.createdtime = DateTime.Now;
   //         info.creator = LoginUserData.UserID;
   //         string databasename = PortalSite.Interface.BLL.SearchBLL.GetTableNameByResType(Convert.ToInt32(info.restype));
   //         info.restype = databasename;
   //         bool success = servicecontBLL.Add(info, out errmsg);
   //         WriteSysLog(0, string.Format("添加服务编号为{0}，返回{1} {2}", info.serviceno, success, errmsg));
   //         if (!success || !string.IsNullOrEmpty(errmsg))
			//	  return new { Rcode = -1, Rmsg = "添加失败" };
			// return new { Rcode = 1, Rmsg = "添加成功" };
		 //}

		 /// <summary>
		 /// 服务内容表 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] chosen_servicecont info) 
		 {
			 info.seqid = id; 	
			 bool success = servicecontBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新服务内容id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 服务内容表 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = servicecontBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除服务内容id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 服务内容表 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = servicecontBLL.BatchDelete(IdArray.IdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除服务内容id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
