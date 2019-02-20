using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class TümMüşteriResimleriniSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.Yöneticiİşlemleri.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                if (MessageBox.Show("Tüm Müşteri Resimlerini Silmek İstiyor Musun? Bu İşlem Geri Alınmaz.", "Sinema", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No) != MessageBoxResult.Yes)
                    return;

                var db = SinemaModel.entities;
                db.Musteriler.ToList().ForEach(z => z.Resim = null);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }

}