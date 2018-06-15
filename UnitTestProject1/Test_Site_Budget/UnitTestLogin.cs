using System;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.Test_Site_Budget
{
    [TestClass]
    public class UnitTestLogin
    {
        [TestMethod]
        public void Test_login()
        {
            var helper=new HttpHelper();
            var item = new HttpItem
            {
                URL = "http://site-budgetstatistics3.jjsoft.cn",
            };
            var result = helper.GetHtml(item);
            item = new HttpItem
            {
                URL = "http://site-budgetstatistics3.jjsoft.cn/AllUser/LoginSubmitJson",
                Postdata = "AccountName=admin&Password=123456",
                Method = "Post",
                Cookie = result.Cookie,
                ContentType = "application/x-www-form-urlencoded"
            };
            result=helper.GetHtml(item);
        }
    }
}
