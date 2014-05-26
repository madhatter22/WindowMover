using System;
using System.Linq;
using System.Windows.Forms;
using WindowMover.Models;
using WindowMover.ViewModels;

namespace WindowMover.Views
{
    public partial class MainView : Form
    {
        private readonly NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenu;
        private readonly MainViewModel _viewModel;

        public MainView(MainViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            InitializeComponent();

            _viewModel = viewModel;
            
            var configMenuItem = new MenuItem("Configuration", ShowConfig);
            var exitMenuItem = new MenuItem("Exit", Exit);

            _notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources.multiple_monitors,
                ContextMenu = new ContextMenu(new[] {configMenuItem, exitMenuItem }),
                Visible = true
            };

            _viewModel.RegisterHotKey(Handle);
        }

        private void Exit(object sender, EventArgs e)
        {
            _viewModel.DeregisterHotKey(Handle);
            _notifyIcon.Visible = false;
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == _viewModel.WindowsHotkeyMsgId)
            {
                DisposeConextMenu();
                _contextMenu = new ContextMenuStrip();
                foreach (var item in _viewModel.Windows.Select(wi => new ToolStripMenuItem(wi.Description) { Tag = wi }))
                {
                    _contextMenu.Items.Add(item);
                }

                _contextMenu.ItemClicked += ContextMenuClick;
                _contextMenu.Items.Add(new ToolStripSeparator());
                _contextMenu.Items.Add("Cancel");
                _contextMenu.Closed += ContextMenuOnClosed;
                _contextMenu.Show(Cursor.Position);
            }

            base.WndProc(ref m);
        }

        private void ContextMenuClick(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null) return;
            var cursorPosition = Cursor.Position;
            _viewModel.MoveWindow(e.ClickedItem.Tag as WindowInfo, cursorPosition.X, cursorPosition.Y);
        }

        private void ContextMenuOnClosed(object sender, ToolStripDropDownClosedEventArgs toolStripDropDownClosingEventArgs)
        {
            DisposeConextMenu();
        }

        private void DisposeConextMenu()
        {
            if (_contextMenu != null)
            {
                foreach (ToolStripItem toolStrip in _contextMenu.Items)
                {
                    toolStrip.Tag = null;
                }
                _contextMenu.Closed -= ContextMenuOnClosed;
                _contextMenu.ItemClicked -= ContextMenuClick;
                _contextMenu = null;
            }
        }

        private void ShowConfig(object sender, EventArgs e)
        {
            using (var config = new ConfigView(_viewModel.GetConfigViewModel()))
            {
                config.ShowDialog();
                _viewModel.DeregisterHotKey(Handle);
                _viewModel.RegisterHotKey(Handle);
            }
        }
    }
}
