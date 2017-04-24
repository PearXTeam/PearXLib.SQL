using MySql.Data.MySqlClient;

namespace PearXLib.SQL
{
	/// <summary>
	/// MySQL extensions.
	/// </summary>
	public static class SqlExtensions
	{
		/// <summary>
		/// Adds an escaped string to the ParameterCollection for using with LIKE request.
		/// </summary>
		/// <param name="p">Parameter collection.</param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public static void AddLikeEscaped(this MySqlParameterCollection p, string name, string value)
		{
			p.AddWithValue(name, SQLUtils.EscapePrepared(value));
		}
	}
}
