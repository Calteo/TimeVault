using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Models
{
	internal class Exclude : DatabaseModel
	{
		/// <summary>
		/// The pattern to exclude
		/// </summary>
		public string Pattern { get; set; } = "";
	}
}
