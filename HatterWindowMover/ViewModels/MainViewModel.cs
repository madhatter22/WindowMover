using System;
using System.Collections.Generic;
using System.Linq;
using WindowMover.Models;
using WindowMover.Services;

namespace WindowMover.ViewModels
{
    public class MainViewModel
    {
        private ISettingsService _settings;
        private IInteropService _interop;
        private int _id;

        public MainViewModel() : this(new SettingsService(), new InteropService())
        {
        }

        public MainViewModel(ISettingsService settings, IInteropService interop)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            if (interop == null) throw new ArgumentNullException("interop");

            _settings = settings;
            _interop = interop;
            _id = "Hatter's Move Window Helper".GetHashCode();
        }

        public IEnumerable<WindowInfo> Windows { get { return _interop.GetWindows(); } }
        public int WindowsHotkeyMsgId { get { return _interop.WindowsHotkeyMsgId; } }

        public ConfigViewModel GetConfigViewModel()
        {
            return new ConfigViewModel(_settings);
        }

        public void RegisterHotKey(IntPtr handle)
        {
            _interop.RegisterHotKey(handle, _id, _settings.GetSavedModifiers().Sum(s => (int)s), _settings.GetDefaultKey());
        }

        public void DeregisterHotKey(IntPtr handle)
        {
            _interop.DeregisterHotKey(handle, _id);
        }

        public void MoveWindow(WindowInfo windowInfo, int x, int y)
        {
            if (windowInfo != null)
                _interop.MoveWindow(windowInfo, x, y);
        }
    }
}
