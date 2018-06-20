using System;
using System.IO;
using System.Net;
using System.Text;
using CsharpHttpHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1.TestStatuCode
{

    [TestClass]
    public class UnitTestStatuCode
    {
        public string LoginUrl = "http://localhost:56662/Login/LoginResult";
        /// <summary>
        ///     用asp.net mvc创建一个网站用于测试
        /// </summary>
        [TestMethod]
        public void Test_302_By_HttpRequest()
        {
            var request = (HttpWebRequest)WebRequest.Create(LoginUrl);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "Post";
            request.AllowAutoRedirect = true;
            WebProxy webProxy = new WebProxy("127.0.0.0", 8888);
            request.Proxy = webProxy;
            var postData = "account=mike&password=123";
            var postBuffers = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postBuffers.Length;
            request.GetRequestStream().Write(postBuffers, 0, postBuffers.Length);

            var response = (HttpWebResponse)request.GetResponse();
            if(response.StatusCode!=HttpStatusCode.OK)
                throw new Exception("登录失败！");

            var rs = response.GetResponseStream();
            var read=new StreamReader(rs);
            var content=read.ReadToEnd();
        }


        /// <summary>
        ///     用asp.net mvc创建一个网站用于测试,测试类HttpHelper
        /// </summary>
        [TestMethod]
        public void Test_302_By_HttpHelper()
        {
            var helper=new HttpHelper();
            var postData = "account=mike&password=123";
            var item=new HttpItem
            {
                URL=LoginUrl,
                Allowautoredirect=true,
                AutoRedirectCookie = true,
                Method = "Post",
                Postdata=postData,
                ContentType = "application/x-www-form-urlencoded",
                ProxyIp = "127.0.0.0:8888"//设置fiddler的代理地址，方便查看包数据
            };
            var result = helper.GetHtml(item);

            var content = result.Html;
        }

    }

}