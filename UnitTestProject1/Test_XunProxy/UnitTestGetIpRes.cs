using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proxy.Xun;
using Proxy.Xun.DynamicForwarding;

namespace UnitTestProject1.Test_XunProxy
{
    [TestClass]
    public class UnitTestGetIpRes
    {
        [TestMethod]
        public void TestMethod1()
        {
            string _orderNo = "ZF20194121860i9NPpV";     //订单号
            string _secret = "6a6615efbfe34ec588feac669fcd6acd";         //secret

            var request = (HttpWebRequest) WebRequest.Create("http://www.xdaili.cn/usercenter");
            request.SetXunDynamicProxy(_orderNo,_secret);
            var response = request.GetResponse();
            var rd = new StreamReader(response.GetResponseStream());
            var content=rd.ReadToEnd();

            //string _requestRes =ZDHelper.GetIpRes(_orderNo, _secret);
            //Console.WriteLine(_requestRes);
        }

        [TestMethod]
        public void Test_Request()
        {

        }
    }
}
