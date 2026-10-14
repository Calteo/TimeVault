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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VaultControl));
			layoutPanel = new TableLayoutPanel();
			buttonFolders = new Button();
			buttonExclude = new Button();
			buttonStartStop = new Button();
			imageList = new ImageList(components);
			labelItemCount = new Label();
			labelFolder = new Label();
			timerProgress = new System.Windows.Forms.Timer(components);
			layoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// layoutPanel
			// 
			layoutPanel.BackColor = Color.Transparent;
			layoutPanel.ColumnCount = 3;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
			layoutPanel.Controls.Add(buttonFolders, 1, 0);
			layoutPanel.Controls.Add(buttonExclude, 2, 0);
			layoutPanel.Controls.Add(buttonStartStop, 0, 0);
			layoutPanel.Controls.Add(labelItemCount, 0, 1);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 40);
			layoutPanel.Margin = new Padding(4);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			layoutPanel.Size = new Size(1057, 193);
			layoutPanel.TabIndex = 0;
			// 
			// buttonFolders
			// 
			buttonFolders.Dock = DockStyle.Fill;
			buttonFolders.Location = new Point(741, 4);
			buttonFolders.Margin = new Padding(4);
			buttonFolders.Name = "buttonFolders";
			buttonFolders.Size = new Size(152, 41);
			buttonFolders.TabIndex = 0;
			buttonFolders.Text = "Folders";
			buttonFolders.UseVisualStyleBackColor = true;
			buttonFolders.Click += ButtonFoldersClick;
			// 
			// buttonExclude
			// 
			buttonExclude.Dock = DockStyle.Fill;
			buttonExclude.Location = new Point(901, 4);
			buttonExclude.Margin = new Padding(4);
			buttonExclude.Name = "buttonExclude";
			buttonExclude.Size = new Size(152, 41);
			buttonExclude.TabIndex = 1;
			buttonExclude.Text = "Exclude";
			buttonExclude.UseVisualStyleBackColor = true;
			buttonExclude.Click += ButtonExcludeClick;
			// 
			// buttonStartStop
			// 
			buttonStartStop.ImageKey = "play";
			buttonStartStop.ImageList = imageList;
			buttonStartStop.Location = new Point(3, 3);
			buttonStartStop.Name = "buttonStartStop";
			buttonStartStop.Size = new Size(112, 43);
			buttonStartStop.TabIndex = 2;
			buttonStartStop.Text = "Start";
			buttonStartStop.TextImageRelation = TextImageRelation.ImageBeforeText;
			buttonStartStop.UseVisualStyleBackColor = true;
			buttonStartStop.Click += ButtonStartStopClick;
			// 
			// imageList
			// 
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = Color.Transparent;
			imageList.Images.SetKeyName(0, "pause");
			imageList.Images.SetKeyName(1, "play");
			imageList.Images.SetKeyName(2, "progress-100");
			imageList.Images.SetKeyName(3, "progress-0");
			imageList.Images.SetKeyName(4, "progress-33");
			imageList.Images.SetKeyName(5, "progress-66");
			// 
			// labelItemCount
			// 
			labelItemCount.AutoSize = true;
			labelItemCount.Location = new Point(3, 49);
			labelItemCount.Name = "labelItemCount";
			labelItemCount.Size = new Size(59, 25);
			labelItemCount.TabIndex = 3;
			labelItemCount.Text = "label1";
			// 
			// labelFolder
			// 
			labelFolder.AutoEllipsis = true;
			labelFolder.BackColor = SystemColors.InactiveCaption;
			labelFolder.Dock = DockStyle.Top;
			labelFolder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelFolder.ImageAlign = ContentAlignment.MiddleRight;
			labelFolder.ImageList = imageList;
			labelFolder.Location = new Point(0, 0);
			labelFolder.Margin = new Padding(4, 0, 4, 0);
			labelFolder.Name = "labelFolder";
			labelFolder.Size = new Size(1057, 40);
			labelFolder.TabIndex = 0;
			labelFolder.Text = "Folder";
			labelFolder.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// timerProgress
			// 
			timerProgress.Interval = 1000;
			timerProgress.Tag = "";
			timerProgress.Tick += TimerProgressTick;
			// 
			// VaultControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ControlLight;
			Controls.Add(layoutPanel);
			Controls.Add(labelFolder);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(12);
			Name = "VaultControl";
			Size = new Size(1057, 233);
			Load += VaultControlLoad;
			layoutPanel.ResumeLayout(false);
			layoutPanel.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel layoutPanel;
		private Label labelFolder;
		private Button buttonFolders;
		private Button buttonExclude;
		private Button buttonStartStop;
		private ImageList imageList;
		private Label labelItemCount;
		private System.Windows.Forms.Timer timerProgress;
	}
}
