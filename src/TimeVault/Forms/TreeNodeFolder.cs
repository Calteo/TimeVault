namespace TimeVault.Forms
{
	internal class TreeNodeFolder : TreeNode
	{
		public TreeNodeFolder(TreeNodeFolder? parent, DirectoryInfo folder, SelectionForm form)
		{
			Parent = parent;
			Folder = folder;

			Text = folder.Name;
			ImageKey = "folder";
			SelectedImageKey = ImageKey;

			StateImageKey = KeyUnselected;
			if (Parent != null)
			{
				if (Parent.StateImageKey==KeyIncluded || Parent.StateImageKey==KeyIncludedParent) 
					StateImageKey = KeyIncludedParent;
			}

			if (form.Included.Contains(Folder.FullName)) StateImageKey = TreeNodeFolder.KeyIncluded;
			else if (form.Excluded.Contains(Folder.FullName)) StateImageKey = TreeNodeFolder.KeyExcluded;
			else if (form.HasSelection.Contains(Folder.FullName)) StateImageKey = TreeNodeFolder.KeyHasSelection;

			ChildFolders = 
				[.. Folder.EnumerateDirectories()
						.Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden)
							&& !d.Attributes.HasFlag(FileAttributes.System)
							&& CanReadDirectory(d)
							&& !form.Vault.Exclusions.IsExcluded(Folder))
				];

			if (ChildFolders.Any())
				Nodes.Add(new TreeNodeExpanding());
		}

		public const string KeyUnselected = "unselected";
		public const string KeyIncluded = "included";
		public const string KeyExcluded = "excluded";
		public const string KeyIncludedParent = "included-parent";
		public const string KeyHasSelection = "has-selection";

		public new TreeNodeFolder? Parent { get; }
		public DirectoryInfo Folder { get; }
		public IEnumerable<DirectoryInfo> ChildFolders { get; }	

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

		public bool Expanding(SelectionForm form)
		{
			if (Nodes.Count==1 && Nodes[0] is TreeNodeExpanding)
			{
				Nodes.Clear();
				foreach (var folder in ChildFolders)
				{
					Nodes.Add(new TreeNodeFolder(this, folder, form));
				}
			}
			return Nodes.Count == 0;
		}

		public IEnumerable<FileInfo> Files
		{
			get
			{
				return Folder.EnumerateFiles()
						.Where(f =>
						!f.Attributes.HasFlag(FileAttributes.Hidden)
						&& !f.Attributes.HasFlag(FileAttributes.System));
			}
		}
	}
}
