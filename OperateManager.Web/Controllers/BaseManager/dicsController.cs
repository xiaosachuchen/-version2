using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;

namespace Microsoft.RIPSP.Controller.BaseManager
{
    /// <summary>
    /// 字典管理 dics Controller 
    /// </summary>
    public class dicsController : BaseController.BaseController
    {
        /// <summary>
        /// 字典管理 列表查询 
        /// </summary>
        public object Get(int offset, int limit, string selected)
        {
            int total;
            List<QueryModel> queryarr = new List<QueryModel>();
            QueryModel model = new QueryModel();
            model.name = "dictype";
            if (selected == "-1")
            {
                List<Options> dicslist = BaseBLL.GlobalCommonBLL.GetDicOptions("", out errmsg);
                model.value = dicslist[0].id;
            }else
            {
                model.value = selected;
            }
            queryarr.Add(model);
            List<base_dics> list = dicsBLL.GetPageList(queryarr, offset, limit, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "获取数据失败" };
            }
            foreach (var item in list)
            {
                item.isdictypename = BaseBLL.GlobalCommonBLL.GetDicName("YesOrNo", item.isdictype.ToString());
            }
            return new { rows = list, total };
        }
        public object Get(string codes, string type, string flig, string dicsid, string dictype)
        {
            string andsql = "";
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "dicvalue"|| type == "dicname")
                {
                    andsql += " and dictype='" + dictype + "'";
                }
                base_dics success = dicsBLL.GetInfoVerify(codes, type, andsql, out errmsg);
                if (string.Equals("编辑字典", flig))
                {
                    if (success != null)
                    {
                        return new { valid = (string.Equals(success.dicid.ToString(), dicsid)) ? true : false };
                    }
                    return new { valid = true };
                }

                return new { valid = (success == null) ? true : false };
            }
            return new { valid = false };
        }

        /// <summary>
        /// 字典管理 详情查询 
        /// </summary>
        public object Get(int id)
        {
            base_dics info = dicsBLL.GetInfo(id, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "获取数据失败" };
            }
            return new { Rcode = 1, Rdata = info };
        }

        /// <summary>
        /// 字典管理 添加数据 
        /// </summary>
        public object Post([FromBody] base_dics info)
        {
            bool success = false;
            if (string.IsNullOrEmpty(info.dicname))
            {
                info.isdictype = 1;
                success = dicsBLL.Add(info, out errmsg);
                WriteSysLog(0, string.Format("添加字典类型{0},返回{1} {2}", info.dictypename, success, errmsg));
            }
            else
            {
                info.isdictype = 0;
                success = dicsBLL.Add(info, out errmsg);
                WriteSysLog(0, string.Format("添加字典{0},返回{1} {2}", info.dicname, success, errmsg));
            }
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_DicsList");
            return new { Rcode = 1, Rmsg = "添加成功" };
        }

        /// <summary>
        /// 字典管理 更新数据 
        /// </summary>
        public object Put(int id, [FromBody] base_dics info)
        {
            info.dicid = id;
            bool success = dicsBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新字典管理 id为 {0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "更新失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_DicsList");
            return new { Rcode = 1, Rmsg = "更新成功" };
        }

        /// <summary>
        /// 字典管理 删除 
        /// </summary>
        public object Delete(int id)
        {
            bool success = dicsBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除字典 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "删除失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_DicsList");
            return new { Rcode = 1, Rmsg = "删除成功" };
        }

        /// <summary>
        /// 字典管理 批量删除 
        /// </summary>
        public object Delete([FromBody]IdArrayModel IdArray)
        {
            if (IdArray == null)
            {
                return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
            }
            bool success=dicsBLL.DeleteByDicType(IdArray.StrIdArray, out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除字典管理 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "删除失败" };
            BaseBLL.FreelyListCache.CacheRemove("Cache_DicsList");
            return new { Rcode = 1, Rmsg = "删除成功" };
        }

    }
}
