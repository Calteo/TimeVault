using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Models
{
	internal class Folder : DatabaseModel
	{
		public string Path { get; set; } = "";
	}
}
