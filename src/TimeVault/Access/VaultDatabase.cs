using TimeVault.Access.Tables;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access
{
	internal class VaultDatabase : Database
	{
		public VaultDatabase(string folder)
			: base($@"{folder}\vault.db")
		{
			Exclusions = AddTable<ExcludeTable>();
			Folders = AddTable<FolderTable>();
		}

		public ExcludeTable Exclusions { get; }
		public FolderTable Folders { get; }
	}
}