using C1.WPF;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sinema
{
    public class SiparişEkle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.ÜrünSat.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                if (parameter != null)
                {
                    var values = (object[])parameter;
                    if (values.All(z => z != null))
                    {
                        var Cb = values[0] as ComboBox;
                        var C1NumericAdet = values[1] as C1NumericBox;
                        var MüşteriID = (Cb.DataContext as Musteriler)?.MusteriID;
                        var Ürün = Cb.SelectedItem as Urunler;

                        var db = SinemaModel.entities;
                        var sipariş = new Siparisler
                        {
                            MusteriID = MüşteriID,
                            SiparisAdet = C1NumericAdet.Value,
                            SiparisBirimFiyat = Ürün.BirimFiyati,
                            UrunID = Ürün.UrunID,
                            ToplamTutar = C1NumericAdet.Value * Ürün.BirimFiyati
                        };
                        db.Musteriler.FirstOrDefault(z => z.MusteriID == MüşteriID).Siparisler.Add(sipariş);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public partial class Urunler
    {
        public ObservableCollection<Urunler> UrunlerCollection { get; set; } = SinemaModel.entities.Urunler.Local;
    }
}