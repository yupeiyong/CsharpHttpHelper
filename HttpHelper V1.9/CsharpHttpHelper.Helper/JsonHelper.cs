using System;
using System.Web.Script.Serialization;

namespace CsharpHttpHelper.Helper
{
	internal class JsonHelper
	{
		internal static object JsonToObject<T>(string jsonstr)
		{
			object result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				result = javaScriptSerializer.Deserialize<T>(jsonstr);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		internal static string ObjectToJson(object obj)
		{
			string result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				result = javaScriptSerializer.Serialize(obj);
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
