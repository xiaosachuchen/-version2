using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.ChosenManager;
using Microsoft.RIPSP.BLL.BaseManager;

namespace Microsoft.RIPSP.Controller.ChosenManager 
{
	 /// <summary>
	 /// 服务权限表 servicepermit Controller 
	 /// </summary>
	 public class servicepermitController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 服务权限表 列表查询 
		 /// </summary>
		 public object Get(string serviceno,int offset,int limit)
		 {
			 int total;
			 
			 List<chosen_servicepermit> list = servicepermitBLL.GetPageList(serviceno, offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            List<string> queryarr = new List<string>();
            foreach (var item in list)
            {
                queryarr.Add(item.userid.ToString());
            }
            List<base_users> userList = usersBLL.GetListInfo(queryarr,out errmsg);
            foreach (var item in list)
            {
                item.nickname = userList.Find(a => a.userid == item.userid).username;
            }
             return new { rows = list, total };
		 }

		 /// <summary>
		 /// 服务权限表 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 chosen_servicepermit info = servicepermitBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }
        
		 /// <summary>
		 /// 服务权限表 添加数据 
		 /// </summary>
		 public object Post([FromBody] chosen_servicepermit info) 
		 {
            info.createdtime = DateTime.Now;
            info.creator = LoginUserData.UserID;
             bool success = servicepermitBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 服务权限表 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] chosen_servicepermit info) 
		 {
			 info.seqid = id; 	
			 bool success = servicepermitBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新服务权限表 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 服务权限表 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = servicepermitBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除服务权限表 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 服务权限表 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = servicepermitBLL.BatchDelete(IdArray.IdArray,out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除服务权限表 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
