using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sinema
{
    public abstract class ResimYakala
    {
        public static RenderTargetBitmap ConvertToBitmap(UIElement uiElement, double resolution)
        {
            var scale = resolution / 96d;

            uiElement.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            var sz = uiElement.DesiredSize;
            var rect = new Rect(sz);
            uiElement.Arrange(rect);

            var bmp = new RenderTargetBitmap((int)(scale * rect.Width), (int)(scale * rect.Height), scale * 96,
                scale * 96, PixelFormats.Default);
            bmp.Render(uiElement);

            return bmp;
        }  
       

        public static string CreateJpeg(BitmapSource bitmap)
        {
            var jpeg = new JpegBitmapEncoder();
            jpeg.Frames.Add(BitmapFrame.Create(bitmap));
            string result;

            using (var memoryStream = new MemoryStream())
            {
                jpeg.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memoryStream, Encoding.Default))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            return result;
        }

        public static void JpegDöndür(UIElement uiElement, string path, double resolution)
        {
            var jpegString = CreateJpeg(ConvertToBitmap(uiElement, resolution));

            if (path != null)
                try
                {
                    using (var fileStream = File.Create(path))
                    {
                        using (var streamWriter = new StreamWriter(fileStream, Encoding.Default))
                        {
                            streamWriter.Write(jpegString);
                        }
                    }
                }
                catch (Exception)
                {
                }
        }
    }
}