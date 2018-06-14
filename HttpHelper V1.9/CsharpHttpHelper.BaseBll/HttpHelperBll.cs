using CsharpHttpHelper.Base;
using CsharpHttpHelper.Enum;
using CsharpHttpHelper.Helper;
using System;
using System.Drawing;

namespace CsharpHttpHelper.BaseBll
{
	internal class HttpHelperBll
	{
		private HttphelperBase httpbase = new HttphelperBase();

		internal HttpResult GetHtml(HttpItem item)
		{
			HttpResult result;
			if (item.Allowautoredirect && item.AutoRedirectCookie)
			{
				HttpResult httpResult = null;
				for (int i = 0; i < 100; i++)
				{
					item.Allowautoredirect = false;
					httpResult = this.httpbase.GetHtml(item);
					if (string.IsNullOrWhiteSpace(httpResult.RedirectUrl))
					{
						break;
					}
					item.URL = httpResult.RedirectUrl;
					item.Method = "GET";
					if (item.ResultCookieType == ResultCookieType.String)
					{
						item.Cookie += httpResult.Cookie;
					}
					else
					{
						item.CookieCollection.Add(httpResult.CookieCollection);
					}
				}
				result = httpResult;
			}
			else
			{
				result = this.httpbase.GetHtml(item);
			}
			return result;
		}

		internal Image GetImage(HttpItem item)
		{
			item.ResultType = ResultType.Byte;
			return ImageHelper.ByteToImage(this.GetHtml(item).ResultByte);
		}

		internal HttpResult FastRequest(HttpItem item)
		{
			return this.httpbase.FastRequest(item);
		}
	}
}
