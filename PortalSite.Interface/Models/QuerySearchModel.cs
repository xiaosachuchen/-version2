using System.Collections.Generic;

namespace PortalSite.Interface.Models
{
    public class QuerySearchModel
    {
        public string queryExp { get; set; }
        public bool isSearch { get; set; }
        public int restype { get; set; }
        public string groupName { get; set; }
        public string sortExp { get; set; }
        public int page { get; set; }
        public int rows { get; set; }
    }
    public class FilterInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<object> Values { get; set; }
    }

}