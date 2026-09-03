namespace TimeVault.Forms
{
	partial class VaultControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			layoutPanel = new TableLayoutPanel();
			labelFolder = new Label();
			SuspendLayout();
			// 
			// layoutPanel
			// 
			layoutPanel.BackColor = Color.Transparent;
			layoutPanel.ColumnCount = 2;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 32);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99.99999F));
			layoutPanel.Size = new Size(758, 261);
			layoutPanel.TabIndex = 0;
			// 
			// labelFolder
			// 
			labelFolder.AutoEllipsis = true;
			labelFolder.BackColor = SystemColors.InactiveCaption;
			labelFolder.Dock = DockStyle.Top;
			labelFolder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelFolder.Location = new Point(0, 0);
			labelFolder.Name = "labelFolder";
			labelFolder.Size = new Size(758, 32);
			labelFolder.TabIndex = 0;
			labelFolder.Text = "Folder";
			labelFolder.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// VaultControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ControlLight;
			Controls.Add(layoutPanel);
			Controls.Add(labelFolder);
			Margin = new Padding(10);
			Name = "VaultControl";
			Size = new Size(758, 293);
			Load += VaultControlLoad;
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel layoutPanel;
		private Label labelFolder;
	}
}
