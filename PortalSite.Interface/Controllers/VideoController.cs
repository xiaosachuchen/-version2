using Microsoft.RIPSP.Model;
using OperateManager.DAL.CourseManager;
using OperateManager.Models.Resourcedb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.SessionState;
using PortalSite.Interface.Globle;

namespace PortalSite.Interface.Controllers
{
    public class VideoController : ApiController,IRequiresSessionState
    {
        [HttpGet, Route("Video/GetVideos")]
        public object GetCourseList(string videotype, bool ispage = false, int rows = 0, int offset = 0)
        {
            string errmsg = null;
            int total = 0;
            //测试代码
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                List<self_video> videos = new List<self_video>();
                for (int i = 0; i < rows; i++)
                {
                    videos.Add(new self_video() {
                                                    subject = 2,
                                                    seqid = 1,
                                                    //source = "img/1/240-240/hx0001.jpg",
                                                    source =RandomPic() ,
                                                    abstracts = "不错的视频",
                                                    title = "测试的视频",
                                                    s_price =20
                                                });
                }
                return new { courses = videos, msg = errmsg, coursetotal = total };
            }
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() {
                                            name ="title",
                                            value =videotype,
                                            exp ="like"
                                        });
            List<self_video> coures = videoDAL.GetVideoByPaging(parms, ispage, offset, rows, out total, out errmsg);
            coures.ForEach(r => r.source = string.Format("{0}{1}",GlobleSetting.GetAppSettingKey("RootFilePath"),r.thumbnail));
            return new { courses = coures, msg = errmsg, coursetotal = total };
        }
        [HttpGet, Route("Video/GetVideoDetail")]
        public object GetCourssDetail(int videoid)
        {
            string errmsg = null;
            int total = 0;
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                return new { video=new self_video()
                                                 {
                                                      title = "易混淆动词解析" ,
                                                      time = "1h:10min",
                                                      teacher =1,courseid=2,
                                                      abstracts = "英语中有些动词的过去式(通常也是过去分词)与另一些动词的现在式相同,因此学生常常会把它们搞错。现 将这些常见的容易使学生搞混的词汇编在一起,用练习形式供学生对比。",
                                                      source="img/video/1.webm"
                                                 },
                                                 teacher=new self_teacher() {
                                                                             title="王老师",
                                                                             abstracts="北京四中物理老师",
                                                                             subject="物理",
                                                                             grade="高二",
                                                                              },
                                                 msg = errmsg, coursetotal = total };
            }
            self_video videoinfo = videoDAL.GetInfo(videoid, out errmsg);
            return new { video =videoinfo ,teacher=videoinfo.teacherobject, msg = errmsg };
        }
        private string RandomPic()
        {
            List<string> filenames = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(ConfigurationManager.AppSettings["randompicpath"].ToString());
            FileInfo[] files = TheFolder.GetFiles();
            DirectoryInfo[] dirInfo = TheFolder.GetDirectories();
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                FileInfo[] fileInfo = NextFolder.GetFiles();
                foreach (FileInfo NextFile in fileInfo)  //遍历文件
                    filenames.Add("img\\1\\"+NextFolder.Name+"\\"+NextFile.Name);
            }
            Random r = new Random();
            return filenames.ElementAt(r.Next(filenames.Count));
        }
    }
}
