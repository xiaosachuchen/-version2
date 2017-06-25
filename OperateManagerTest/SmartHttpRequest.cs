using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using LitJson;
namespace PortalSite.Interface.WebServiceClient
{
    public class SmartHttpRequest
    {
        public static JsonData WebServicePost(string url, string method, string param)
        {
            //转换输入参数的编码类型，获取bytep[]数组 
            byte[] byteArray = Encoding.UTF8.GetBytes("json=" + param);
            //初始化新的webRequst
            //1． 创建httpWebRequest对象
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url + "/" + method));
            //2． 初始化HttpWebRequest对象
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)
            Stream newStream = webRequest.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            //4． 读取服务器的返回信息
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            JsonData result =JsonMapper.ToObject(new StreamReader(response.GetResponseStream(), Encoding.UTF8));
            return result;
        }
        public static JsonData WebServiceGet(string url,string method)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
            request.Method = method;
            if (method.ToLower() == "post")
            {
                request.ContentType = "application/x-www-form-urlencoded";
                /*  可接收可压缩HTML*/
                request.ContentLength = 0;
                request.Headers.Add("Accept-Language", "zh-cn");
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            JsonData result = JsonMapper.ToObject(new StreamReader(responseStream, Encoding.UTF8));
            return result;
        }
    }
}