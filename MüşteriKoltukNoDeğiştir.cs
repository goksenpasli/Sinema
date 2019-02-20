using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sinema
{
    public class MüşteriKoltukNoDeğiştir : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var Cb = parameter as ComboBox;
                var Müşteri = Cb.DataContext as Musteriler;
                var Koltuk = Cb.SelectedItem as Koltuklar;
                var MevcutKoltukID = Müşteri?.KoltukID;
                var DeğiştirilecekKoltukID = Koltuk?.KoltukID;
                var KoltukNo = Koltuk?.KoltukNo;
                if (Koltuk.Musteriler.Any(z => z.Seanslar.FilmSaati > Seanslar.SeansKapanışSaati
                    && z.SeansID == Müşteri.Seanslar.SeansID))
                {
                    MessageBox.Show($"Bu Seansta {KoltukNo} Nolu Koltukta {Koltuk.Musteriler.FirstOrDefault(z=>z.Seanslar.SeansID==Müşteri.SeansID)?.MusteriAdi} {Koltuk.Musteriler.FirstOrDefault(z => z.Seanslar.SeansID == Müşteri.SeansID)?.MusteriSoyadi} Adlı Müşteri Var.",
                        "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (Müşteri.Koltuklar.KoltukTipleri.KoltukFiyati != Koltuk.KoltukTipleri.KoltukFiyati)
                {
                    MessageBox.Show($"{Müşteri.MusteriAdi} {Müşteri.MusteriSoyadi} Adlı Müşteri Bulunduğu Koltuğa {Müşteri.Koltuklar.KoltukTipleri.KoltukFiyati:C} Ödeme Yapmıştır. Taşınan Koltuk {Koltuk.KoltukTipleri.KoltukFiyati:C} Olduğundan Farkın Tahsili/İadesi Gerekir.",
                        "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                var db = SinemaModel.entities;
                var güncellenecek = db.Set<Musteriler>().FirstOrDefault(z =>
                    (z.KoltukID == MevcutKoltukID) & (z.MusteriID == Müşteri.MusteriID));
                güncellenecek.KoltukID = DeğiştirilecekKoltukID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}