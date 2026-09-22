using System.ComponentModel;
using TimeVault.Access.Models;
using TimeVault.Vaults;

namespace TimeVault.Forms
{
	internal partial class SelectionForm : Form
	{
		public SelectionForm()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required Vault Vault { get; init; }

		public HashSet<string> Deselected { get; private set; } = [];
		public HashSet<string> Selected { get; private set; } = [];

		public HashSet<string> HasSelection { get; private set; } = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		private void FolderSelectionLoad(object sender, EventArgs e)
		{
			var selections = Vault.Selections.Select().ToDictionary(s => s.Path);

			Selected = new HashSet<string>(selections.Values.Where(s => s.Selected).Select(s => s.Path), StringComparer.InvariantCultureIgnoreCase);
			Deselected = new HashSet<string>(selections.Values.Where(s => !s.Selected).Select(s => s.Path), StringComparer.InvariantCultureIgnoreCase);

			foreach (var selection in selections.Values)
			{
				var path = Path.GetDirectoryName(selection.Path);
				while (!string.IsNullOrEmpty(path))
				{
					if (!selections.ContainsKey(path)) HasSelection.Add(path);
					path = Path.GetDirectoryName(path);
				}
			}

			var ignore = new DriveType[] { DriveType.Removable, DriveType.Unknown, DriveType.CDRom, DriveType.NoRootDirectory }.ToHashSet();

			foreach (var drive in DriveInfo.GetDrives().Where(d => !ignore.Contains(d.DriveType)).OrderBy(d => d.Name))
			{
				var node = new TreeNodeFolder(null, new DirectoryInfo(drive.Name), this)
				{
					Text = $"{drive.Name} - {drive.VolumeLabel}",
					ImageKey = drive.DriveType == DriveType.Network ? "drives" : "drive"
				};
				node.SelectedImageKey = node.ImageKey;

				treeView.Nodes.Add(node);
			}
		}

		private void TreeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node is TreeNodeFolder node)
			{
				e.Cancel = node.Expanding(this);
			}
		}

		private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			listView.Items.Clear();
			if (e.Node is TreeNodeFolder node && node.State != SelectionState.Excluded)
			{				
				var items = node.Files.Select(f => new ListItemFile(listView, node, f)).ToArray();
				listView.Items.AddRange(items);
				foreach (ColumnHeader header in listView.Columns)
				{
					header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
				}
			}
		}

		private void TreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeViewHitTestInfo hit = treeView.HitTest(e.Location);

			NodeClicked = e.Node as TreeNodeFolder;

			if (e.Button == MouseButtons.Left && hit.Location == TreeViewHitTestLocations.StateImage && hit.Node is TreeNodeFolder folderNode)
			{
				switch (folderNode.State)
				{
					case SelectionState.Unselected:
						folderNode.State = SelectionState.Selected;
						break;
					case SelectionState.Selected:
						folderNode.State = SelectionState.Deselected;
						break;
					case SelectionState.SelectedParent:
						folderNode.State = SelectionState.Deselected;
						break;
					case SelectionState.DeselectedParent:
						folderNode.State = SelectionState.Selected;
						break;
					case SelectionState.Deselected: // --> Unselected
						if (folderNode.Parent is TreeNodeFolder parent)
						{
							switch (parent.State)
							{
								case SelectionState.Selected:
								case SelectionState.SelectedParent:
									folderNode.State = SelectionState.SelectedParent;
									break;
								case SelectionState.Deselected:
								case SelectionState.DeselectedParent:
									folderNode.State = SelectionState.DeselectedParent;
									break;
								default:
									folderNode.State = SelectionState.Unselected;
									break;
							}
						}
						else
							folderNode.State = SelectionState.Unselected;
						break;
				}
			}
		}

		private TreeNodeFolder? NodeClicked { get; set; }

		private void ContextMenuTreeOpening(object sender, CancelEventArgs e)
		{
			e.Cancel = NodeClicked == null;

			if (NodeClicked != null)
			{
				menuItemSelectFolder.Enabled = NodeClicked.State != SelectionState.Excluded;
				menuItemDeselectFolder.Enabled = NodeClicked.State != SelectionState.Excluded;
			}
		}

		private void MenuItemSelectFolderClick(object sender, EventArgs e)
		{
			if (NodeClicked == null) return;

			NodeClicked.State = SelectionState.Selected;
			UpdateChildNodes(NodeClicked, SelectionState.SelectedParent);

			NodeClicked = null;
		}

		private void MenuItemDeselectFolderClick(object sender, EventArgs e)
		{
			if (NodeClicked == null) return;

			NodeClicked.State = SelectionState.Deselected;
			UpdateChildNodes(NodeClicked, SelectionState.DeselectedParent);
			NodeClicked = null;
		}

		private void UpdateChildNodes(TreeNodeFolder node, SelectionState state)
		{
			foreach (var childNode in node.FolderNodes)
			{
				childNode.State = state;
				UpdateChildNodes(childNode, state);
			}
		}

		private List<Selection> Selections { get; } = [];

		private void ButtonOkClick(object sender, EventArgs e)
		{
			foreach (TreeNode node in treeView.Nodes)
			{
				CollectSelections(node);
			}

			Vault.Selections.Replace(Selections);
		}

		private void CollectSelections(TreeNode node)
		{
			if (node is TreeNodeFolder folderNode)
			{
				if (folderNode.State is SelectionState.Selected or SelectionState.Deselected)
				{
					AddSelection(folderNode);
				}
				if (folderNode.Mixed || folderNode.State is SelectionState.ContainsSelection)
				{
					foreach  (TreeNode childNode in folderNode.Nodes)
					{
						CollectSelections(childNode);
					}
				}
			}
			else if (node is TreeNodeExpanding expandingNode)
			{
				if (expandingNode.Parent is TreeNodeFolder parent)
				{
					foreach(var deselected in Deselected.Where(p => p.StartsWith(parent.Folder.FullName) && p.Length>parent.Folder.FullName.Length))
					{
						Selections.Add(new Selection { IsDirectory = true, Path = deselected, Selected = false }); 
					}
					foreach (var selected in Selected.Where(p => p.StartsWith(parent.Folder.FullName) && p.Length > parent.Folder.FullName.Length))
					{
						Selections.Add(new Selection { IsDirectory = true, Path = selected, Selected = true });
					}
				}
			}
		}

		private void AddSelection(TreeNodeFolder node)
		{
			Selections.Add(new Selection{ IsDirectory = true, Path = node.Folder.FullName, Selected = node.State==SelectionState.Selected });
		}
	}
}
