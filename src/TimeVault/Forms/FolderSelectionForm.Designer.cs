namespace TimeVault.Forms
{
	partial class FolderSelectionForm
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderSelectionForm));
			treeView = new TreeView();
			imageListTreeView = new ImageList(components);
			SuspendLayout();
			// 
			// treeView
			// 
			treeView.FullRowSelect = true;
			treeView.HideSelection = false;
			treeView.ImageIndex = 0;
			treeView.ImageList = imageListTreeView;
			treeView.Location = new Point(22, 13);
			treeView.Margin = new Padding(4);
			treeView.Name = "treeView";
			treeView.SelectedImageIndex = 0;
			treeView.Size = new Size(533, 478);
			treeView.TabIndex = 0;
			treeView.BeforeExpand += TreeViewBeforeExpand;
			// 
			// imageListTreeView
			// 
			imageListTreeView.ColorDepth = ColorDepth.Depth32Bit;
			imageListTreeView.ImageStream = (ImageListStreamer)resources.GetObject("imageListTreeView.ImageStream");
			imageListTreeView.TransparentColor = Color.Transparent;
			imageListTreeView.Images.SetKeyName(0, "folder");
			imageListTreeView.Images.SetKeyName(1, "drives");
			imageListTreeView.Images.SetKeyName(2, "drive");
			// 
			// FolderSelection
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1482, 774);
			Controls.Add(treeView);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4);
			Name = "FolderSelection";
			Text = "FolderSelection";
			Load += FolderSelectionLoad;
			ResumeLayout(false);
		}

		#endregion

		private TreeView treeView;
		private ImageList imageListTreeView;
	}
}