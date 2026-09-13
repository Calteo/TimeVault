using TimeVault.Access;
using Toolbox.Configuration;
using Toolbox.Forms;

namespace TimeVault.Vaults;

/// <summary>
/// A vault is a folder that contains the storage for a time vault. 
/// </summary>
internal class Vault
{
	public string Folder { get; }
	public WorkerPool Pool { get; } = new WorkerPool();
	private VaultDatabase Database { get; }
	public ConfigurationTree<VaultConfiguration> Configurations { get; } = new();

	public Vault(string folder)
	{
		Folder = folder;
		Database = new VaultDatabase(folder);
		Database.Open();
	}
}