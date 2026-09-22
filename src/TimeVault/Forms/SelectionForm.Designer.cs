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
			contextMenuTree = new ContextMenuStrip(components);
			menuItemSelectFolder = new ToolStripMenuItem();
			menuItemDeselectFolder = new ToolStripMenuItem();
			imageList = new ImageList(components);
			splitContainer = new SplitContainer();
			listView = new ListView();
			columnHeaderName = new ColumnHeader();
			columnHeaderLastChanged = new ColumnHeader();
			columnHeaderType = new ColumnHeader();
			columnHeaderSize = new ColumnHeader();
			imageListFileTypes = new ImageList(components);
			layoutPanel = new TableLayoutPanel();
			buttonOk = new Button();
			buttonCancel = new Button();
			contextMenuTree.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			layoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// treeView
			// 
			treeView.ContextMenuStrip = contextMenuTree;
			treeView.Dock = DockStyle.Fill;
			treeView.FullRowSelect = true;
			treeView.HideSelection = false;
			treeView.ImageIndex = 0;
			treeView.ImageList = imageList;
			treeView.Location = new Point(0, 0);
			treeView.Margin = new Padding(4);
			treeView.Name = "treeView";
			treeView.SelectedImageIndex = 0;
			treeView.Size = new Size(323, 521);
			treeView.StateImageList = imageList;
			treeView.TabIndex = 0;
			treeView.BeforeExpand += TreeViewBeforeExpand;
			treeView.AfterSelect += TreeViewAfterSelect;
			treeView.NodeMouseClick += TreeViewNodeMouseClick;
			// 
			// contextMenuTree
			// 
			contextMenuTree.ImageScalingSize = new Size(20, 20);
			contextMenuTree.Items.AddRange(new ToolStripItem[] { menuItemSelectFolder, menuItemDeselectFolder });
			contextMenuTree.Name = "contextMenuTree";
			contextMenuTree.Size = new Size(140, 56);
			contextMenuTree.Opening += ContextMenuTreeOpening;
			// 
			// menuItemSelectFolder
			// 
			menuItemSelectFolder.Image = Properties.Resources.checked_checkbox;
			menuItemSelectFolder.Name = "menuItemSelectFolder";
			menuItemSelectFolder.Size = new Size(139, 26);
			menuItemSelectFolder.Text = "Select";
			menuItemSelectFolder.Click += MenuItemSelectFolderClick;
			// 
			// menuItemDeselectFolder
			// 
			menuItemDeselectFolder.Image = Properties.Resources.checked_inverse_x;
			menuItemDeselectFolder.Name = "menuItemDeselectFolder";
			menuItemDeselectFolder.Size = new Size(139, 26);
			menuItemDeselectFolder.Text = "Deselect";
			menuItemDeselectFolder.Click += MenuItemDeselectFolderClick;
			// 
			// imageList
			// 
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = Color.Transparent;
			imageList.Images.SetKeyName(0, "folder");
			imageList.Images.SetKeyName(1, "drives");
			imageList.Images.SetKeyName(2, "drive");
			imageList.Images.SetKeyName(3, "Unselected");
			imageList.Images.SetKeyName(4, "Selected");
			imageList.Images.SetKeyName(5, "SelectedParent");
			imageList.Images.SetKeyName(6, "file-selected");
			imageList.Images.SetKeyName(7, "file");
			imageList.Images.SetKeyName(8, "ContainsSelection");
			imageList.Images.SetKeyName(9, "Excluded");
			imageList.Images.SetKeyName(10, "DeselectedParent");
			imageList.Images.SetKeyName(11, "Deselected");
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
			splitContainer.Size = new Size(970, 521);
			splitContainer.SplitterDistance = 323;
			splitContainer.TabIndex = 1;
			// 
			// listView
			// 
			listView.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderLastChanged, columnHeaderType, columnHeaderSize });
			listView.Dock = DockStyle.Fill;
			listView.FullRowSelect = true;
			listView.LargeImageList = imageListFileTypes;
			listView.Location = new Point(0, 0);
			listView.Name = "listView";
			listView.Size = new Size(643, 521);
			listView.SmallImageList = imageListFileTypes;
			listView.Sorting = SortOrder.Ascending;
			listView.StateImageList = imageList;
			listView.TabIndex = 1;
			listView.UseCompatibleStateImageBehavior = false;
			listView.View = View.Details;
			// 
			// columnHeaderName
			// 
			columnHeaderName.Text = "Name";
			columnHeaderName.Width = 180;
			// 
			// columnHeaderLastChanged
			// 
			columnHeaderLastChanged.Text = "Changed";
			columnHeaderLastChanged.Width = 150;
			// 
			// columnHeaderType
			// 
			columnHeaderType.Text = "Type";
			columnHeaderType.Width = 120;
			// 
			// columnHeaderSize
			// 
			columnHeaderSize.Text = "Size";
			columnHeaderSize.TextAlign = HorizontalAlignment.Right;
			columnHeaderSize.Width = 120;
			// 
			// imageListFileTypes
			// 
			imageListFileTypes.ColorDepth = ColorDepth.Depth32Bit;
			imageListFileTypes.ImageSize = new Size(24, 24);
			imageListFileTypes.TransparentColor = Color.Transparent;
			// 
			// layoutPanel
			// 
			layoutPanel.ColumnCount = 3;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
			layoutPanel.Controls.Add(splitContainer, 0, 0);
			layoutPanel.Controls.Add(buttonOk, 1, 1);
			layoutPanel.Controls.Add(buttonCancel, 2, 1);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 0);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 2;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
			layoutPanel.Size = new Size(976, 569);
			layoutPanel.TabIndex = 2;
			// 
			// buttonOk
			// 
			buttonOk.DialogResult = DialogResult.OK;
			buttonOk.Dock = DockStyle.Fill;
			buttonOk.Location = new Point(679, 530);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new Size(144, 36);
			buttonOk.TabIndex = 2;
			buttonOk.Text = "&Ok";
			buttonOk.UseVisualStyleBackColor = true;
			buttonOk.Click += ButtonOkClick;
			// 
			// buttonCancel
			// 
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.Dock = DockStyle.Fill;
			buttonCancel.Location = new Point(829, 530);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(144, 36);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			// 
			// SelectionForm
			// 
			AcceptButton = buttonOk;
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(976, 569);
			Controls.Add(layoutPanel);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4);
			Name = "SelectionForm";
			Text = "Selection";
			Load += FolderSelectionLoad;
			contextMenuTree.ResumeLayout(false);
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
		private ContextMenuStrip contextMenuTree;
		private ToolStripMenuItem menuItemDeselectFolder;
		private ToolStripMenuItem menuItemSelectFolder;
		private Button buttonOk;
		private Button buttonCancel;
		private ColumnHeader columnHeaderLastChanged;
		private ColumnHeader columnHeaderType;
		private ColumnHeader columnHeaderSize;
		private ImageList imageListFileTypes;
	}
}