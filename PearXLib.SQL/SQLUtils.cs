using System;
namespace PearXLib.SQL
{
	public class SQLUtils
	{
		public static string FormatRequest(string req)
		{
			return req.Replace("'", "''");
		}
	}
}
