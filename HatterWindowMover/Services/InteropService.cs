using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WindowMover.Models;

namespace WindowMover.Services
{
    public interface IInteropService
    {
        bool RegisterHotKey(IntPtr hWnd, int id, int keyModifiers, int? key);
        bool DeregisterHotKey(IntPtr hWnd, int id);
        bool MoveWindow(WindowInfo process, int x, int y);
        IEnumerable<WindowInfo> GetWindows();

        int WindowsHotkeyMsgId { get; }
    }

    public class InteropService : IInteropService
    {
        private delegate bool EnumWindowsCallback(IntPtr hWnd, ArrayList wList);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsCallback callback, ArrayList lParam);
        
        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd); 
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        
        public int WindowsHotkeyMsgId { get { return 0x0312; } }
        
        bool IInteropService.RegisterHotKey(IntPtr hWnd, int id, int keyModifiers, int? key)
        {
            return key.HasValue && RegisterHotKey(hWnd, id, keyModifiers, key.Value);
        }

        bool IInteropService.DeregisterHotKey(IntPtr hWnd, int id)
        {
            return UnregisterHotKey(hWnd, id);
        }

        bool IInteropService.MoveWindow(WindowInfo process, int x, int y)
        {
            var result = SetWindowPos(process.Handle, new IntPtr(0), x, y, 0, 0, 0x0001 + 0x0040) || MoveWindow(process.Handle, x, y, 100, 100, true);
            if (!result)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return result;
        }

        public IEnumerable<WindowInfo> GetWindows()
        {
            var windowHandles = new ArrayList();
            EnumWindows(GetWindowHandle, windowHandles);

            return windowHandles.Cast<WindowInfo>()
                                .Where(i => !string.IsNullOrWhiteSpace(i.Description))
                                .ToList();
        }

        private static bool GetWindowHandle(IntPtr windowHandle, ArrayList windowHandles)
        {
            int size = GetWindowTextLength(windowHandle);
            if (size++ > 0 && IsWindowVisible(windowHandle))
            {
                var sb = new StringBuilder(size);
                GetWindowText(windowHandle, sb, size);
                windowHandles.Add(new WindowInfo{Description = sb.ToString(), Handle = windowHandle});
            }
            
            return true;
        }
    }
}
