using TimeVault.Access.Tables;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access
{
	internal class VaultDatabase : Database
	{
		public VaultDatabase(string folder)
			: base($@"{folder}\vault.db")
		{
			Exclusions = AddTable<ExclusionTable>();
			Folders = AddTable<FolderTable>();
		}

		public ExclusionTable Exclusions { get; }
		public FolderTable Folders { get; }

		protected override void Create(VersionInfo version)
		{
			base.Create(version);

			// Exculde folders
			Exclusions.DirectInsert(true,				
				Environment.GetFolderPath(Environment.SpecialFolder.Windows),
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
				Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles),
				Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86)
				);
			// Exclude files
			Exclusions.DirectInsert(false,
				"*.log",
				"*.tmp",
				"*.temp"
				);
		}
	}
}