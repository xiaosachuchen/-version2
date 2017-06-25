using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;
using System.Text;

namespace Microsoft.RIPSP.Controller.BaseManager
{
    /// <summary>
    /// 管理员管理 managers Controller 
    /// </summary>
    public class managersController : BaseController.BaseController
    {
        

     
        /// <summary>
        /// 管理员管理 列表查询 
        /// </summary>
        public object Get(int offset, int limit,string nametype,string name)
        {
            
            int total;
            List<QueryModel> queryarr = new List<QueryModel>();
            QueryModel model = null;
            if (nametype == "1")
            {
                model = new QueryModel();
                model.name = "username";
                model.value = name;
            }else
            {
                model = new QueryModel();
                model.name = "realname";
                model.value = name;
            }
            queryarr.Add(model);
            List<base_managers> list = managersBLL.GetPageList(queryarr, offset, limit, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "获取数据失败" };
            }
            foreach (var item in list)
            {
                item.statusname = BaseBLL.GlobalCommonBLL.GetDicName("UserStatus", item.status.ToString());
            }
            return new { rows = list, total };
        }
        /// <summary>
        /// 管理员管理 详情查询 
        /// </summary>
        public object Get(int id)
        {
            List<int> list = new List<int>();
            List<db_datalibrarys> data = managersBLL.GetDatalibraryslist(out errmsg);
            List<base_mgdb> mgdb = managersBLL.Getmgdblist(id,out errmsg);
            List<Options> oplist = new List<Options>();
            if ((mgdb != null) && (mgdb.Count > 0))
            {
                foreach (var item in mgdb)
                {
                    list.Add(item.databaseid.Value);
                }
            }
            foreach (var item in data)
            {
                Options op = new Options {
                    id = item.databaseid.ToString(),
                    text = item.databasecname,
                    selected = Array.IndexOf(list.ToArray(), item.databaseid) >= 0
                };
                oplist.Add(op);
            }
            return new { result= oplist };
        }
        
        /// <summary>
        /// 验证是否重复
        /// </summary>
        /// <param name="codes">验证的数据</param>
        /// <param name="type">验证类型</param>
        /// <returns></returns>
        public object Get(string codes,string type,string flig,string userid)
        {

            if (!string.IsNullOrEmpty(type))
            {
                base_managers success = managersBLL.GetInfoVerify(codes, type, out errmsg);
                if (string.Equals("编辑", flig))
                {
                    if(success!=null)
                    {
                        return new { valid = (string.Equals(success.userid.ToString(), userid)) ? true : false };
                    }
                    return new { valid = true };
                    //编辑是根据 codes查询数据后 对比数据id是否与本id相同
                }
                
                return new { valid = (success == null) ? true : false };
            }
            return new { valid = false };
        }

        /// <summary>
        /// 管理员管理 添加数据 
        /// </summary>
        public object Post([FromBody] base_managers info)
        {
            info.creator = LoginUserData.UserID;
            info.createdtime = DateTime.Now;
            info.orgid = 0;    
            bool success = managersBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加管理员{0},返回{1} {2}", info.username, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            return new { Rcode = 1, Rmsg = "添加成功" };
        }

        /// <summary>
        /// 管理员管理 更新数据 
        /// </summary>
        public object Put(int id, [FromBody] base_managers info)
        {
            info.userid = id;
            bool success = false;
            if (info.username == null && info.email == null && info.phone == null)
            {
                success = managersBLL.UpdateOrg(info.orgid, info.userid, out errmsg);
            }
            else
            {
                success = managersBLL.Update(info, out errmsg);
            }
          WriteSysLog(1, string.Format("更新管理员管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "更新失败" };
            return new { Rcode = 1, Rmsg = "更新成功" };
        }

        /// <summary>
        /// 管理员管理 删除 
        /// </summary>
        public object Delete(int id)
        {
            bool success = managersBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除管理员管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "删除失败" };
            return new { Rcode = 1, Rmsg = "删除成功" };
        }

        /// <summary>
        /// 管理员管理 批量删除 
        /// </summary>
        public object Delete(IdArrayModel IdArray)
        {
            if (IdArray == null)
            {
                return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
            }
            bool success = managersBLL.BatchDelete(IdArray.IdArray, out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除管理员管理 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "删除失败" };
            return new { Rcode = 1, Rmsg = "删除成功" };
        }

    }
}
