using TimeVault.Vaults;
using Toolbox.ComponentModel;
using Toolbox.Settings;

namespace TimeVault.Forms
{
	internal class TimeVaultContext : ApplicationContext
	{
		public Options Options { get; }
		public TimeVaultSetting Setting { get; }
		public BindableList<Vault> Vaults { get; } = [];

		public TimeVaultContext(Options options)
		{
			Options = options;

#if DEBUG   // Relocate settings folder to the base directory for debugging purposes.
			Settings.Folders[Store.Local] = AppDomain.CurrentDomain.BaseDirectory;
#endif
			Setting = Settings.Get<TimeVaultSetting>();

			foreach (var vaultFolder in Setting.Vaults)
			{
				Vaults.Add(new Vault(vaultFolder));
			}

			_menu = CreateMenu();
			_trayIcon = CreateTrayIcon();

			if (Options.Show)
			{
				TrayIconDoubleClick(this, EventArgs.Empty);
			}
		}

		protected override void ExitThreadCore()
		{
			foreach (var vault in Vaults)
			{
				vault.Pool.Cancel();
			}

			DistroyDisplay();

			base.ExitThreadCore();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_trayIcon.Dispose();
				_menu.Dispose();
			}

			base.Dispose(disposing);
		}

		#region AppForm
		private AppForm? _appForm;

		private void TrayIconDoubleClick(object? sender, EventArgs e)
		{
			if (_appForm == null || _appForm.IsDisposed)
			{
				_appForm = new AppForm
				{
					Setting = Setting,
					Vaults = Vaults,
				};
				_appForm.FormClosed += AppFormClosed;
			}

			_appForm.Show();
			_appForm.Activate();
		}

		private void AppFormClosed(object? sender, FormClosedEventArgs e)
		{
			_appForm = null;
		}
		#endregion

		#region Display
		private readonly NotifyIcon _trayIcon;
		private readonly ContextMenuStrip _menu;

		private ContextMenuStrip CreateMenu()
		{
			var menu = new ContextMenuStrip();

			menu.Items.Add(
				"Hello",
				null,
				(_, _) => MessageBox.Show("Hello!"));

			menu.Items.Add(new ToolStripSeparator());

			menu.Items.Add(
				"Exit",
				null,
				(_, _) => Application.Exit());

			return menu;
		}

		private NotifyIcon CreateTrayIcon()
		{ 
			var notify = new NotifyIcon
			{
				Icon = new Icon(GetType(), "Icons.archive-icon-icon-6.ico"),
				Text = "TimeVault",
				ContextMenuStrip = _menu,
				Visible = true
			};
			notify.DoubleClick += TrayIconDoubleClick;
			return notify;
		}

		private void DistroyDisplay()
		{
			_appForm?.Close();
			_trayIcon.Visible = false;
		}

		#endregion
	}
}