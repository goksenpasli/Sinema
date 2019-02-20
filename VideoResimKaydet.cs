using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using WPFMediaKit.DirectShow.Controls;

namespace Sinema
{
    public class VideoResimKaydet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ResimKaydet.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                ResimYakala.JpegDöndür(parameter as MediaUriElement, Folder + "\\Resim.jpg", 96);
                if (MessageBox.Show("Kaydedilen Resmi Açmak İstiyor Musun?", "Sinema", MessageBoxButton.YesNo,
                        MessageBoxImage.Exclamation, MessageBoxResult.No)
                    == MessageBoxResult.Yes) Process.Start(Folder + "\\Resim.jpg");
            }
            catch (Exception)
            {
            }
        }
    }
}