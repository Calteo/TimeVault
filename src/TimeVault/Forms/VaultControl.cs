using System.ComponentModel;
using TimeVault.Vaults;
using Toolbox.Forms;

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

			this.Subscribe(Vault, nameof(Vault.RunningChanged), VaultRunningChanged);
			this.Subscribe(Vault, nameof(Vault.ActiveItemsChanged), VaultActiveItemsChanged);

			VaultRunningChanged(this, EventArgs.Empty);
			VaultActiveItemsChanged(this, EventArgs.Empty);
		}

		private void VaultActiveItemsChanged(object? sender, EventArgs e)
		{
			labelItemCount.Text = $"Active Items: {Vault.ActiveItems}";
		}

		private void VaultRunningChanged(object? sender, EventArgs e)
		{
			if (Vault.Running)
			{
				buttonStartStop.Text = "Stop";
				buttonStartStop.ImageKey = "pause"; ;
			}
			else
			{
				buttonStartStop.Enabled = true;
				buttonStartStop.Text = "Start";
				buttonStartStop.ImageKey = "play"; ;
			}
		}

		private void ButtonFoldersClick(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			var form = new SelectionForm { Vault = Vault };
			form.ShowDialog(this);
			ParentForm?.UseWaitCursor = false;
			Cursor = Cursors.Default;

		}

		private void ButtonExcludeClick(object sender, EventArgs e)
		{
			var form = new ExclusionForm { Vault = Vault };
			form.ShowDialog(this);
		}

		private void ButtonStartStopClick(object sender, EventArgs e)
		{
			if (Vault.Running)
			{
				buttonStartStop.Enabled = false;
				Vault.Stop();

				buttonStartStop.Text = "Start";
				buttonStartStop.ImageKey = "play"; ;
			}
			else
			{
				Vault.Start();

				buttonStartStop.Text = "Stop";
				buttonStartStop.ImageKey = "pause"; ;
			}
		}
	}
}
