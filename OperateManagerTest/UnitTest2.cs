using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperateManager.Models.Resourcedb;
using OperateManager.DAL.CourseManager;
using Microsoft.RIPSP.Model;
using System.Collections.Generic;
using LitJson;
using PortalSite.Interface.WebServiceClient;

namespace OperateManagerTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestInsertVideo()
        {
            for (int i = 0; i < 20; i++)
            {
                self_video video = new self_video()
                {
                    title = "测试视频"+i,
                    courseid = 2,
                    teacher = 1,
                    m_price = 10,
                    fullpath = "test.mp4",
                    grade = 1,
                    subject = 1,
                    abstracts = "文言文讲解",
                    press = 1,
                    thumbnail = "hx0001.jpg",
                    createdtime = DateTime.Now,
                    downloads = 100,
                    status = 4
                };
                string ermsg = "";
                videoDAL.Add(video, out ermsg);
                Equals(ermsg != null);
            }
            
        }
        [TestMethod]
        public void TestInsertTeacher()
        {
            self_teacher teacher = new self_teacher()
            {
                thumbnail = "F:\\",
                title = "教师1",
                abstracts = "高级讲师",
                createdtime = DateTime.Now,
                status = 4,
                grade = "一年级",
                subject = "物理",
                hits = 100
            };
            string errmsg = "";
            teacherDAL.Add(teacher,out errmsg);
            Equals(errmsg!=null);
        }
        [TestMethod]
        public void TestGetVideos()
        {
            string errmsg = "";
            int total = 0;
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel()
            {
                name = "title",
                value = "",
                exp = "like"
            });
            videoDAL.SetSqlConnectionString();
            List<self_video> videos = videoDAL.GetVideoByPaging(parms, false, 0, 10, out total, out errmsg);
            Equals(errmsg != null);
        }
        [TestMethod]
        public void TestGetVideoDetail()
        {
            videoDAL.SetSqlConnectionString();
            string errmsg = "";
            int total = 0;
            self_video videos = videoDAL.GetInfo(21, out errmsg);
            Equals(errmsg != null);
        }
        [TestMethod]
        public void TestJsondata()
        {
            string errmsg = "";
            int total = 0;
            JsonData videos = new JsonData();
            for (int i = 0; i < 10; i++)
            {
                JsonData temp = new JsonData()
                {
                    ["title"] = "video",
                    ["url"] = "http://www.baidu.com"
                };
                videos.Add(temp);
            }
            JsonData data = new JsonData() {
                ["title"]="s",
                ["type"]=5,
                ["hits"]=4,
                ["students"]=new JsonData() {
                    ["title"]="3",
                    ["grade"]=2,
                    ["class"]=3
                },
                ["videos"]=videos

            };
            
            Equals(data != null);
        }
        [TestMethod]
        public void TestWebClient()
        {
            string errmsg = "";
            int total = 0;
            JsonData data = SmartHttpRequest.WebServiceGet("http://123.57.12.82:8900/Interface/Base/GetItemContents?itemmark=bg_zkbg&ispage=false&rows=3&offset=0","Get");
            Equals(data != null);
        }
        [TestMethod]
        public void TestWebClientPost()
        {
            string errmsg = "";
            int total = 0;
            //url 不对
            JsonData data = SmartHttpRequest.WebServiceGet("http://123.57.12.82:8900/Interface/Base/GetItemContents?itemmark=bg_zkbg&ispage=false&rows=3&offset=0", "Post");
            Equals(data != null);
        }
    }
}
