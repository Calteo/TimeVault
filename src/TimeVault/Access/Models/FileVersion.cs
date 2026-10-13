using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Models
{
	internal class FileVersion : DatabaseModel
	{		
		public string Path { get; set; } = "";
		public int Version { get; set; }
		public DateTime LastWrite { get; set; }
	}
}
