using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Proxy.Xun.DynamicForwarding
{
    /// <summary>
    /// 动态转发辅助
    /// </summary>
    public class ZDHelper
    {
        public static string AuthHeader(string orderno, string secret)
        {
            var timestamp = ConvertDateTimeInt(DateTime.Now);

            //拼装签名字符串
            var planText = $"orderno={orderno},secret={secret},timestamp={timestamp}";

            //计算签名
            var sign = GetMD5(planText);
            //拼装请求头Proxy-Authorization的值
            var authHeader = $"sign={sign}&orderno={orderno}&timestamp={timestamp}";
            return authHeader;
        }

        /// <summary>
        ///     生成10位时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int) (time - startTime).TotalSeconds;
        }

        /// <summary>
        ///     获取MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var encryptedBytes = md5.ComputeHash(Encoding.Default.GetBytes(str));
            var sb = new StringBuilder();
            for (var i = 0; i < encryptedBytes.Length; i++) sb.Append(encryptedBytes[i].ToString("x2"));
            return sb.ToString().ToUpper();
        }
    }
}