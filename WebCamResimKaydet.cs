using C1.WPF;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMediaKit.DirectShow.Controls;

namespace Sinema
{
    public class WebCamResimKaydet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ResimKaydet.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var ResimYolu = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Webcam.jpg";
                ResimYakala.JpegDöndür(parameter as VideoCaptureElement, ResimYolu, 96);
                var C1MüşteriResim =
                    ((parameter as VideoCaptureElement).Parent as Grid).FindName("C1MüşteriResimYükle") as C1FilePicker;
                C1MüşteriResim.SelectedFile = new FileInfo(ResimYolu);
            }
            catch (Exception)
            {
            }
        }
    }
}