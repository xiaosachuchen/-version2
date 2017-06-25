using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.RIPSP.Model;
using SqlHelperClass;

namespace Microsoft.RIPSP.DAL.BaseManager
{
    public static class shopinfoDAL
    {
        /// <summary>
        /// 查询商品列表
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<Options> GetShopInfoList(out string errmsg)
        {
            errmsg = null;
            string sqlstr = "SELECT databasename id,databasecname text FROM db_datalibrarys WHERE  isware=1";
            return MySqlHelper.GetDataList<Options>(sqlstr, out errmsg);
        }
    }
}
