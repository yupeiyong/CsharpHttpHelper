using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using CsharpHttpHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1.Test_Globalcompanions
{

    /// <summary>
    ///     测试globalcompanions网站
    /// </summary>
    [TestClass]
    public class UnitTestGlobalcompanions
    {

        [TestClass]
        public class UnitLogin
        {

            [TestMethod]
            public void Test_Login()
            {
                var ladie = new OrientLadie();
                ladie.UserName = "1352091";
                ladie.Password = "888";
                ladie.Login2();
            }


            [TestMethod]
            public void Test_Home_Is_Allowautoredirect()
            {
                var helper = new HttpHelper();
                var str = "http://www.globalcompanions.com/";

                var header = new WebHeaderCollection();
                header.Add("Accept-Encoding:gzip,deflate");
                var item = new HttpItem
                {
                    URL = str,
                    Allowautoredirect = true,
                    AutoRedirectCookie = false
                };

                var result = helper.GetHtml(item);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("登录失败(" + result.StatusCode + ")");
                }
            }


            [TestMethod]
            public void Test_Home_Is_Allowautoredirect_And_AutoRedirectCookie()
            {
                var helper = new HttpHelper();
                var str = "http://www.globalcompanions.com/";

                var header = new WebHeaderCollection();
                header.Add("Accept-Encoding:gzip,deflate");
                var item = new HttpItem
                {
                    URL = str,
                    Allowautoredirect = true,
                    AutoRedirectCookie = false
                };

                var result = helper.GetHtml(item);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("登录失败(" + result.StatusCode + ")");
                }
            }

            [TestMethod]
            public void Test_Home_Not_Allowautoredirect()
            {
                var helper = new HttpHelper();
                var str = "http://www.globalcompanions.com/";

                var header = new WebHeaderCollection();
                header.Add("Accept-Encoding:gzip,deflate");
                var item = new HttpItem
                {
                    URL = str,
                    Allowautoredirect = false,
                    AutoRedirectCookie = false
                };

                var result = helper.GetHtml(item);
                item = new HttpItem
                {
                    URL = result.RedirectUrl,
                    Cookie = result.Cookie
                };
                result = helper.GetHtml(item);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("登录失败(" + result.StatusCode + ")");
                }

            }
        }


        public class OrientLadie
        {

            public bool bContactFinish;
            public bool bSoulmateSearchStart = true;
            public object LockUpdateMessageToken = new object();
            public int nContactIndex;
            public int nCurSendIndex;
            public int nMsgIndex;
            public int SendNumber;
            public bool State;


            public string EveryoneMessageToken { get; set; }

            public string AttentionsToken { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }

            public bool IsLogined { get; set; }

            public string Cookie { get; set; }

            public string Id { get; set; }


            private string GetLoginResult(string html)
            {
                if (html.IndexOf("<input type=\"submit\" name=\"ctl00$Header$cntrlLogin$btnLogout\" value=\"Logout\"", StringComparison.Ordinal) >= 0)
                {
                    var match = Regex.Match(html, "(?<=<span id=\"Header_cntrlLogin_lblLogin\" class=\"login-number\">)(.+?)(?=</span>)");
                    if (match.Success)
                    {
                        Id = match.Value;
                    }
                    return "登录成功";
                }
                if (html.IndexOf("<span id=\"Header_cntrlLogin_lblErrorMsg\" class=\"error\">Incorrect login or password</span>") >= 0)
                {
                    return "账号或密码错误";
                }
                return "登录失败,未知错误";
            }


            private LoginParam GetPararm(string html)
            {
                if (html == "")
                {
                    return null;
                }
                LoginParam param = new LoginParam();
                var match = Regex.Match(html, "(?<=id=\"__VIEWSTATE\" value=\")(.+?)(?=\" />)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                param.__VIEWSTATE = match.Value;
                if (param.__VIEWSTATE == "")
                {
                    return null;
                }
                match = Regex.Match(html, "(?<=id=\"__VIEWSTATEGENERATOR\" value=\")(.+?)(?=\" />)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                param.__VIEWSTATEGENERATOR = match.Value;
                if (param.__VIEWSTATEGENERATOR == "")
                {
                    return null;
                }
                return param;
            }



            public string Login()
            {
                try
                {
                    var helper = new HttpHelper();
                    var str = "http://www.globalcompanions.com/";

                    var header = new WebHeaderCollection();
                    header.Add("Accept-Encoding:gzip,deflate");
                    var item = new HttpItem
                    {
                        URL = str,
                        Allowautoredirect = false,
                        AutoRedirectCookie = false
                    };

                    var result = helper.GetHtml(item);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        return ("登录失败(" + result.StatusCode + ")");
                    }

                    LoginParam pararm = GetPararm(result.Html);
                    if (pararm == null)
                    {
                        return "登录失败(LoginParam failed)";
                    }
                    pararm.ctl00_Header_cntrlLogin_txtBoxLogin = UserName;
                    pararm.ctl00_Header_cntrlLogin_txtBoxPassword = Password;

                    var loginParameters = pararm.ToPostData();
                    var pBuffers = Encoding.UTF8.GetBytes(loginParameters);

                    var request = (HttpWebRequest) WebRequest.Create("http://www.globalcompanions.com/default.aspx");
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";
                    request.Referer = "http://www.globalcompanions.com/";
                    request.AllowAutoRedirect = true;

                    request.Method = "Post";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = pBuffers.Length;
                    var requestStream = request.GetRequestStream();
                    var c = new CookieContainer();
                    request.CookieContainer = c;
                    requestStream.Write(pBuffers, 0, pBuffers.Length);
                    requestStream.Close();

                    var response = (HttpWebResponse) request.GetResponse();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return ("登录失败(" + response.StatusCode + ")");
                    }
                    var cookies = response.Headers.Get("Set-Cookie");

                    //request = (HttpWebRequest)WebRequest.Create(url);
                    //request.CookieContainer = new CookieContainer();
                    //request.CookieContainer.SetCookies(request.RequestUri, cookies);
                    response = (HttpWebResponse) request.GetResponse();

                    var rStream = response.GetResponseStream();
                    var r = new StreamReader(rStream);
                    var content = r.ReadToEnd();

                    var loginResult = GetLoginResult(content);
                    if (loginResult.IndexOf("登录成功", StringComparison.Ordinal) >= 0)
                    {
                        IsLogined = true;
                        Cookie = item.Cookie;
                        if (result.Cookie == null)
                        {
                            return loginResult;
                        }
                        if (result.Cookie == "")
                        {
                            return loginResult;
                        }
                        Cookie = Cookie + ";" + result.Cookie;
                    }
                    return loginResult;
                }
                catch (Exception exception)
                {
                    return ("登录失败:" + exception.Message);
                }
            }


            public string Login2()
            {
                try
                {
                    var helper = new HttpHelper();
                    var str = "http://www.globalcompanions.com/";

                    var header = new WebHeaderCollection();
                    header.Add("Accept-Encoding:gzip,deflate");
                    var item = new HttpItem
                    {
                        URL = str,
                        Allowautoredirect = true,
                        AutoRedirectCookie = true
                    };

                    var result = helper.GetHtml(item);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        return ("登录失败(" + result.StatusCode + ")");
                    }

                    LoginParam pararm = GetPararm(result.Html);
                    if (pararm == null)
                    {
                        return "登录失败(LoginParam failed)";
                    }
                    pararm.ctl00_Header_cntrlLogin_txtBoxLogin = UserName;
                    pararm.ctl00_Header_cntrlLogin_txtBoxPassword = Password;

                    item = new HttpItem
                    {
                        URL = "http://www.globalcompanions.com/default.aspx",
                        Referer = "http://www.globalcompanions.com/",
                        Allowautoredirect = true,
                        AutoRedirectCookie = true,
                        Method = "POST",
                        Header = header,
                        Postdata = pararm.ToPostData(),
                        Cookie = result.Cookie,
                        UserAgent = "Mozilla/5.0 (compatible;Windows NT 6.1; WOW64;Trident/6.0;MSIE 9.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.27 Safari/537.36",
                        ContentType = "application/x-www-form-urlencoded"
                    };
                    result = helper.GetHtml(item);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        return ("登录失败(" + result.StatusCode + ")");
                    }

                    var loginResult = GetLoginResult(result.Html);
                    if (loginResult.IndexOf("登录成功", StringComparison.Ordinal) >= 0)
                    {
                        IsLogined = true;
                        Cookie = item.Cookie;
                        if (result.Cookie == null)
                        {
                            return loginResult;
                        }
                        if (result.Cookie == "")
                        {
                            return loginResult;
                        }
                        Cookie = Cookie + ";" + result.Cookie;
                    }
                    return loginResult;
                }
                catch (Exception exception)
                {
                    return ("登录失败:" + exception.Message);
                }
            }


            public bool SendLetter(long TargetId, string Letter)
            {
                return true;
            }

        }

    }

}