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
			buttonFolders = new Button();
			buttonExclude = new Button();
			labelFolder = new Label();
			layoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// layoutPanel
			// 
			layoutPanel.BackColor = Color.Transparent;
			layoutPanel.ColumnCount = 2;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			layoutPanel.Controls.Add(buttonFolders, 1, 0);
			layoutPanel.Controls.Add(buttonExclude, 0, 0);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 40);
			layoutPanel.Margin = new Padding(4, 4, 4, 4);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99.99999F));
			layoutPanel.Size = new Size(990, 215);
			layoutPanel.TabIndex = 0;
			// 
			// buttonFolders
			// 
			buttonFolders.Location = new Point(499, 4);
			buttonFolders.Margin = new Padding(4, 4, 4, 4);
			buttonFolders.Name = "buttonFolders";
			buttonFolders.Size = new Size(118, 32);
			buttonFolders.TabIndex = 0;
			buttonFolders.Text = "Folders";
			buttonFolders.UseVisualStyleBackColor = true;
			buttonFolders.Click += ButtonFoldersClick;
			// 
			// buttonExclude
			// 
			buttonExclude.Location = new Point(4, 4);
			buttonExclude.Margin = new Padding(4, 4, 4, 4);
			buttonExclude.Name = "buttonExclude";
			buttonExclude.Size = new Size(118, 32);
			buttonExclude.TabIndex = 1;
			buttonExclude.Text = "Exclude";
			buttonExclude.UseVisualStyleBackColor = true;
			buttonExclude.Click += ButtonExcludeClick;
			// 
			// labelFolder
			// 
			labelFolder.AutoEllipsis = true;
			labelFolder.BackColor = SystemColors.InactiveCaption;
			labelFolder.Dock = DockStyle.Top;
			labelFolder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelFolder.Location = new Point(0, 0);
			labelFolder.Margin = new Padding(4, 0, 4, 0);
			labelFolder.Name = "labelFolder";
			labelFolder.Size = new Size(990, 40);
			labelFolder.TabIndex = 0;
			labelFolder.Text = "Folder";
			labelFolder.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// VaultControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ControlLight;
			Controls.Add(layoutPanel);
			Controls.Add(labelFolder);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(12, 12, 12, 12);
			Name = "VaultControl";
			Size = new Size(990, 255);
			Load += VaultControlLoad;
			layoutPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel layoutPanel;
		private Label labelFolder;
		private Button buttonFolders;
		private Button buttonExclude;
	}
}
