using Microsoft.RIPSP.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.Globle
{
    public class GlobleSetting
    {
        public static List<Options> subjects = new List<Options>();
        public static List<Options> grades = new List<Options>();
        public static List<Options> versions = new List<Options>();
        public static List<Options> press = new List<Options>();
        public static List<Options> coursetypes = new List<Options>();
        public static string GetAppSettingKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static string GetDicValue(string key,string type)
        {
            switch (type)
            {
                case "subject":
                               return subjects.Where(r => r.text.Equals(key)).FirstOrDefault().id;
                case "grade":
                               return grades.Where(r => r.text.Equals(key)).FirstOrDefault().id;
                case "version":
                               return versions.Where(r => r.text.Equals(key)).FirstOrDefault().id;
                case "press":
                               return press.Where(r => r.text.Equals(key)).FirstOrDefault().id;
                case "coursetype":
                               return coursetypes.Where(r => r.text.Equals(key)).FirstOrDefault().id;
                default:
                               return "";

            }
        }
    }
}