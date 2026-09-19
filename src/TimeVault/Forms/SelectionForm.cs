using System.ComponentModel;
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

		public HashSet<string> Excluded { get; private set; } = [];
		public HashSet<string> Included { get; private set; } = [];

		public HashSet<string> HasSelection { get; private set; } = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		private void FolderSelectionLoad(object sender, EventArgs e)
		{
			var selections = Vault.Selections.Select().ToDictionary(s => s.Path);

			Included = new HashSet<string>(selections.Values.Where(s => s.Selected).Select(s => s.Path), StringComparer.InvariantCultureIgnoreCase);
			Excluded = new HashSet<string>(selections.Values.Where(s => !s.Selected).Select(s => s.Path), StringComparer.InvariantCultureIgnoreCase);

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
			if (e.Node is TreeNodeFolder node)
			{
			}
		}

		private void TreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeViewHitTestInfo hit = treeView.HitTest(e.Location);

			if (hit.Location == TreeViewHitTestLocations.StateImage)
			{
				switch (e.Node?.StateImageKey)
				{
					case TreeNodeFolder.KeyUnselected:
						e.Node.StateImageKey = TreeNodeFolder.KeyIncluded;
						break;
					case TreeNodeFolder.KeyIncluded:
						e.Node.StateImageKey = TreeNodeFolder.KeyUnselected;
						break;
				}
			}
		}
	}
}
