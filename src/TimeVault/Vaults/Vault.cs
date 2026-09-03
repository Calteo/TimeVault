using Toolbox.Forms;

namespace TimeVault.Vaults;

/// <summary>
/// A vault is a folder that contains the storage for a time vault. 
/// </summary>
internal class Vault
{
	public string Folder { get; }
	public WorkerPool Pool { get; } = new WorkerPool();
	private Database Database { get; }

	public Vault(string folder)
	{
		Folder = folder;
		Database = new Database(folder);
	}
}