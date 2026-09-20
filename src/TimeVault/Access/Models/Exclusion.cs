using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Toolbox.Dapper.SQLite;
using Toolbox.Dapper.SQLite.Attributes;

namespace TimeVault.Access.Models
{
	[DebuggerDisplay("{IsDirectory ? 'D' : 'F'} - {Pattern}")]
	internal partial class Exclusion : DatabaseModel, IDataErrorInfo
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
			set
			{
				if (SetField(ref _isDirectory, value)) return;
				CreateRegex();
			}
		}
		#endregion
		#region Pattern
		private string _patternError = "";
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
			set
			{
				if (SetField(ref _pattern, value)) return;
				CreateRegex();
			}
		}
		#endregion

		private Regex? _regex;
		[DbIgnore]
		public Regex Regex => _regex!;

		[GeneratedRegex(@"(?<two>\*\*)|(?<one>\*)|(?<single>\?)|(?<escape>[$()\[\].\\])")]
		private static partial Regex DirectoryReplacement();
		[GeneratedRegex(@"(?<one>\*)|(?<single>\?)|(?<escape>[$()\[\].])|(?<validate>.)")]
		private static partial Regex FileReplacement();

		private void CreateRegex()
		{
			if (Pattern.Length==0)
			{
				_pattern = "Pattern must contain value.";
				_regex = null;
				return;
			}

			try
			{
				var regexPattern = IsDirectory
					? DirectoryReplacement().Replace(Pattern, ReplaceDirectoryPattern) + @"(\\.*)?"
					: FileReplacement().Replace(Pattern, ReplaceFilePattern);

				_regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				_patternError = "";
			}
			catch (Exception exception)
			{
				_patternError = exception.Message;
			}
		}

		private string ReplaceFilePattern(Match match)
		{
			if (match.Groups["escape"].Success) return @"\" + match.Value;
			if (match.Groups["one"].Success) return @"[^\\]*";
			if (match.Groups["single"].Success) return @"[^\\]";
			if (match.Groups["validate"].Success)
			{
				if (Path.GetInvalidFileNameChars().Any(c => c == match.Value[0]))
					throw new Exception($"Illegal char '{match.Value}' in file pattern at index {match.Index}.");
			}

			return match.Value;
		}

		private string ReplaceDirectoryPattern(Match match)
		{
			if (match.Groups["escape"].Success) return @"\"+match.Value;
			if (match.Groups["two"].Success) return ".*";
			if (match.Groups["one"].Success) return @"[^\\]*";
			if (match.Groups["single"].Success) return @"[^\\]";

			return match.Value;
		}

		public bool IsMatch(string fullpath) => _regex!.IsMatch(fullpath);

		/// <summary>
		/// No errors on row
		/// </summary>
		public string Error => ""; // Pattern == "" ? "Pattern must be filled." : "";

		public string this[string columnName]
		{
			get
			{
				if (columnName != nameof(Pattern)) return "";

				return _patternError;
			}
		}		
	}
}