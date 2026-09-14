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
			layoutPanel.Location = new Point(0, 32);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99.99999F));
			layoutPanel.Size = new Size(792, 172);
			layoutPanel.TabIndex = 0;
			// 
			// buttonFolders
			// 
			buttonFolders.Location = new Point(399, 3);
			buttonFolders.Name = "buttonFolders";
			buttonFolders.Size = new Size(94, 26);
			buttonFolders.TabIndex = 0;
			buttonFolders.Text = "Folders";
			buttonFolders.UseVisualStyleBackColor = true;
			buttonFolders.Click += ButtonFoldersClick;
			// 
			// buttonExclude
			// 
			buttonExclude.Location = new Point(3, 3);
			buttonExclude.Name = "buttonExclude";
			buttonExclude.Size = new Size(94, 26);
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
			labelFolder.Name = "labelFolder";
			labelFolder.Size = new Size(792, 32);
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
			Size = new Size(792, 204);
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
