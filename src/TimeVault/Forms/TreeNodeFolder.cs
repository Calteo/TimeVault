namespace TimeVault.Forms
{
	internal class TreeNodeFolder : TreeNode
	{
		public TreeNodeFolder(DirectoryInfo folder)
		{
			Folder = folder;

			Text = folder.Name;
			ImageKey = "folder";
			SelectedImageKey = ImageKey;
											
			if (ChildFolders.Any())
				Nodes.Add(new TreeNodeExpanding());
		}

		public DirectoryInfo Folder { get; }
		public IEnumerable<DirectoryInfo> ChildFolders
		{
			get
			{
				return Folder.EnumerateDirectories()
						.Where(d => 
						!d.Attributes.HasFlag(FileAttributes.Hidden)
						&& !d.Attributes.HasFlag(FileAttributes.System)
						&& CanReadDirectory(d));
			}
		}


		private bool CanReadDirectory(DirectoryInfo directory)
		{
			try
			{
				using var enumerator = directory.EnumerateFileSystemInfos().GetEnumerator();
				enumerator.MoveNext(); // Force access check
				return true;
			}
			catch (UnauthorizedAccessException)
			{
				return false;
			}
		}

		public bool Expanding()
		{
			if (Nodes.Count==1 && Nodes[0] is TreeNodeExpanding)
			{
				Nodes.Clear();
				foreach (var folder in ChildFolders)
				{
					Nodes.Add(new TreeNodeFolder(folder));
				}
			}
			return Nodes.Count == 0;
		}
	}
}
