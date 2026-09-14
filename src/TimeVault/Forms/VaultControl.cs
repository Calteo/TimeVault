using System.ComponentModel;
using TimeVault.Vaults;

namespace TimeVault.Forms
{
	internal partial class VaultControl : UserControl
	{
		private bool selected;

		public VaultControl()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required Vault Vault { get; init; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Selected
		{
			get => selected;
			set
			{
				selected = value;
				labelFolder.BackColor = selected ? SystemColors.ActiveCaption : SystemColors.InactiveCaption;
				BackColor = selected ? SystemColors.ControlLightLight : SystemColors.ControlLight;
			}
		}

		private void VaultControlLoad(object sender, EventArgs e)
		{
			if (DesignMode) return;

			labelFolder.Text = Vault.Folder;
		}

		private void ButtonFoldersClick(object sender, EventArgs e)
		{
			var form = new FolderSelectionForm { Vault = Vault };
			form.ShowDialog(this);
		}

		private void ButtonExcludeClick(object sender, EventArgs e)
		{
			var form = new ExclusionForm { Vault = Vault };
			form.ShowDialog(this);
		}
	}
}
