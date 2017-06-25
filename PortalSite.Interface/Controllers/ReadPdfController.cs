using PortalSite.Interface.BLL;
using PortalSite.Interface.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PortalSite.Interface.Controllers
{
    public class ReadPdfController : ApiController
    {
        BLL.UserLoginManager userLoginManager = new BLL.UserLoginManager();

        [HttpPost, Route("ReadPdf/GetBookInfo")]
        public object GetBookInfo(BookParm book)
        {
            string errmsg = null;
            if (book == null || book.did< 1 || book.sid < 1 || book.pid < 0)
            {
                return new { Rcode = -1, Rmsg = "参数错误" };
            }
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };
            string userIP = null;

            string tablename, serviceno, orderno;
            int costtype;
            decimal m_price, m_discount;
            int status = BaseBLL.GetFullPathResourceCheckCost(book.did, book.sid, userLoginManager.LoginUser.UserID, userLoginManager.LoginUser.Customerid, userIP, out tablename, out costtype, out m_price, out m_discount, out serviceno, out orderno, out errmsg);
            if (status == -1)
                return new { Rcode = -1, Rmsg = errmsg };

            if (status == 0)
                return new { Rcode = -3, Rmsg = "此资源需要付费才能在线阅读，现在去付费", Rdata = orderno };

            BookInfo bookinfo = ResourcesBLL.GetFilePath(tablename, book.sid, book.pid, "fullpath", out errmsg);
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
            if (!Path.GetExtension(pdffile).Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
            {
                return new { Rcode = -1, Rmsg = "此资源非在线阅读资源，请直接下载" };
            }
            BookInfo info = new BookInfo();
            info.IsCopy = false;
            info.IsDown = false;
            info.IsFind = false;
           
            string swfpath = Path.GetDirectoryName(pdffile) + "\\swf";
            if (!Directory.Exists(swfpath))
            {
                return new { Rcode = -1, Rmsg = "资源文件未转换" };
            }
            string[] files = Directory.GetFiles(swfpath, "*.swf");
            if (files.Length == 0)
            {
                return new { Rcode = -1, Rmsg = "资源文件未转换或转换失败" };
            }
            info.LimitPage = bookinfo.LimitPage;
            info.PageCount = files.Length;
            info.Domain = string.Format("http://{0}", HttpContext.Current.Request.Url.Authority);
            info.BookPath = Path.GetDirectoryName(bookinfo.BookPath) + "/swf";
            string[] fiarr = Directory.GetFiles(Path.GetDirectoryName(pdffile), "*.xml");
            if (fiarr.Length > 0)
            {
                try
                {
                    //解析xml目录文件

                    //string xmlstr = File.ReadAllText(fiarr[0]);
                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.LoadXml(xmlstr);
                    //XmlElement rootnode = (XmlElement)xmldoc.SelectSingleNode("Bookmark");
                    //RIPS.BaseModels.Options options = new RIPS.BaseModels.Options() { id = "0", text = "", children = new List<RIPS.BaseModels.Options>() };
                    //int idnum = 0;
                    //getChildren(rootnode, options, idnum);
                    //info.ThemeStr = options.children;
                }
                catch (Exception ex)
                {
                    info.ThemeStr = null;
                }
            }
            return new { Rcode = 1, Rdata = info };
        }
        //private static void getChildren(XmlElement element, RIPS.BaseModels.Options option, int idnum)
        //{
        //    option.children = new List<RIPS.BaseModels.Options>();
        //    RIPS.BaseModels.Options op;
        //    string text;
        //    foreach (object obja in element.ChildNodes)
        //    {
        //        if (obja.GetType() == typeof(XmlElement))
        //        {
        //            XmlElement el = (XmlElement)obja;
        //            if (el.FirstChild != null)
        //                text = el.FirstChild.Value;
        //            else
        //                text = el.InnerText;
        //            text = text.Replace("\r\n", "");
        //            op = new RIPS.BaseModels.Options() { id = idnum.ToString() + "_" + el.GetAttribute("Page"), text = text, children = new List<RIPS.BaseModels.Options>() };
        //            idnum++;
        //            option.children.Add(op);
        //            if (el.ChildNodes.Count > 0)
        //            {
        //                getChildren(el, op, idnum);
        //            }
        //        }
        //    }       
        //}


    }
}
