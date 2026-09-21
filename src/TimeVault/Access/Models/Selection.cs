using System.Diagnostics;
using Toolbox.Dapper.SQLite;
using Toolbox.Dapper.SQLite.Attributes;

namespace TimeVault.Access.Models
{
	[DebuggerDisplay("{IsDirectory ? 'D' : 'F',nq} {Selected}: {Path}")]
	internal class Selection : DatabaseModel
	{
		#region IsDirectory
		private bool _isDirectory;
		/// <summary>
		/// Marks the exclusion for a directory
		/// </summary>
		[DbColumn("Directory")]
		public bool IsDirectory
		{
			get => _isDirectory;
			set => SetField(ref _isDirectory, value);
		}
		#endregion
		#region Path
		private string _path = "";
		public string Path
		{
			get => _path;
			set => SetField(ref _path, value);
		}
		#endregion

		#region Selected
		private bool _selected;
		public bool Selected
		{
			get => _selected;
			set => SetField(ref _selected, value);
		}
		#endregion
	}
}
