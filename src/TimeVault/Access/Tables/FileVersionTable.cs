using Dapper;
using TimeVault.Access.Models;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Tables
{
	/// <summary>
	/// Virtual table that contains the file versions. 
	/// This table is not stored in the database, but is created by the tables "Files" and "Versions"
	/// </summary>
	internal class FileVersionTable() : DatabaseTable<VaultDatabase, Exclusion>("")
	{
		public IEnumerable<FileVersion> SelectFileVersions()
		{
			var sql = GetCommand(nameof(SelectFileVersions))
				?? throw new InvalidOperationException($"SQL command for {nameof(SelectFileVersions)} not found.");

			using var connection = GetConnection();
			return connection.Query<FileVersion>(sql);
		}

		public void Insert(FileVersion file)
		{
			using var connection = GetConnection();
			using var transaction = connection.BeginTransaction();

			if (file.Id == 0)
			{
				var sql = GetCommand("InsertFile")
					?? throw new InvalidOperationException($"SQL command for InsertFile not found.");				

				file.Id = connection.ExecuteScalar<long>(sql, file, transaction);

				sql = GetCommand("InsertVersion")
					?? throw new InvalidOperationException($"SQL command for InsertVersion not found.");
				
				connection.Execute(sql, file, transaction);
			}
			else
			{
				var sql = GetCommand("InsertVersion")
					?? throw new InvalidOperationException($"SQL command for InsertVersion not found.");

				connection.Execute(sql, file, transaction);
			}

			transaction.Commit();
		}
	}
}