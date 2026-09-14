using System.ComponentModel;
using System.Diagnostics;
using TimeVault.Access.Models;
using TimeVault.Access.Tables;
using TimeVault.Vaults;
using Toolbox.ComponentModel;

namespace TimeVault.Forms
{
	internal partial class ExclusionForm : Form
	{
		public ExclusionForm()
		{
			InitializeComponent();

			grid.AutoGenerateColumns = false;
		}

		private void ExclusionItemChanged(object? sender, ItemChangedEventArgs<Exclusion> e)
		{
			if (e.Item.Id != 0) Changed.Add(e.Item);
			EnableOk();
		}

		private void ExclusionsItemAdded(object? sender, ItemEventArgs<Exclusion> e)
		{
			Added.Add(e.Item);
			EnableOk();
		}

		private void ExclusionsItemRemoved(object? sender, ItemEventArgs<Exclusion> e)
		{
			Added.Remove(e.Item);

			if (e.Item.Id != 0) Deleted.Add(e.Item);
			EnableOk();
		}

		private void EnableOk()
		{
			buttonOk.Enabled = Exclusions.Where(e => e!=NewRow).All(e => e.Error == "" && e[nameof(Exclusion.Pattern)] == "")
				&& (Added.Count > 0 || Changed.Count > 0 || Deleted.Count > 0);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required Vault Vault { get; init; }

		private BindableList<Exclusion> Exclusions { get; set; } = [];
		private HashSet<Exclusion> Deleted { get; } = [];
		private HashSet<Exclusion> Changed { get; } = [];
		private HashSet<Exclusion> Added { get; } = [];

		private void ExcludeFormLoad(object sender, EventArgs e)
		{
			Exclusions = [.. Vault.Exclusions.Select()];  // get copy from database
			Exclusions.ItemRemoved += ExclusionsItemRemoved;
			Exclusions.ItemAdded += ExclusionsItemAdded;
			Exclusions.ItemChanged += ExclusionItemChanged;

			grid.DataSource = Exclusions;

			EnableOk();
		}

		private void ButtonOkClick(object sender, EventArgs e)
		{
			Vault.Exclusions.Modify(Added, Changed, Deleted);
		}

		private void GridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var row = grid.Rows[e.RowIndex];
			if (row.DataBoundItem is Exclusion exclusion)
			{
				var cell = row.Cells[e.ColumnIndex];
				if (!string.IsNullOrEmpty(cell.ErrorText))
				{
					e.CellStyle.BackColor = Color.LightCoral;
					cell.ToolTipText = cell.ErrorText;
				}
				else
				{
					cell.ToolTipText = "";
				}
			}
		}

		private void GridRowValidated(object sender, DataGridViewCellEventArgs e)
		{
			EnableOk();
		}

		private Exclusion? NewRow { get; set; }

		private void GridCurrentCellChanged(object sender, EventArgs e)
		{
			if (grid.CurrentRow != null && grid.CurrentRow.IsNewRow)
			{
				NewRow = Exclusions[grid.CurrentRow.Index];
			}
			else
			{
				NewRow = null;
			}
			EnableOk();
		}
	}
}
