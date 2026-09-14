namespace TimeVault.Forms
{
	partial class ExclusionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			grid = new DataGridView();
			layoutPanel = new TableLayoutPanel();
			buttonOk = new Button();
			buttonCancel = new Button();
			ColumnDirectory = new DataGridViewCheckBoxColumn();
			ColumnPattern = new DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)grid).BeginInit();
			layoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// grid
			// 
			grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			grid.Columns.AddRange(new DataGridViewColumn[] { ColumnDirectory, ColumnPattern });
			layoutPanel.SetColumnSpan(grid, 3);
			grid.Dock = DockStyle.Fill;
			grid.Location = new Point(3, 3);
			grid.Name = "grid";
			grid.RowHeadersWidth = 51;
			grid.Size = new Size(1060, 484);
			grid.TabIndex = 0;
			grid.CellFormatting += GridCellFormatting;
			grid.CurrentCellChanged += GridCurrentCellChanged;
			grid.RowValidated += GridRowValidated;
			// 
			// layoutPanel
			// 
			layoutPanel.ColumnCount = 3;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
			layoutPanel.Controls.Add(grid, 0, 0);
			layoutPanel.Controls.Add(buttonOk, 1, 1);
			layoutPanel.Controls.Add(buttonCancel, 2, 1);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 0);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
			layoutPanel.Size = new Size(1066, 532);
			layoutPanel.TabIndex = 1;
			// 
			// buttonOk
			// 
			buttonOk.DialogResult = DialogResult.OK;
			buttonOk.Dock = DockStyle.Fill;
			buttonOk.Enabled = false;
			buttonOk.Location = new Point(749, 493);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new Size(154, 36);
			buttonOk.TabIndex = 1;
			buttonOk.Text = "&Ok";
			buttonOk.UseVisualStyleBackColor = true;
			buttonOk.Click += ButtonOkClick;
			// 
			// buttonCancel
			// 
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.Dock = DockStyle.Fill;
			buttonCancel.Location = new Point(909, 493);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(154, 36);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			// 
			// ColumnDirectory
			// 
			ColumnDirectory.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			ColumnDirectory.DataPropertyName = "IsDirectory";
			ColumnDirectory.FillWeight = 53.4759369F;
			ColumnDirectory.HeaderText = "Directory";
			ColumnDirectory.MinimumWidth = 6;
			ColumnDirectory.Name = "ColumnDirectory";
			ColumnDirectory.SortMode = DataGridViewColumnSortMode.Automatic;
			ColumnDirectory.Width = 90;
			// 
			// ColumnPattern
			// 
			ColumnPattern.DataPropertyName = "Pattern";
			ColumnPattern.FillWeight = 146.524063F;
			ColumnPattern.HeaderText = "Pattern";
			ColumnPattern.MinimumWidth = 6;
			ColumnPattern.Name = "ColumnPattern";
			// 
			// ExclusionForm
			// 
			AcceptButton = buttonOk;
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(1066, 532);
			Controls.Add(layoutPanel);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4);
			Name = "ExclusionForm";
			Text = "Exclude";
			Load += ExcludeFormLoad;
			((System.ComponentModel.ISupportInitialize)grid).EndInit();
			layoutPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private DataGridView grid;
		private TableLayoutPanel layoutPanel;
		private Button buttonOk;
		private Button buttonCancel;
		private DataGridViewCheckBoxColumn ColumnDirectory;
		private DataGridViewTextBoxColumn ColumnPattern;
	}
}