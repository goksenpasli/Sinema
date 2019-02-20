using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sinema
{
    public static class IconExtractor
    {
        public static Icon Extract(string file, int number, bool largeIcon)
        {
            ExtractIconEx(file, number, out var large, out var small, 1);
            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }
        }

        public static ImageSource GetIcon(string regkey, string regvalue, string exe, int iconindex)
        {
            try
            {
                var Yol = Registry.GetValue(regkey, regvalue, null);
                if (Yol != null)
                {
                    var IconImage = Extract((string)Yol + exe, iconindex, true);
                    return Imaging.CreateBitmapSourceFromHIcon(IconImage.Handle, Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static ImageSource GetIcon(string regkey, string regvalue, int iconindex)
        {
            try
            {
                var Yol = Registry.GetValue(regkey, regvalue, null);
                if (Yol != null)
                {
                    var IconImage = Extract((string)Yol, iconindex, true);
                    return Imaging.CreateBitmapSourceFromHIcon(IconImage.Handle, Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion,
            out IntPtr piSmallVersion, int amountIcons);
    }
}