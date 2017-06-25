using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
    public class OrgUsers
    {
        /// <summary>
        /// 机构名称
        /// </summary>
        public string servicename { get; set; }
        /// <summary>
        /// 机构地区
        /// </summary>
        public string areacode { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string industry { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public string stype { get; set; }
        public string stypename { get; set;}
        /// <summary>
        /// 服务开始时间
        /// </summary>
        public DateTime starttime { get; set; }
        /// <summary>
        /// 服务结束时间
        /// </summary>
        public DateTime endtime { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime paytime { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal m_price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int maxnum { get; set; }
    }
}
