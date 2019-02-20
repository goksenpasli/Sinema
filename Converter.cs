using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.MediaPlayers;
using Brushes = System.Windows.Media.Brushes;

namespace Sinema
{
    public class AdminToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null && ((Kullanicilar)value).KullaniciAdi != "ADMIN";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class YetkilerToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>Enum.GetValues(typeof(İzinTipleri)).Cast<İzinTipleri>().Where(yetki => ((int)value & (int)yetki) == (int)yetki).ToList();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class BooleanAllNotNullConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => values.All(z => z != null);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    public class BooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => values.Any(z => (bool)z);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value == true ? Visibility.Visible : (object)Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class ByteArrayToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            try
            {
                int DecodeHeight = parameter!=null ? System.Convert.ToInt32(parameter) : 480;
                BitmapImage bmp;
                using (var webp = new WebP())
                {
                    bmp =Convert(webp.Decode(value as byte[]), DecodeHeight);
                }

                return bmp;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
        private static BitmapImage Convert(Image img, int PixelHeight)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Jpeg);
                memory.Seek(0, SeekOrigin.Begin);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.DecodePixelHeight = PixelHeight;
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }



    public class CloneConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => values.Clone();

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class ComboBoxKişiKoltukMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(z => z == null)) return DependencyProperty.UnsetValue;
            {
                try
                {
                    var kişi = values[0] as Seanslar;
                    var KoltukId = System.Convert.ToInt32(values[1]);
                    return kişi.Musteriler.FirstOrDefault(z => z.SeansID == kişi.SeansID && z.KoltukID == KoltukId);
                }
                catch (Exception)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class EnglishtoTurkishTranslateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null ? WebdenGetir.Translate((string)value, "en", "tr") : (string)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class IntToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var renk = Brushes.Transparent;
            switch ((int)value)
            {
                case 0:
                    return renk = Brushes.Transparent;

                case 1:
                    return renk = Brushes.Red;

                case 2:
                    return renk = Brushes.Green;

                case 3:
                    return renk = Brushes.Blue;

                case 4:
                    return renk = Brushes.Cyan;

                case 5:
                    return renk = Brushes.Magenta;

                case 6:
                    return renk = Brushes.Yellow;

                case 7:
                    return renk = Brushes.Orange;

                case 8:
                    return renk = Brushes.Purple;

                case 9:
                    return renk = Brushes.Lime;

                case 10:
                    return renk = Brushes.DarkCyan;
            }

            renk.Freeze();
            return renk;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    public class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var renk = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(string.Format("#{0:X6}", (int)value));
                return new SolidColorBrush(renk);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class KoltukNoSıraConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Koltuklar koltuklar
                ? koltuklar.KoltukNo % koltuklar.Salonlar.En == 0
                    ? Salonlar.SalonHarfleri().Take((int)(koltuklar.Salonlar.En * koltuklar.Salonlar.Boy)).ElementAtOrDefault((int)koltuklar.Salonlar.En - 1) + koltuklar.KoltukNo
                    : Salonlar.SalonHarfleri().Take((int)(koltuklar.Salonlar.En * koltuklar.Salonlar.Boy)).ElementAtOrDefault((int)(koltuklar.KoltukNo % koltuklar.Salonlar.En) - 1) + koltuklar.KoltukNo
                : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class MüşterilerKoltukNoSıraConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Koltuklar koltuklar
                ? koltuklar.KoltukNo % koltuklar.Salonlar.En == 0
                    ? Salonlar.SalonHarfleri().Take((int)(koltuklar.Salonlar.En * koltuklar.Salonlar.Boy)).ElementAtOrDefault((int)koltuklar.Salonlar.En - 1)
                    : Salonlar.SalonHarfleri().Take((int)(koltuklar.Salonlar.En * koltuklar.Salonlar.Boy)).ElementAtOrDefault((int)(koltuklar.KoltukNo % koltuklar.Salonlar.En) - 1)
                : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
    }

    public class NullToCollapsedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class ObjectToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class PathToFileInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null
                ? File.Exists((string)value) ? new FileInfo((string)value) : DependencyProperty.UnsetValue
                : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ((FileInfo)value)?.FullName ?? DependencyProperty.UnsetValue;
    }

    public class PathToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null ? new Uri((string)value) : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class SeansButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null
                ? (bool)value ? Visibility.Collapsed : Visibility.Visible
                : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    } 

    public class SeansTarihAndSalonAktifConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var seansgeçti = (bool)values[0];
            var salonaktifmi = (bool)values[1];
            return seansgeçti && salonaktifmi;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    public class SeansTarihGeçtiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (DateTime)value != default(DateTime)
                ? (DateTime)value > Seanslar.SeansKapanışSaati
                : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class SeansTarihGeçtiVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (DateTime)value != default(DateTime)
                ? (DateTime)value > Seanslar.SeansKapanışSaati ? Visibility.Visible :
                Visibility.Collapsed
                : DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class VideoRendererConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !EskiİşletimSistemi()
                ? VideoRendererType.EnhancedVideoRenderer
                : VideoRendererType.VideoMixingRenderer9;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        private static bool EskiİşletimSistemi() => Environment.OSVersion.Version.Major.ToString() == "5";
    }
}