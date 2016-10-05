using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PearXLib.SQL
{
	public static class SQLExtensions
	{
		public static List<Dictionary<string, string>> ExecuteListRows(this MySqlCommand cmd)
		{
			List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
			using (var rdr = cmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					Dictionary<string, string> dict = new Dictionary<string, string>();
					for (int i = 0; i < rdr.FieldCount; i++)
					{
						dict.Add(rdr.GetName(i), rdr.GetString(i));
					}
					lst.Add(dict);
				}
			}
			return lst;
		}

		public static Dictionary<string, List<string>> ExecuteListColumns(this MySqlCommand cmd)
		{
			Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

			using (var rdr = cmd.ExecuteReader())
			{
				for (int i = 0; i < rdr.FieldCount; i++)
				{
					dict.Add(rdr.GetName(i), new List<string>());
				}
				while (rdr.Read())
				{
					for (int i = 0; i < rdr.FieldCount; i++)
					{
						dict[rdr.GetName(i)].Add(rdr.GetString(i));
					}
				}
			}
			return dict;
		}

		public static List<string> ExecuteSingleList(this MySqlCommand cmd)
		{
			List<string> lst = new List<string>();
			using (var rdr = cmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					lst.Add(rdr.GetString(0));
				}
			}
			return lst;
		}
	}
}
