using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class ÜrünGüncelle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ÜrünFiyatıGüncelle.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Urunler = parameter as Urunler;
                var db = SinemaModel.entities;
                var güncellenecek = db.Urunler.FirstOrDefault(z => z.UrunID == Urunler.UrunID);
                güncellenecek.BirimFiyati = Urunler.BirimFiyati;
                güncellenecek.UrunSatilabilirmi = Urunler.UrunSatilabilirmi;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}