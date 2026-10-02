using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TimeVault.Access.Models;
using TimeVault.Vaults;
using Toolbox.CommandLine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

		public HashSet<string> DeselectedFiles { get; private set; } = [];
		public HashSet<string> SelectedFiles { get; private set; } = [];


		public HashSet<string> HasSelection { get; private set; } = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		private void FolderSelectionLoad(object sender, EventArgs e)
		{
			var selectionGroups = Vault.Selections.Select().GroupBy(s => s.IsDirectory);

			foreach (var selectionGroup in selectionGroups)
			{
				var selected = selectionGroup.Where(s => s.Selected);
				var deselected = selectionGroup.Where(s => !s.Selected);

				if (selectionGroup.Key) // Directories
				{
					Selected = selected.Select(s => s.Path).ToHashSet(StringComparer.InvariantCultureIgnoreCase);
					Deselected = deselected.Select(s => s.Path).ToHashSet(StringComparer.InvariantCultureIgnoreCase);

					foreach (var selection in selectionGroup)
					{
						FillHasSelection(Path.GetDirectoryName(selection.Path));
					}
				}
				else // Files
				{
					SelectedFiles = selected.Select(s => s.Path).ToHashSet(StringComparer.InvariantCultureIgnoreCase);
					DeselectedFiles = deselected.Select(s => s.Path).ToHashSet(StringComparer.InvariantCultureIgnoreCase);

					foreach (var fileSelection in selectionGroup)
					{
						FillHasSelection(Path.GetDirectoryName(fileSelection.Path));
					}
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

		private void FillHasSelection(string? path)
		{
			while (!string.IsNullOrEmpty(path))
			{
				if (!Selected.Contains(path) && !Deselected.Contains(path)) HasSelection.Add(path);
				path = Path.GetDirectoryName(path);
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
			if (e.Node is TreeNodeFolder node)
				UpdateListView(node);
		}

		private void UpdateListView(TreeNodeFolder? node)
		{
			listView.Items.Clear();

			if (node != null && node.State != SelectionState.Excluded)
			{
				var items = node.Files.Select(f => new ListItemFile(listView, node, f, this)).ToArray();
				listView.Items.AddRange(items);

				var resize = items.Length == 0 ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent;
				foreach (ColumnHeader header in listView.Columns)
				{
					header.AutoResize(resize);
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
				SelectedFiles.Where(f => f.StartsWith(folderNode.Folder.FullName, StringComparison.CurrentCultureIgnoreCase))
					.ToList()
					.ForEach(f => SelectedFiles.Remove(f));
				DeselectedFiles.Where(f => f.StartsWith(folderNode.Folder.FullName, StringComparison.CurrentCultureIgnoreCase))
					.ToList()
					.ForEach(f => DeselectedFiles.Remove(f));
			}
			UpdateListView(NodeClicked);
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
			foreach (var selectedFile in SelectedFiles)
			{
				AddFileSelection(selectedFile, true);
			}
			foreach (var deselectedFile in DeselectedFiles)
			{
				AddFileSelection(deselectedFile, false);
			}

			Vault.Selections.Replace(Selections);
		}

		private void AddFileSelection(string file, bool selected)
		{
			Selections.Add(new Selection { Path = file, IsDirectory = false, Selected = selected });
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
					foreach (TreeNode childNode in folderNode.Nodes)
					{
						CollectSelections(childNode);
					}
				}
			}
			else if (node is TreeNodeExpanding expandingNode)
			{
				if (expandingNode.Parent is TreeNodeFolder parent)
				{
					foreach (var deselected in Deselected.Where(p => p.StartsWith(parent.Folder.FullName) && p.Length > parent.Folder.FullName.Length))
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
			Selections.Add(new Selection { IsDirectory = true, Path = node.Folder.FullName, Selected = node.State == SelectionState.Selected });
		}

		private void ListViewMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			ListViewHitTestInfo hit = listView.HitTest(e.Location);

			if (hit.Item==null || hit.Location!=ListViewHitTestLocations.StateImage) return;

			if (hit.Item is ListItemFile fileItem)
			{
				switch (fileItem.State)
				{
					case SelectionState.Unselected:
						fileItem.State = SelectionState.Selected;						
						SelectedFiles.Add(fileItem.File.FullName);						
						break;
					case SelectionState.Selected:
						SelectedFiles.Remove(fileItem.File.FullName);
						if (fileItem.Node.IsSelectedState)
						{
							fileItem.State = SelectionState.Deselected;
							DeselectedFiles.Add(fileItem.File.FullName);
						}
						else if (fileItem.Node.IsDeselectedState)
						{
							fileItem.State = SelectionState.DeselectedParent;
						}
						else
						{
							fileItem.State = SelectionState.Unselected;
						}												
						break;
					case SelectionState.Deselected:
						fileItem.State = fileItem.Node.State == SelectionState.Unselected ? SelectionState.Unselected : SelectionState.SelectedParent;
						DeselectedFiles.Remove(fileItem.File.FullName);
						break;
					case SelectionState.SelectedParent:
						fileItem.State = SelectionState.Deselected;
						DeselectedFiles.Add(fileItem.File.FullName);
						break;
				}
				fileItem.Node.UpdatedFile();
			}
		}
	}
}
