using TimeVault.Access.Models;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Tables
{
	internal class ExclusionTable() : DatabaseTable<VaultDatabase, Exclusion>("Exclusion")
	{
		/// <summary>
		/// Insert patterns
		/// </summary>
		/// <param name="patterns"></param>
		/// <remarks>
		/// This method should only be called during <see cref="Database.Create(Database.VersionInfo)"/>
		/// of <see cref="Database.UpgradeFrom(int)"/>, since it only inserts direxty into the database
		/// and not updates the internal tables.
		/// </remarks>
		public void DirectInsert(bool directory, params string[] patterns)
		{
			if (patterns == null || patterns.Length == 0) return;

			using var connection = GetConnection();
			using var transaction = connection.BeginTransaction();

			var exclusions = patterns.Select(p => new Exclusion { IsDirectory = directory, Pattern = p });

			foreach (var exclusion in exclusions)
			{
				Insert(exclusion, transaction);
			}
			transaction.Commit();
		}

		bool _fetched = false;
		private List<Exclusion> _exclusions = [];
		private List<Exclusion> _fileExclusions = [];
		private List<Exclusion> _directoryExclusions = [];

		private void Fetch()
		{
			if (_fetched) return;

			_exclusions.AddRange(Select()); // fetch all database entries
			_directoryExclusions.AddRange(_exclusions.Where(e => e.IsDirectory));
			_fileExclusions.AddRange(_exclusions.Where(e => !e.IsDirectory));

			var vaultExclusion = new Exclusion { IsDirectory = true, Pattern = Database.Folder };
			_exclusions.Add(vaultExclusion);
			_directoryExclusions.Add(vaultExclusion);

			_fetched = true;
		}

		private List<Exclusion> Fetch(List<Exclusion> returnValue)
		{
			if (_fetched) return returnValue;

			Fetch(returnValue);	

			return returnValue;
		}

		internal void Modify(HashSet<Exclusion> added, HashSet<Exclusion> changed, HashSet<Exclusion> deleted)
		{
			using var connection = GetConnection();
			using var transaction = connection.BeginTransaction();

			foreach (var item in added)
			{
				Insert(item, transaction);
			}
			foreach (var item in changed)
			{
				Update(item, transaction);
			}
			foreach (var item in deleted)
			{
				Delete(item, transaction);
			}
			transaction.Commit();

			_exclusions.Clear();
			_fileExclusions.Clear();
			_directoryExclusions.Clear();
			_fetched = false;
		}

		internal bool IsExcluded(DirectoryInfo folder)
		{
			Fetch();
			// return _directoryExclusions.Any(e => e.Matches());
			return false;
		}

		public IEnumerable<Exclusion> Exclusions => Fetch(_exclusions);
	}
}