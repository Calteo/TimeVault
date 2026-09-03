using Dapper;
using Microsoft.Data.Sqlite;
using Toolbox;

namespace TimeVault.Vaults
{
	internal class Database
	{
		private const int CurrentVersion = 1;

		public Database(string folder)
		{
			Connection = new SqliteConnection($@"Data Source={folder}\vault.db");
			Connection.Open();

			var exists = Connection.ExecuteScalar<bool>("SELECT 1 FROM sqlite_schema WHERE type = 'table' AND name = 'Version'");
			Version = exists ? Upgrade() : Create();
		}
		
		public SqliteConnection Connection { get; }

		private Version Create()
		{
			var sql = GetType().GetRessourceString("vault_create.sql");
			Connection.Execute(sql);

			var version = new Version { Id = CurrentVersion, CreateAt = DateTime.Now, Comment = "Initial version" };
			
			var sqlInsert = GetType().GetRessourceString("version_insert.sql");
			var affected = Connection.Execute(sqlInsert, version);

			return version;
		}

		public Version Version { get; set; }

		private Version Upgrade()
		{
			var sql = GetType().GetRessourceString("version_select.sql");

			var version = Connection.QueryFirst<Version>(sql);

			// Upgrade logic can be implemented here based on the current version
			if (version.Id != CurrentVersion)
				throw new InvalidOperationException("Database version mismatch. Please upgrade the database.");

			return version;
		}
	}
}