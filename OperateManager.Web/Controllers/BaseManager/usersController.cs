using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.BaseManager;
using Microsoft.RIPSP.BaseBLL;

namespace Microsoft.RIPSP.Controller.BaseManager 
{
	 /// <summary>
	 /// 用户管理 users Controller 
	 /// </summary>
	 public class usersController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 用户管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
    
			 List<base_users> list = usersBLL.GetPageList(queryarr,offset,limit,out total,out errmsg);
            
            if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            foreach (base_users baseusers in list)
            {
                 baseusers.sexname = BaseBLL.GlobalCommonBLL.GetDicName("Sex", baseusers.sex.ToString());
                 baseusers.sourcetypename = BaseBLL.GlobalCommonBLL.GetDicName("UserSourceType", baseusers.sourcetype.ToString());
                 baseusers.statusname=GlobalCommonBLL.GetDicName("UserStatus", baseusers.status.ToString());
                 baseusers.usertypename= BaseBLL.GlobalCommonBLL.GetDicName("UserType", baseusers.usertype.ToString());
                 baseusers.countryname= BaseBLL.GlobalCommonBLL.GetDicName("Country", baseusers.country);
                 baseusers.areacodename = BaseBLL.GlobalCommonBLL.GetDicName("Areacode", baseusers.areacode);
            }
             return new { rows = list, total };
		 }

		 /// <summary>
		 /// 用户管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 base_users info = usersBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }
         /// <summary>
         ///检测用户名是否存在 
         /// </summary>
         /// <param name="username"></param>
         /// <returns></returns>
         [HttpGet]
        public object Get(string codes,string type, string flig, string userid)
        {
            if (!string.IsNullOrEmpty(type)) {
                base_users info = usersBLL.selectusername(codes, type, out errmsg);
                if (string.Equals("修改", flig)) {
                    if (info != null)
                    {
                        return new { valid = (string.Equals(info.userid.ToString(), userid)) ? true : false };
                    }
                    return new { valid = true };
                }
                return new { valid = (info == null) ? true : false };
            }
        

            return new { valid =false};
        }
		 /// <summary>
		 /// 用户管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] base_users info) 
		 {
            info.createdtime = DateTime.Now;
            info.nickname = info.username;
            info.usertype = 1;//用户类型
            info.userlevel = 1;
            info.balance = 0;
            info.score = 0;
            info.sourcetype = 1;//来源类型
            info.passwd = GlobalParameters.DefaultUserPasswd;
            info.customerid = 0;

			 bool success = usersBLL.Add(info, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }
      
		 /// <summary>
		 /// 用户管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] base_users info) 
		 {
            bool success = false;
            info.userid = id;
            if (info.username == null && info.email == null & info.phone == null)
            {
                success = usersBLL.UpdateCus(info.customerid, info.userid, out errmsg);

            }
            else {
                info.createdtime = DateTime.Now;
                success = usersBLL.Update(info, out errmsg);
            }
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 用户管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = usersBLL.Delete(id, out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 用户管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = usersBLL.BatchDelete(IdArray.IdArray,out errmsg); 
			 if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }
        #region
        ///检测用户名
        #endregion
    }
}
