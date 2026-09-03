using System.ComponentModel;
using TimeVault.Vaults;
using Toolbox.ComponentModel;
using Toolbox.Forms;

namespace TimeVault.Forms
{
	internal partial class AppForm : Form
	{
		private VaultControl? selected;

		public AppForm()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required TimeVaultSetting Setting { get; init; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required BindableList<Vault> Vaults { get; init; }

		private void ButtonAddClick(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				var vault = new Vault(folderBrowserDialog.SelectedPath);
				Vaults.Add(vault);
				Setting.Vaults.Add(vault.Folder);
				Setting.Save();

				SetSelected(CreateVaultControl(vault));
			}
		}

		private VaultControl CreateVaultControl(Vault vault)
		{
			var control = new VaultControl()
			{
				Vault = vault,
				Width = layoutPanel.ClientSize.Width - Margin.Left - Margin.Right
			};
			HookEvents(control, control, OnVaultControlClick);
			layoutPanel.Controls.Add(control);
			return control;
		}

		private void HookEvents(VaultControl vault, Control control, EventHandler<EventArgs> onClick)
		{
			control.Click += (s, e) => onClick(vault, e);
			foreach (Control child in control.Controls)
			{
				HookEvents(vault, child, onClick);
			}
		}

		private VaultControl? GetSelected()
		{
			return selected;
		}

		private void SetSelected(VaultControl? value)
		{
			if (selected != null)
			{
				selected.Selected = false;
			}
			selected = value;
			if (selected != null)
			{
				selected.Selected = true;
			}
			buttonRemove.Enabled = selected != null;
		}

		private void OnVaultControlClick(object? sender, EventArgs e)
		{
			if (sender is VaultControl vaultControl)
			{
				SetSelected(vaultControl);
			}
		}

		private void AppFormLoad(object sender, EventArgs e)
		{
			foreach (var vault in Vaults)
				CreateVaultControl(vault);

			if (Vaults.Count > 0)
				SetSelected(layoutPanel.Controls[0] as VaultControl);
		}

		private void LayoutPanelResize(object sender, EventArgs e)
		{
			foreach (Control control in layoutPanel.Controls)
			{
				control.Width = layoutPanel.ClientSize.Width - control.Margin.Left - control.Margin.Right;
			}
		}

		private void ButtonRemoveClick(object sender, EventArgs e)
		{
			if (selected == null) return;

			var text = $"Are you sure you want to remove the selected vault '{selected.Vault.Folder}'?";
			if (DialogResult.Yes == MsgBox.Show(this, text, "Delete Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{				
				selected.Vault.Pool.Cancel();
				Vaults.Remove(selected.Vault);
				Setting.Vaults.Remove(selected.Vault.Folder);
				Setting.Save();
				layoutPanel.Controls.Remove(selected);
				selected.Dispose();

				SetSelected(null);
			}
		}
	}
}