using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using SystemColors = System.Windows.SystemColors;

namespace Sinema
{
    internal static class ExtendGlass
    {
        /// <summary>
        ///     Defines the WM_DWMCOMPOSITIONCHANGED
        /// </summary>
        public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

        /// <summary>
        ///     The ExtendDwmGlass
        /// </summary>
        /// <param name="window">The <see cref="Window" /></param>
        /// <param name="thikness">The <see cref="Thickness" /></param>
        public static void ExtendDwmGlass(Window window, Thickness thikness)
        {
            try
            {
                var isGlassEnabled = 0;
                DwmIsCompositionEnabled(ref isGlassEnabled);
                if (Environment.OSVersion.Version.Major > 5 && isGlassEnabled > 0)
                {
                    // Get the window handle
                    var helper = new WindowInteropHelper(window);
                    var mainWindowSrc = HwndSource.FromHwnd(helper.Handle);
                    mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

                    // Get the dpi of the screen
                    var desktop = Graphics.FromHwnd(mainWindowSrc.Handle);
                    var dpiX = desktop.DpiX / 96;
                    var dpiY = desktop.DpiY / 96;

                    // Set Margins
                    var margins = new MARGINS
                    {
                        cxLeftWidth = (int)(thikness.Left * dpiX),
                        cxRightWidth = (int)(thikness.Right * dpiX),
                        cyBottomHeight = (int)(thikness.Bottom * dpiY),
                        cyTopHeight = (int)(thikness.Top * dpiY)
                    };

                    window.Background = Brushes.Transparent;

                    var hr = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                }
                else
                {
                    window.Background = SystemColors.WindowBrush;
                }
            }
            catch (DllNotFoundException)
            {
            }
        }

        /// <summary>
        ///     The DwmExtendFrameIntoClientArea
        /// </summary>
        /// <param name="hWnd">The <see cref="IntPtr" /></param>
        /// <param name="pMarInset">The <see cref="MARGINS" /></param>
        /// <returns>The <see cref="int" /></returns>
        [DllImport("dwmapi.dll")]
        private static extern int
            DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        /// <summary>
        ///     The DwmIsCompositionEnabled
        /// </summary>
        /// <param name="en">The <see cref="int" /></param>
        /// <returns>The <see cref="int" /></returns>
        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(ref int en);

        /// <summary>
        ///     Defines the <see cref="MARGINS" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            /// <summary>
            ///     Defines the cxLeftWidth
            /// </summary>
            public int cxLeftWidth;

            /// <summary>
            ///     Defines the cxRightWidth
            /// </summary>
            public int cxRightWidth;

            /// <summary>
            ///     Defines the cyTopHeight
            /// </summary>
            public int cyTopHeight;

            /// <summary>
            ///     Defines the cyBottomHeight
            /// </summary>
            public int cyBottomHeight;
        }
    }
}