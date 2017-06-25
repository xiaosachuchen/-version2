using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.Models
{
    public class BookParm
    {
        /// <summary>
        /// 资源类型编号
        /// </summary>
        public int did { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        public long sid { get; set; }
        /// <summary>
        /// 详细资源id（页码id）
        /// </summary>
        public long pid { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long sdt { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
    public class BookInfo
    {
        /// <summary>
        /// 是否可查找（支持文字提取）
        /// </summary>
        public bool IsFind { get; set; }
        /// <summary>
        /// 是否可复制
        /// </summary>
        public bool IsCopy { get; set; }
        /// <summary>
        /// 是否可下载
        /// </summary>
        public bool IsDown { get; set; }
        /// <summary>
        /// 目录内容
        /// </summary>
        public object ThemeStr { get; set; }
        /// <summary>
        /// 资源访问地址
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 资源路径
        /// </summary>
        public string BookPath { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 初始页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 限制页数
        /// </summary>
        public int LimitPage { get; set; }
        /// <summary>
        /// 受限打开页面地址
        /// </summary>
        public string LimitUrl { get; set; }
    }
}