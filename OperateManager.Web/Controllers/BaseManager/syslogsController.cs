using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager; 

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 系统日志管理 syslogs Controller 
	 /// </summary>
	 public class syslogsController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 系统日志管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit,string logtype, string creator, string startTime,string endTime)
		 {
            int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
            QueryModel model = null;
            if (!string.IsNullOrEmpty(logtype)&&!string.Equals(logtype, "-1"))
            {
                model = new QueryModel();
                model.exp = "操作类型";
                model.name = "logtype";
                model.value = logtype;
                queryarr.Add(model);
            }
            if(!string.IsNullOrEmpty(creator))
            {
                model = new QueryModel();
                model.exp = "创建人";
                model.name = "creator";
                model.value = creator;
                queryarr.Add(model);
            }
            if(!string.IsNullOrEmpty(startTime))
            {
                model = new QueryModel();
                model.exp = "开始时间";
                model.name = "createdtime";
                model.value = startTime;
                queryarr.Add(model);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                model = new QueryModel();
                model.exp = "结束时间";
                model.name = "createdtime";
                model.value = endTime;
                queryarr.Add(model);
            }
            List<base_syslogs> list = syslogsBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            foreach (var item in list)
            {
                item.logtypename = BaseBLL.GlobalCommonBLL.GetDicName("SysLogType", item.logtype.ToString());
            }
            return new { rows = list, total };
		 }

		 /// <summary>
		 /// 系统日志管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_syslogs info = syslogsBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 系统日志管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_syslogs info) 
		 {
			 bool success = syslogsBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 系统日志管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_syslogs info) 
		 {
			 info.logid = id; 	
			 bool success = syslogsBLL.Update(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 系统日志管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = syslogsBLL.Delete(id, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 系统日志管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = syslogsBLL.BatchDelete(IdArray.IdArray,out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

	 }
}
