using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.Models
{
    public class ISeeIndexServer
    {
        public ISeeIndexServer()
        {

        }
        public ISeeIndexServer(int _rtype, string _ip, int _port, string _index, bool _autofiltertag, int _maxmatchcount)
        {
            this.RType = _rtype;
            this.IP = _ip;
            this.Port = _port;
            this.Index = _index;
            this.AutoFilterTag = _autofiltertag;
            this.MaxMatchCount = _maxmatchcount;
        }
        public int RType { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Index { get; set; }
        public bool AutoFilterTag { get; set; }
        public int MaxMatchCount { get; set; }

        public List<SearchMeta> Fields { get; set; }
        public List<SearchMeta> Attrs { get; set; }
    }
    public class SearchMeta
    {
        public string MetaName { get; set; }
        public string MetaCname { get; set; }
        public string MetaType { get; set; }
    }

    public class SearchGroupModel
    {
        public string groupname { get; set; }
        public string groupvalue { get; set; }
        public int groupcount { get; set; }
        public string grouptags { get; set; }
        public List<SearchGroupModel> children { get; set; }
    }
}