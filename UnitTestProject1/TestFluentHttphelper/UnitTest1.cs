using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1.TestFluentHttphelper
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var content = GetSyncSample();
        }
        public static string GetSyncSample()
        {
            var httpHelper = new FluentHttpHelper("http://www.globalcompanions.com/default.aspx");
            var request = httpHelper.HttpWebRequest;
            request.Referer = "http://www.globalcompanions.com/";

            using (var stream = httpHelper.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            var responese = httpHelper.HttpWebResponse;
        }
    }
}
