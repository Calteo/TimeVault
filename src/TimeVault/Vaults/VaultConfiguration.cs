using System;
using System.Collections.Generic;
using System.Text;
using Toolbox.Configuration;

namespace TimeVault.Vaults
{
	internal class VaultConfiguration : ConfigurationTreeItem<VaultConfiguration>
	{
		public VaultConfiguration(string key) : base(key)
		{
		}
	}
}
