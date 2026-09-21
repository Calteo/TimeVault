using System.Diagnostics;
using Toolbox.Collection.Generics;

namespace TimeVault.Forms
{
	[DebuggerDisplay("{Text,nq} - {State,nq}")]
	internal class TreeNodeFolder : TreeNode
	{
		private SelectionState _state;

		public TreeNodeFolder(TreeNodeFolder? parent, DirectoryInfo folder, SelectionForm form)
		{
			Parent = parent;
			Folder = folder;
			Form = form;

			Text = folder.Name;
			ImageKey = "folder";
			SelectedImageKey = ImageKey;
			StateImageKey = State.ToString();			

			if (form.Selected.Contains(Folder.FullName)) State = SelectionState.Selected;
			else if (form.Deselected.Contains(Folder.FullName)) State = SelectionState.Deselected;
			else if (form.HasSelection.Contains(Folder.FullName)) State = SelectionState.ContainsSelection;
			else if (form.Vault.Exclusions.IsExcluded(Folder)) State = SelectionState.Excluded;

			if (_state != SelectionState.Excluded)
			{
				ChildFolders =
					[.. Folder.EnumerateDirectories()
						.Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden)
							&& !d.Attributes.HasFlag(FileAttributes.System)
							&& CanReadDirectory(d))
					];

				if (ChildFolders.Any())
					Nodes.Add(new TreeNodeExpanding());
			}
			else
			{
				ChildFolders = [];
			}
			Mixed = Form.Selected.Any(p => p.StartsWith(Folder.FullName)) && Form.Deselected.Any(p => p.StartsWith(Folder.FullName));
		}

		public SelectionState State
		{
			get => _state;
			set
			{
				if (_state == value) return;
				_state = value;
				StateImageKey = _state.ToString();
				ForeColor = DefaultForeColor;

				_updatingState = true;

				Parent?.UpdatedChild();
				FolderNodes.ForEach(n => n.UpdatedParent());

				_updatingState = false;
			}
		}

		private Color DefaultForeColor => State == SelectionState.Excluded ? SystemColors.GrayText : SystemColors.WindowText;

		private bool _updatingState;
		
		private void UpdatedChild()
		{
			if (_updatingState) return;

			var childNodes = FolderNodes.ToArray();

			switch (State)
			{
				case SelectionState.Unselected:
				case SelectionState.ContainsSelection:
					State = childNodes.Any(n => n.State != SelectionState.Unselected)
						? SelectionState.ContainsSelection
						: SelectionState.Unselected;
					break;
			}

			Mixed = Nodes.OfType<TreeNodeFolder>()
						.Where(n => n.State != SelectionState.Excluded)
						.GroupBy(n => n.State).Skip(1).Any();

			Parent?.UpdatedChild();
		}

		private void UpdatedParent()
		{
			if (_updatingState || State is SelectionState.Excluded or SelectionState.Selected or SelectionState.Deselected) return;

			if (Parent!.State is SelectionState.Deselected or SelectionState.DeselectedParent)
			{
				State = SelectionState.DeselectedParent;
			}
			else if (Parent!.State is SelectionState.Selected or SelectionState.SelectedParent)
			{
				State = SelectionState.SelectedParent;
			}
			else if (Parent!.State is SelectionState.Unselected)
			{
				State = SelectionState.Unselected;
			}
		}

		public new SelectionForm Form { get; }
		public new TreeNodeFolder? Parent { get; }
		public DirectoryInfo Folder { get; }
		public IEnumerable<DirectoryInfo> ChildFolders { get; }
		public IEnumerable<TreeNodeFolder> FolderNodes => Nodes.OfType<TreeNodeFolder>().Where(n => n.State != SelectionState.Excluded);

		#region Mixed
		private bool _mixed;
		public bool Mixed
		{
			get => _mixed;
			private set
			{
				_mixed = value;

				if (Mixed && State is SelectionState.Selected or SelectionState.Deselected)
				{
					ToolTipText = "Mixed selections.";
					NodeFont = new Font(Form.Font, FontStyle.Italic);
				}
				else
				{
					ToolTipText = "";
					NodeFont = null;
				}
			}
		}
		#endregion

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

		public bool WasExpanded => Nodes.Count < 1 || Nodes[0] is not TreeNodeExpanding;

		public bool Expanding(SelectionForm form)
		{
			if (!WasExpanded)
			{
				_updatingState = true; // prevent partial updates

				Nodes.Clear();
				foreach (var folder in ChildFolders)
				{
					var node = new TreeNodeFolder(this, folder, form);
					Nodes.Add(node);
					node.UpdatedParent();
				}
				_updatingState = false;

				UpdatedChild();
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
