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
		public static List<Dictionary<string, string>> ExecuteListRows(this MySqlCommand cmd, bool autoclose = true)
		{
			List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
			using (var rdr = cmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					Dictionary<string, string> dict = new Dictionary<string, string>();
					for (int i = 0; i < rdr.FieldCount; i++)
					{
						try
						{
							dict.Add(rdr.GetName(i), rdr.GetString(i));
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
		public static Dictionary<string, List<string>> ExecuteListColumns(this MySqlCommand cmd, bool autoclose = true)
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
						try
						{
							dict[rdr.GetName(i)].Add(rdr.GetString(i));
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
		public static List<string> ExecuteSingleList(this MySqlCommand cmd, bool autoclose = true)
		{
			List<string> lst = new List<string>();
			using (var rdr = cmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					try
					{
						lst.Add(rdr.GetString(0));
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
