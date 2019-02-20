﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Sinema
{
    public static class WindowExtensions
    {
        private const int _AboutSysMenuID = 1001;

        private const int GWL_STYLE = -16, WS_MAXIMIZEBOX = 0x10000, WS_MINIMIZEBOX = 0x20000;

        private const uint MF_BYCOMMAND = 0x00000000;

        private const int MF_BYPOSITION = 0x400;

        private const uint MF_ENABLED = 0x00000000;

        private const uint MF_GRAYED = 0x00000001;

        private const uint SC_CLOSE = 0xF060;

        private const int WM_SHOWWINDOW = 0x00000018;

        private const int WM_SYSCOMMAND = 0x112;

        private static readonly MainWindow _form = Application.Current.Windows[0] as MainWindow;

        public static IntPtr GlassWndProc(this IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == ExtendGlass.WM_DWMCOMPOSITIONCHANGED)
            {
                ExtendGlass.ExtendDwmGlass(_form, new Thickness(-1));
                handled = true;
            }

            return IntPtr.Zero;
        }

        public static void SystemMenu(MainWindow form)
        {
            var systemMenuHandle = GetSystemMenu(new WindowInteropHelper(form).Handle, false);
            InsertMenu(systemMenuHandle, 7, MF_BYPOSITION, _AboutSysMenuID, "Hakkında...");

            var source = HwndSource.FromHwnd(new WindowInteropHelper(form).Handle);
            source.AddHook(WndProc);
        }

        internal static void DisableCloseButton(this Window window, bool disable)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            var sysMenu = GetSystemMenu(hwnd, false);
            if (disable)
                EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
            else
                EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND | MF_ENABLED);
        }

        internal static void HideMinimizeButtons(this Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            var currentStyle = GetWindowLong(hwnd, GWL_STYLE);

            SetWindowLong(hwnd, GWL_STYLE, currentStyle & ~WS_MINIMIZEBOX);
        }

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool InsertMenu(IntPtr hMenu, uint wPosition, uint wFlags, int wIDNewItem,
            string lpNewItem);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int value);

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SYSCOMMAND)
                switch (wParam.ToInt32())
                {
                    case _AboutSysMenuID:
                        var Hakkında = new Hakkında
                        {
                            Owner = _form
                        };
                        Hakkında.ShowDialog();

                        handled = true;
                        break;
                }

            return IntPtr.Zero;
        }
    }
}