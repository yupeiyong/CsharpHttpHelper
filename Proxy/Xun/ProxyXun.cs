using System;
using System.Collections.Generic;
using System.Net;
using CsharpHttpHelper;
using Newtonsoft.Json;
using Proxy.Xun.DynamicForwarding;

namespace Proxy.Xun
{
    /// <summary>
    ///     讯代理
    /// </summary>
    public static class ProxyXun
    {
        private const string XunProxyIp = "forward.xdaili.cn";
        private const int XunProxyPort = 80;

        private static readonly Dictionary<string, string> _responseErrors = new Dictionary<string, string>
        {
            {"auth fail, no orderno", "没有订单号，需要核对下订单号是否正确，以及是否跟secret是同一个账号"},
            {"The number of requests exceeds the limit", "提取数量超过限制，请续费"},
            {"session expired,please check out your timestamp", "时间戳取10位，并且核对是否与当前时间超过1天"},
            {"auth fail, invalid status", "订单异常被冻结"},
            {"auth fail, no secret", "请前往个人中心更换下secret"},
            {"auth fail, sign error", "请参照示例代码进行生成sign，生成sign用的参数需要跟传给我们的参数保持一致"},
            {"Concurrent number exceeds limit", "订单每秒并发数超过限制,建议减少并发数,可以减少这个错误的出现次数"},
            {
                "auth fail, no auth header",
                "1.是请求头没有根据要求设置 2.是因为请求需要重定向的url但是本身用的包使用代理自动重定向请求的时候会丢失hearder，这个时候就需要用户,禁止重定向，然后根据返回的状态码301/302的时候，从响应头的Location中获取新的请求url"
            }
        };

        /// <summary>
        ///     设置讯代理的动态转发
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orderNo">订单号</param>
        /// <param name="secret">密码</param>
        public static void SetXunDynamicProxy(this HttpItem item, string orderNo, string secret)
        {
            item.Header.Add(HttpRequestHeader.ProxyAuthorization, ZDHelper.AuthHeader(orderNo, secret));
            item.WebProxy = new WebProxy(XunProxyIp, XunProxyPort);
        }

        /// <summary>
        ///     设置讯代理的动态转发
        /// </summary>
        /// <param name="request"></param>
        /// <param name="orderNo">订单号</param>
        /// <param name="secret">密码</param>
        public static void SetXunDynamicProxy(this HttpWebRequest request, string orderNo, string secret)
        {
            request.Headers.Add(HttpRequestHeader.ProxyAuthorization, ZDHelper.AuthHeader(orderNo, secret));
            request.Proxy = new WebProxy(XunProxyIp, XunProxyPort);
        }

        public static bool Check(string orderNo, string secret,out string error)
        {
            error = string.Empty;
            var httpItem = new HttpItem
            {
                URL = "http://www.xdaili.cn/"
            };
            var helper = new HttpHelper();
            var result = helper.GetHtml(httpItem);
            var content = result.Html;

            try
            {
                var res = JsonConvert.DeserializeObject<XunResponse>(content);
                var msg = res.msg.Trim();
                if (_responseErrors.ContainsKey(msg))
                    error= _responseErrors[msg];

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private class XunResponse
        {
            public int code { get; set; }

            public string msg { get; set; }
        }
    }
}