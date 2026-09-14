using System.ComponentModel;
using System.Diagnostics;
using Toolbox.Dapper.SQLite;
using Toolbox.Dapper.SQLite.Attributes;

namespace TimeVault.Access.Models
{
	internal class Exclusion : DatabaseModel, IDataErrorInfo
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
		#region Pattern
		private string _pattern = "";
		/// <summary>
		/// Pattern to exclude.
		/// </summary>
		/// <remarks>
		/// The options in the pattern vary depending on <see cref="IsDirectory"/>.
		/// </remarks>
		public string Pattern
		{
			get => _pattern;
			set => SetField(ref _pattern, value);
		}

		/// <summary>
		/// No errors on row
		/// </summary>
		public string Error => ""; // Pattern == "" ? "Pattern must be filled." : "";

		public string this[string columnName]
		{
			get
			{
				if (columnName != nameof(Pattern)) return "";

				if (string.IsNullOrEmpty(Pattern)) return "Pattern must contain value.";

				return "";
			}
		}
		#endregion
	}
}