using TimeVault.Forms;
using Toolbox.CommandLine;

namespace TimeVault
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();

			var parser = Parser.Create<Options>();
			var result = parser.Parse(args);
			result
				.OnError(HandleParserError)
				.OnHelp(HandleParserHelp)
				.On<Options>(Run);		
		}

		private static int Run(Options options)
		{
			Application.Run(new TimeVaultContext(options));

			return 0;
		}

		private static int HandleParserHelp(ParseResult result)
		{
			MessageBox.Show(result.GetHelpText(), "TimeVault - Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
			return 0;
		}

		private static int HandleParserError(ParseResult result)
		{
			MessageBox.Show(result.Text, "TimeVault - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return 0;
		}

	}
}