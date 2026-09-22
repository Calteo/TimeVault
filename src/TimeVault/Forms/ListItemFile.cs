using Microsoft.Win32;

namespace TimeVault.Forms
{
	internal class ListItemFile : ListViewItem
	{
		public ListItemFile(ListView view, TreeNodeFolder node, FileInfo file)
			: base(file.Name)
		{
			Node = node;
			File = file;

			var info = WindowsFileInfo.Get(file);
					
			SubItems.AddRange(file.LastWriteTime.ToString(), info.TypeName, WindowsFileInfo.LengthString(file));
			if (!view.SmallImageList!.Images.ContainsKey(file.Extension))
			{
				view.SmallImageList.Images.Add(file.Extension, info.Icon);
			}

			ImageKey = file.Extension;

			switch (node.State)
			{
				case SelectionState.Selected:
				case SelectionState.SelectedParent:
					State = SelectionState.SelectedParent;
					break;
				case SelectionState.Deselected:
				case SelectionState.DeselectedParent:
					State = SelectionState.DeselectedParent;
					break;
				default:
					State = node.State;
					break;
			}
		}

		public TreeNodeFolder Node { get; }
		public FileInfo File { get; }

		#region State
		private SelectionState _state;
		public SelectionState State
		{
			get => _state;
			set
			{
				_state = value;
				StateImageIndex = Node.TreeView!.StateImageList!.Images.IndexOfKey(State.ToString());
			}
		}
		#endregion

		private string GetWindowsFileType()
		{
			var extension = File.Extension;

			if (string.IsNullOrEmpty(extension)) return "File";

			using var key = Registry.ClassesRoot.OpenSubKey(extension);

			if (key == null) return "File";

			var progId = key.GetValue(null) as string;

			if (string.IsNullOrEmpty(progId)) return "File";

			using var progIdKey = Registry.ClassesRoot.OpenSubKey(progId);

			return progIdKey?.GetValue(null) as string ?? "File";
		}
	}
}
