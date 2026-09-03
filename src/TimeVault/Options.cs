using System.ComponentModel;
using Toolbox.CommandLine;

namespace TimeVault
{
	internal class Options
	{
		[Option("show"), Description("Open the application window.")]
		public bool Show { get; set; }
	}
}