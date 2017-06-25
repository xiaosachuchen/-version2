using PortalSite.Interface.BLL;
using PortalSite.Interface.Models;
using PublicHelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PortalSite.Interface.Controllers
{
    public class DownFileController : ApiController
    {
        BLL.UserLoginManager userLoginManager = new BLL.UserLoginManager();
        string errmsg = null;

        [HttpGet, Route("DownFile/DownFileData")]
        public object DownFileData(int Restype, long SeqID, string ColumnName)
        {
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rdata = "未登录" };

            string userIP = null;

            string tablename, serviceno, orderno;
            int costtype;
            decimal m_price, m_discount;
            int status = BaseBLL.GetFullPathResourceCheckCost(Restype, SeqID, userLoginManager.LoginUser.UserID, userLoginManager.LoginUser.Customerid, userIP, out tablename, out costtype, out m_price, out m_discount, out serviceno, out orderno, out errmsg);
            if (status == -1)
                return new { Rcode = -1, Rmsg = errmsg };

            if (status == 0)
                return new { Rcode = -3, Rmsg = "此资源需要付费才能下载，现在去付费", Rdata = orderno };

            BookInfo bookinfo = ResourcesBLL.GetFilePath(tablename, SeqID, 0, ColumnName, out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || bookinfo == null)
                return new { Rcode = -1, Rmsg = "获取资源数据错误" + errmsg };
            if (string.IsNullOrEmpty(bookinfo.BookPath))
            {
                return new { Rcode = -1, Rmsg = "资源文件路径不存在" };
            }
            string pdffile = string.Format("{0}{1}", GlobalParameters.RootFilePath, bookinfo.BookPath).Replace("//", "/").Replace("/", "\\");
            if (!File.Exists(pdffile))
            {
                return new { Rcode = -1, Rmsg = "资源文件不存在" };
            }
            string filecode = StrFormatClass.Encrypt(bookinfo.BookPath + DateTime.Now.ToString("yyyyMMddHHmmss"), ref errmsg);
            return new { Rcode = 1, Rdata = filecode };
        }

        [HttpGet, Route("DownFile/DownFile")]
        public object DownFile(string filecode)
        {
            if (string.IsNullOrEmpty(filecode))
            {
                HttpContext.Current.Response.Write("参数错误");
                HttpContext.Current.Response.End();
                return null;
            }
            string filepath = StrFormatClass.Decrypt(filecode, ref errmsg);
            if (!string.IsNullOrEmpty(errmsg) || string.IsNullOrEmpty(filepath) || filepath.Length <= 14)
            {
                HttpContext.Current.Response.Write("参数解析错误");
                HttpContext.Current.Response.End();
                return null;
            }
            filepath = filepath.Substring(0, filepath.Length - 14);
            string filename = Models.GlobalParameters.RootFilePath + filepath;
            if (!File.Exists(filename))
            {
                HttpContext.Current.Response.Write("文件不存在");
                HttpContext.Current.Response.End();
                return null;
            }
            string fname = Path.GetFileName(filename);
            //文件名  
            string str = "attachment;filename=" + fname;
            //把文件头输出，此文件头激活文件下载框  
            HttpContext.Current.Response.AppendHeader("Content-Disposition", str);//http报头文件  
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(filename);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            return null;
        }
    }
}
