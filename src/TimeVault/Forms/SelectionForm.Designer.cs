namespace TimeVault.Forms
{
	partial class SelectionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionForm));
			treeView = new TreeView();
			imageList = new ImageList(components);
			splitContainer = new SplitContainer();
			listView = new ListView();
			columnHeaderName = new ColumnHeader();
			layoutPanel = new TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			layoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// treeView
			// 
			treeView.Dock = DockStyle.Fill;
			treeView.FullRowSelect = true;
			treeView.HideSelection = false;
			treeView.ImageIndex = 0;
			treeView.ImageList = imageList;
			treeView.Location = new Point(0, 0);
			treeView.Margin = new Padding(4);
			treeView.Name = "treeView";
			treeView.SelectedImageIndex = 0;
			treeView.Size = new Size(323, 527);
			treeView.StateImageList = imageList;
			treeView.TabIndex = 0;
			treeView.BeforeExpand += TreeViewBeforeExpand;
			treeView.AfterSelect += TreeViewAfterSelect;
			treeView.NodeMouseClick += TreeViewNodeMouseClick;
			// 
			// imageList
			// 
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = Color.Transparent;
			imageList.Images.SetKeyName(0, "folder");
			imageList.Images.SetKeyName(1, "drives");
			imageList.Images.SetKeyName(2, "drive");
			imageList.Images.SetKeyName(3, "unselected");
			imageList.Images.SetKeyName(4, "included");
			imageList.Images.SetKeyName(5, "file-selected");
			imageList.Images.SetKeyName(6, "file");
			imageList.Images.SetKeyName(7, "included-parent");
			imageList.Images.SetKeyName(8, "has-selection");
			imageList.Images.SetKeyName(9, "excluded");
			// 
			// splitContainer
			// 
			layoutPanel.SetColumnSpan(splitContainer, 3);
			splitContainer.Dock = DockStyle.Fill;
			splitContainer.Location = new Point(3, 3);
			splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			splitContainer.Panel1.Controls.Add(treeView);
			// 
			// splitContainer.Panel2
			// 
			splitContainer.Panel2.Controls.Add(listView);
			splitContainer.Size = new Size(970, 527);
			splitContainer.SplitterDistance = 323;
			splitContainer.TabIndex = 1;
			// 
			// listView
			// 
			listView.Columns.AddRange(new ColumnHeader[] { columnHeaderName });
			listView.Dock = DockStyle.Fill;
			listView.FullRowSelect = true;
			listView.Location = new Point(0, 0);
			listView.Name = "listView";
			listView.Size = new Size(643, 527);
			listView.Sorting = SortOrder.Ascending;
			listView.StateImageList = imageList;
			listView.TabIndex = 0;
			listView.UseCompatibleStateImageBehavior = false;
			listView.View = View.Details;
			// 
			// columnHeaderName
			// 
			columnHeaderName.Text = "Name";
			columnHeaderName.Width = 150;
			// 
			// layoutPanel
			// 
			layoutPanel.ColumnCount = 3;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
			layoutPanel.Controls.Add(splitContainer, 0, 0);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 0);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			layoutPanel.Size = new Size(976, 569);
			layoutPanel.TabIndex = 2;
			// 
			// SelectionForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(976, 569);
			Controls.Add(layoutPanel);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4);
			Name = "SelectionForm";
			Text = "FolderSelection";
			Load += FolderSelectionLoad;
			splitContainer.Panel1.ResumeLayout(false);
			splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
			splitContainer.ResumeLayout(false);
			layoutPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TreeView treeView;
		private ImageList imageList;
		private SplitContainer splitContainer;
		private TableLayoutPanel layoutPanel;
		private ListView listView;
		private ColumnHeader columnHeaderName;
	}
}