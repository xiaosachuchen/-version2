using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;
using Microsoft.RIPSP.BaseController;

namespace OperateManager.Web.Controllers.BaseManager
{
    public class WareManagerController : BaseController
    {
        private string errmsg = null;
        public object Get( bool WithNone=false)
        {
            List<Options> list = shopinfBLL.GetShopInfoList(out errmsg);
            if (list == null)
            {
                list = new List<Options>();
            }
            if (WithNone)
            {
                Options options2 = new Options
                {
                    id = "-1",
                    text = "--请选择--"
                };
                list.Insert(0, options2);
            }

            return list;
        }
    }
}