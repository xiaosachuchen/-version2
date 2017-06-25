using Microsoft.RIPSP.DAL.BaseManager;
using Microsoft.RIPSP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.RIPSP.BLL.BaseManager
{
    public static class shopinfBLL
    {
        /// <summary>
        /// 查询商品列表
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<Options> GetShopInfoList(out string errmsg)
        {
            return shopinfoDAL.GetShopInfoList(out errmsg);
        }
    }
}
