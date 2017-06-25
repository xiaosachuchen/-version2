using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.ChosenManager;
using Microsoft.RIPSP.BLL.BaseManager;

namespace Microsoft.RIPSP.Controller.ChosenManager 
{
	 /// <summary>
	 /// 服务管理 serviceinfo Controller 
	 /// </summary>
	 public class serviceinfoController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 服务管理 列表查询 
		 /// </summary>
		 public object Get(string type,int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
			 List<chosen_serviceinfo> list = serviceinfoBLL.GetPageList(type,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            List<string> listarr = new List<string>();
            foreach (chosen_serviceinfo item in list)
            {
                item.statusname = BaseBLL.GlobalCommonBLL.GetDicName("ResServiceStatus", item.status.ToString());//服务状态
                item.stypename = BaseBLL.GlobalCommonBLL.GetDicName("ServiceType", item.stype.ToString());//订单状态
                listarr.Add(item.customerid.ToString());
            }
            List<base_customers> customerList = customersBLL.GetInfoList(listarr, out errmsg);
            foreach (var item in list)
            {
                item.customername = customerList.Find(a => a.customerid == item.customerid).customername;
            }
            return new { rows = list, total };
		 }

		 /// <summary>
		 /// 服务管理 详情查询 
		 /// </summary>
		 public object Get(string id) 
		 {
			 chosen_serviceinfo info = serviceinfoBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 服务管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] chosen_serviceinfo info) 
		 {
            info.creator = LoginUserData.UserID;
            info.createdtime = DateTime.Now;
            string datenum = DateTime.Now.ToString("yyyyMMddhh24mmss");
            Random rd = new Random();
            string sjnum = rd.Next(1000000,10000000).ToString();
            string viceo = datenum + sjnum;
            info.serviceno = viceo;
            info.leftnum = info.maxnum;
            info.status = 0;
			 bool success = serviceinfoBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加服务{0}，返回{1} {2}", info.servicename, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 服务管理 更新数据 
		 /// </summary>
		 public object Put(string id,[FromBody] chosen_serviceinfo info) 
		 {
			 info.serviceno = id;
            info.creator = LoginUserData.UserID;
            info.createdtime = DateTime.Now;
            bool success = serviceinfoBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新服务id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				   return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 服务管理 删除 
		 /// </summary>
		 public object Delete(string id) 
		 {
			 bool success = serviceinfoBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除服务id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 服务管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = serviceinfoBLL.BatchDelete(IdArray.StrIdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量服务id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
