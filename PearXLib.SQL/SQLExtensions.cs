using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PearXLib.SQL
{
	/// <summary>
	/// MySQL extensions.
	/// </summary>
	public static class SQLExtensions
	{
		/// <summary>
		/// Executes a command and gets a list of rows.
		/// </summary>
		/// <returns>A list of rows.</returns>
		/// <param name="cmd">Command.</param>
		public static List<Dictionary<string, object>> ExecuteListRows(this MySqlCommand cmd, bool autoclose = true)
		{
			List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
			using (var rdr = cmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					Dictionary<string, object> dict = new Dictionary<string, object>();
					for (int i = 0; i < rdr.FieldCount; i++)
					{
						try
						{
							dict.Add(rdr.GetName(i), rdr.GetValue(i));
						}
						catch
						{
							dict.Add(rdr.GetName(i), null);
						}
					}
					lst.Add(dict);
				}
				rdr.Close();
			}
			if (autoclose)
				cmd.Connection.Close();
			return lst;
		}

		/// <summary>
		/// Executes a command and gets a list of columns.
		/// </summary>
		/// <returns>A list of columns.</returns>
		/// <param name="cmd">Command.</param>
		public static Dictionary<string, List<object>> ExecuteListColumns(this MySqlCommand cmd, bool autoclose = true)
		{
			Dictionary<string, List<object>> dict = new Dictionary<string, List<object>>();

			using (var rdr = cmd.ExecuteReader())
			{
				for (int i = 0; i < rdr.FieldCount; i++)
				{
					dict.Add(rdr.GetName(i), new List<object>());
				}
				while (rdr.Read())
				{
					for (int i = 0; i < rdr.FieldCount; i++)
					{
						try
						{
							dict[rdr.GetName(i)].Add(rdr.GetValue(i));
						}
						catch
						{
							dict[rdr.GetName(i)].Add(null);
						}
					}
				}
				rdr.Close();
			}
			if (autoclose)
				cmd.Connection.Close();
			return dict;
		}

		/// <summary>
		/// Executes a command and gets a single list.
		/// </summary>
		/// <returns>A single list.</returns>
		/// <param name="cmd">Command.</param>
		public static List<object> ExecuteSingleList(this MySqlCommand cmd, bool autoclose = true)
		{
			List<object> lst = new List<object>();
			using (var rdr = cmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					try
					{
						lst.Add(rdr.GetValue(0));
					}
					catch
					{
						lst.Add(null);
					}
				}
				rdr.Close();
			}
			if (autoclose)
				cmd.Connection.Close();
			return lst;
		}
	}
}
