using System.Diagnostics;
using TimeVault.Access;
using TimeVault.Access.Models;
using TimeVault.Access.Tables;
using Toolbox.Forms;
using Toolbox.Settings;

namespace TimeVault.Vaults;

/// <summary>
/// A vault is a folder that contains the storage for a time vault. 
/// </summary>
internal class Vault
{
	public string Folder { get; }
	public WorkerPool Pool { get; } = new WorkerPool();
	private VaultDatabase Database { get; }

	public Vault(string folder)
	{
		Folder = folder;
		Database = new VaultDatabase(folder);
		Database.Open();

		foreach (var fileVersion in Database.FileVersions.SelectFileVersions())
		{
			FileVersions[fileVersion.Path] = fileVersion;
		}

		Pool.ItemStarted += PoolItemStarted;
		Pool.ItemStopped += PoolItemStopped;
		Pool.Started += PoolStarted;
		Pool.Stopped += PoolStopped;
	}

	private Dictionary<string, FileVersion> FileVersions { get; } = new(StringComparer.InvariantCultureIgnoreCase);

	private void PoolStarted(object? sender, EventArgs e)
	{	
	}

	private void PoolStopped(object? sender, EventArgs e)
	{
		if (_pendingStop)
			Running = false;
	}

	#region ActiveItems
	private int activeItems;
	/// <summary>
	/// Numbner of active work items in the worker pool. This is updated when work items are started and stopped.
	/// </summary>
	public int ActiveItems
	{
		get => activeItems;
	}

	private void IncreaseActiveItems()
	{
		Interlocked.Increment(ref activeItems);
		ActiveItemsChanged?.Invoke(this, EventArgs.Empty);
	}
	private void DecreaseActiveItems()
	{
		Interlocked.Decrement(ref activeItems);
		ActiveItemsChanged?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>
	/// Raised when the number of active work items in the worker pool changes.
	/// </summary>
	public event EventHandler? ActiveItemsChanged;
	#endregion

	private void PoolItemStopped(object? sender, WorkItemEventArgs e)
	{
		DecreaseActiveItems();
	}

	private void PoolItemStarted(object? sender, WorkItemEventArgs e)
	{
		IncreaseActiveItems();
	}

	public ExclusionTable Exclusions => Database.Exclusions;
	public SelectionTable Selections => Database.Selections;

	#region Running
	private bool _running;
	/// <summary>
	/// Is the worker pool running and processing work items.
	/// </summary>
	public bool Running
	{
		get => _running;
		private set
		{
			if (_running == value) return;
			_pendingStop = false;
			_running = value;
			RunningChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	/// <summary>
	/// Raised when the running state of the vault changes.
	/// </summary>
	public event EventHandler? RunningChanged;
	#endregion

	/// <summary>
	/// Starts the vault. This will start the worker pool and begin processing work items.
	/// </summary>
	public void Start()
	{
		Running = true;
		Pool.Enqueue(ScanVault);
	}

	private bool _pendingStop;

	/// <summary>
	/// Stops the vault. This will stop the worker pool and cancel any running work items.
	/// </summary>
	/// <remarks>
	/// If vault is running, it will set a pending stop flag and cancel the worker pool. 
	/// The vault will not be considered stopped until the worker pool has finished processing or canceled all work items.
	/// </remarks>	
	public void Stop()
	{
		if (Pool.Running)
		{
			_pendingStop = true;
			Pool.Cancel();
			return;
		}

		Running = false;
	}

	private void WorkerProgress(string name, int progress, object? state)
	{
	}

	/// <summary>
	/// Scans the vault for changed files and adds them to the work queue. 
	/// This method is initiated by the worker pool when it is started.
	/// </summary>
	private void ScanVault(IWorker worker)
	{
		foreach (var selection in Selections.SelectWhere("[Selected]=true"))
		{
			if (worker.IsCancelled) return;
			if (selection.IsDirectory)
			{
				var directoryInfo = new DirectoryInfo(selection.Path);
				if (directoryInfo.Exists)
					Pool.Enqueue<DirectoryInfo>(ScanDirectory, directoryInfo);
			}
			else
			{
				var fileInfo = new FileInfo(selection.Path);
				if (fileInfo.Exists)
					Pool.Enqueue<FileInfo>(ScanFile, fileInfo);
			}
		}
	}

	private void ScanDirectory(IWorker worker, DirectoryInfo directory)
	{
		if (worker.IsCancelled) return;

		foreach (var file in directory.EnumerateFiles())
		{
			if (worker.IsCancelled) return;
			if (Exclusions.IsExcluded(file)) continue;

			ScanFile(worker, file);
		}
		foreach (var subDirectory in directory.EnumerateDirectories())
		{
			if (worker.IsCancelled) return;
			if (Exclusions.IsExcluded(subDirectory)) continue;

			Pool.Enqueue<DirectoryInfo>(ScanDirectory, subDirectory);
		}
	}

	private void ScanFile(IWorker worker, FileInfo file)
	{
		if (worker.IsCancelled) return;
		
		if (!FileVersions.TryGetValue(file.FullName, out var fileVersion))
		{
			fileVersion = new FileVersion
			{
				Path = file.FullName,
			};
			Store(fileVersion, file);
			FileVersions[file.FullName] = fileVersion;
		}
		else if (file.LastWriteTimeUtc > fileVersion.LastWrite)
		{
			Store(fileVersion, file);
		}
	}

	private void Store(FileVersion fileVersion, FileInfo fileInfo)
	{
		var version = fileVersion.Version+1;
		
		var filePath = GetVaultFilePath(fileInfo, version);
		
		fileInfo.CopyTo(filePath, true);

		fileVersion.Version = version;
		fileVersion.LastWrite = fileInfo.LastWriteTimeUtc;

		Database.FileVersions.Insert(fileVersion);
	}

	private string GetVaultFilePath(FileInfo fileInfo, int version)
	{
		var filePath = Path.Combine(Folder, fileInfo.FullName[..1], fileInfo.FullName[3..]);

		var directory = Path.GetDirectoryName(filePath);
		if (!Directory.Exists(directory))
			Directory.CreateDirectory(directory!);

		filePath = Path.Combine(directory!, $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}.v{version}{fileInfo.Extension}");
		return filePath;
	}
}