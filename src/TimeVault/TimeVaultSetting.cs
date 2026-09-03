using Toolbox.Settings;

namespace TimeVault
{
	[Location(Store.Local)]
	internal class TimeVaultSetting : Setting
	{
		public List<string> Vaults { get; set; } = [];
	}
}
