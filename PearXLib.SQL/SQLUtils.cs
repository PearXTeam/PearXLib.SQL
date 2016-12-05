namespace PearXLib.SQL
{
	/// <summary>
	/// MySQL Utils!
	/// </summary>
	public class SQLUtils
	{
		/// <summary>
		/// Makes string safe for MySQL requests.
		/// </summary>
		/// <returns>An input string.</returns>
		/// <param name="req">Formatted string.</param>
		public static string FormatRequest(string req)
		{
			return req.Replace("'", @"\'").Replace("\"", "\\\"").Replace("%", @"\%").Replace("\n", @"\n").Replace("_", @"\_");
		}
	}
}
