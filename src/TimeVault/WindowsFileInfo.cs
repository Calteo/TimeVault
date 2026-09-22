using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TimeVault
{
	internal class WindowsFileInfo
	{
		private WindowsFileInfo(FileInfo file)
		{
			uint flags = SHGFI_ICON |
							SHGFI_TYPENAME |
							SHGFI_USEFILEATTRIBUTES |
							SHGFI_SMALLICON;

			SHGetFileInfo(
				file.Extension,
				0,
				out SHFILEINFO info,
				(uint)Marshal.SizeOf<SHFILEINFO>(),
				flags);

			if (info.hIcon == IntPtr.Zero)
			{
				TypeName = "File";
				Icon = SystemIcons.GetStockIcon(StockIconId.DocumentNoAssociation, StockIconOptions.SmallIcon);
				return;
			}

			TypeName = info.szTypeName;
			try
			{
				Icon = (Icon)Icon.FromHandle(info.hIcon).Clone();
			}
			catch
			{
				Icon = SystemIcons.GetStockIcon(StockIconId.DocumentNoAssociation, StockIconOptions.SmallIcon);
			}
			finally
			{
				DestroyIcon(info.hIcon);
			}

		}

		public Icon Icon { get; }
		public string TypeName { get; }


		private static Dictionary<string, WindowsFileInfo> Types { get; } 
			= new Dictionary<string, WindowsFileInfo>(StringComparer.InvariantCultureIgnoreCase);

		public static WindowsFileInfo Get(FileInfo file)
		{
			if (Types.TryGetValue(file.Extension, out var typeInfo)) return typeInfo;

			Types[file.Extension] = typeInfo = new WindowsFileInfo(file);
			return typeInfo;
		}

		public static string LengthString(FileInfo file)
		{
			var buffer = new char[64];

			StrFormatByteSizeW(file.Length, buffer, buffer.Length);

			return new string(buffer).TrimEnd('\0');
		}


		#region P/Invoke
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct SHFILEINFO
		{
			public IntPtr hIcon;
			public int iIcon;
			public uint dwAttributes;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		}

		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr SHGetFileInfo(
			string pszPath,
			uint dwFileAttributes,
			out SHFILEINFO psfi,
			uint cbFileInfo,
			uint uFlags);

		private const uint SHGFI_ICON = 0x100;
		private const uint SHGFI_LARGEICON = 0x0;
		private const uint SHGFI_SMALLICON = 0x1;
		private const uint SHGFI_TYPENAME = 0x400;
		private const uint SHGFI_USEFILEATTRIBUTES = 0x10;

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool DestroyIcon(IntPtr hIcon);

		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		private static extern int StrFormatByteSizeW(long qdw, char[] pszBuf, int cchBuf);
		#endregion
	}
}
