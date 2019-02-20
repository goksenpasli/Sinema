using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class ResimKaydet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ResimKaydet.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                if (!(parameter is byte[] resim)) return;

                var dialog = new SaveFileDialog
                {
                    Filter = "Webp Image|*.webp",
                    FileName = "Resim"
                };
                if (dialog.ShowDialog() == true) File.WriteAllBytes(dialog.FileName, resim);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}