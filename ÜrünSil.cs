using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class ÜrünSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ÜrünEkle.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Urunler = parameter as Urunler;
                if (MessageBox.Show(
                        $"{Urunler.UrunAdi} Adlı Ürünü Silmek İstiyor Musun? Dikkat Ürünü Silerseniz İlişkili Olan Siparişler De Silinecektir.",
                        "Sinema", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No)
                    != MessageBoxResult.Yes) return;

                var db = SinemaModel.entities;
                var silinecek = db.Urunler.FirstOrDefault(z => z.UrunID == Urunler.UrunID);
                db.Urunler.Remove(silinecek);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}