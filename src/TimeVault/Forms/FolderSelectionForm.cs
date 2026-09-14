using TimeVault.Vaults;
using System.ComponentModel;

namespace TimeVault.Forms
{
	internal partial class FolderSelectionForm : Form
	{
		public FolderSelectionForm()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required Vault Vault { get; init; }

		private void FolderSelectionLoad(object sender, EventArgs e)
		{
			var ignore = new DriveType[] { DriveType.Removable, DriveType.Unknown, DriveType.CDRom, DriveType.NoRootDirectory }.ToHashSet();

			foreach (var drive in DriveInfo.GetDrives().Where(d => !ignore.Contains(d.DriveType)).OrderBy(d => d.Name))
			{
				var node = new TreeNodeFolder(new DirectoryInfo(drive.Name))
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
				e.Cancel = node.Expanding();
			}
		}
	}
}
