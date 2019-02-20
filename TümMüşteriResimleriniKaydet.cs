using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class TümMüşteriResimleriniKaydet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.Yöneticiİşlemleri.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {

                if (MessageBox.Show("Müşteri Resimleri Resimler Klasörüne Kaydedilecek. Devam Etmek İstiyor Musun?", "Sinema", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No) != MessageBoxResult.Yes)
                    return;

                SinemaModel.entities.Musteriler.ToList().ForEach(z =>
                {
                    if (z.Resim != null)
                    {
                        File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" + z.MusteriID.ToString() + ".webp", z.Resim);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }

}