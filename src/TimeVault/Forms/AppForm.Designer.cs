namespace TimeVault.Forms
{
	partial class AppForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
			layoutPanel = new FlowLayoutPanel();
			folderBrowserDialog = new FolderBrowserDialog();
			toolStrip = new ToolStrip();
			buttonAdd = new ToolStripButton();
			buttonRemove = new ToolStripButton();
			toolStripContainer = new ToolStripContainer();
			toolStrip.SuspendLayout();
			toolStripContainer.ContentPanel.SuspendLayout();
			toolStripContainer.TopToolStripPanel.SuspendLayout();
			toolStripContainer.SuspendLayout();
			SuspendLayout();
			// 
			// layoutPanel
			// 
			layoutPanel.AutoScroll = true;
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 0);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.Size = new Size(1182, 622);
			layoutPanel.TabIndex = 0;
			layoutPanel.Resize += LayoutPanelResize;
			// 
			// folderBrowserDialog
			// 
			folderBrowserDialog.Description = "Select folder to store the vault";
			// 
			// toolStrip
			// 
			toolStrip.Dock = DockStyle.None;
			toolStrip.GripStyle = ToolStripGripStyle.Hidden;
			toolStrip.ImageScalingSize = new Size(20, 20);
			toolStrip.Items.AddRange(new ToolStripItem[] { buttonAdd, buttonRemove });
			toolStrip.Location = new Point(0, 0);
			toolStrip.Name = "toolStrip";
			toolStrip.Size = new Size(1182, 27);
			toolStrip.Stretch = true;
			toolStrip.TabIndex = 1;
			toolStrip.Text = "toolStrip1";
			// 
			// buttonAdd
			// 
			buttonAdd.Image = (Image)resources.GetObject("buttonAdd.Image");
			buttonAdd.ImageTransparentColor = Color.Magenta;
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new Size(61, 24);
			buttonAdd.Text = "Add";
			buttonAdd.Click += ButtonAddClick;
			// 
			// buttonRemove
			// 
			buttonRemove.Enabled = false;
			buttonRemove.Image = (Image)resources.GetObject("buttonRemove.Image");
			buttonRemove.ImageTransparentColor = Color.Magenta;
			buttonRemove.Name = "buttonRemove";
			buttonRemove.Size = new Size(87, 24);
			buttonRemove.Text = "Remove";
			buttonRemove.Click += ButtonRemoveClick;
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			toolStripContainer.ContentPanel.Controls.Add(layoutPanel);
			toolStripContainer.ContentPanel.Size = new Size(1182, 622);
			toolStripContainer.Dock = DockStyle.Fill;
			toolStripContainer.Location = new Point(0, 0);
			toolStripContainer.Name = "toolStripContainer";
			toolStripContainer.Size = new Size(1182, 649);
			toolStripContainer.TabIndex = 3;
			toolStripContainer.Text = "toolStripContainer2";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
			// 
			// AppForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1182, 649);
			Controls.Add(toolStripContainer);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4);
			Name = "AppForm";
			Text = "Time Vault";
			Load += AppFormLoad;
			toolStrip.ResumeLayout(false);
			toolStrip.PerformLayout();
			toolStripContainer.ContentPanel.ResumeLayout(false);
			toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			toolStripContainer.TopToolStripPanel.PerformLayout();
			toolStripContainer.ResumeLayout(false);
			toolStripContainer.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private FlowLayoutPanel layoutPanel;
		private FolderBrowserDialog folderBrowserDialog;
		private ToolStrip toolStrip;
		private ToolStripButton buttonAdd;		
		private ToolStripContainer toolStripContainer;
		private ToolStripButton buttonRemove;
	}
}
