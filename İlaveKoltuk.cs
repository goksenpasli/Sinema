using C1.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class İlaveKoltukEkle : ICommand
    {
        private List<Tuple<int, int>> liste;

        public event EventHandler CanExecuteChanged;

        public void C1NumericBoxChanged(object sender, PropertyChangedEventArgs<double> e)
        {
            try
            {
                var İlaveKoltukSayısı = e.NewValue;
                var ToplamKoltukSayısı =
                    (((sender as C1NumericBox)?.DataContext as Yönetici)?.CbSalon.SelectedItem as Salonlar)?.Boy
                    * (((sender as C1NumericBox)?.DataContext as Yönetici)?.CbSalon.SelectedItem as Salonlar)?.En
                    + İlaveKoltukSayısı;
                KoltukGruplaListeyeEkle(ToplamKoltukSayısı);
                ((sender as C1NumericBox)?.DataContext as Yönetici).CbDizilim.SelectedItem = null;
                ((sender as C1NumericBox)?.DataContext as Yönetici).CbDizilim.ItemsSource = liste;
            }
            catch (Exception)
            {
            }
        }

        public bool CanExecute(object parameter) => true;

        public void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var İlaveKoltukSayısı = ((sender as ComboBox)?.DataContext as Yönetici)?.C1İlaveKoltuk.Value;
                var ToplamKoltukSayısı =
                    ((sender as ComboBox)?.SelectedItem as Salonlar)?.Boy
                    * ((sender as ComboBox)?.SelectedItem as Salonlar)?.En + İlaveKoltukSayısı;
                KoltukGruplaListeyeEkle(ToplamKoltukSayısı);
                ((sender as ComboBox)?.DataContext as Yönetici).CbDizilim.ItemsSource = liste;
            }
            catch (Exception)
            {
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                var CbSalon = (parameter as Yönetici)?.CbSalon;
                var İlaveKoltukSayısı = (parameter as Yönetici)?.C1İlaveKoltuk.Value;
                var CbDizilim = (parameter as Yönetici)?.CbDizilim;
                if ((CbSalon.SelectedItem as Salonlar).Koltuklar.Any(z => z.KoltukGorunmezMi == true))
                {
                    MessageBox.Show(
                        "Bu Salonda Gizlenmiş Koltuk Bulunmaktadır. Koltuk Eklemeden Önce Tüm Gizlenmiş Koltukları Görünür Yapın.",
                        "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if ((CbSalon.SelectedItem as Salonlar)?.Boy * (CbSalon.SelectedItem as Salonlar)?.En
                    + İlaveKoltukSayısı > 3600)
                {
                    MessageBox.Show("Azami Koltuk Sayısı Olan 3600 Değerini Geçemezsiniz.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var db = SinemaModel.entities;
                var SeçiliSalon = CbSalon.SelectedItem as Salonlar;
                var EnSonKoltukNo = SeçiliSalon.Boy * SeçiliSalon.En;
                SeçiliSalon.Boy = (CbDizilim.SelectedItem as Tuple<int, int>).Item1;
                SeçiliSalon.En = (CbDizilim.SelectedItem as Tuple<int, int>).Item2;

                var koltuklar = new List<Koltuklar>();
                for (var i = (int)EnSonKoltukNo + 1; i < EnSonKoltukNo + 1 + İlaveKoltukSayısı; i++)
                {
                    var koltuk = new Koltuklar
                    {
                        KoltukNo = i,
                        KoltukTipi = db.KoltukTipleri.Local[0].KoltukTipleriID,
                        KoltukGorunmezMi = false,
                        SalonID = SeçiliSalon.SalonID,
                        KoltukAktifMi = true
                    };
                    koltuklar.Add(koltuk);
                }

                db.Koltuklar.AddRange(koltuklar);
                db.SaveChanges();
                CollectionViewSource.GetDefaultView(SinemaModel.SalonlarCollection).Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        private void KoltukGruplaListeyeEkle(double? ToplamKoltukSayısı)
        {
            liste = new List<Tuple<int, int>>();
            liste.Clear();
            KoltukGrupla.Bölenler((int)ToplamKoltukSayısı).GroupBy(2).ToList().ForEach(z =>
           {
               liste.Add(new Tuple<int, int>(z.ElementAt(0), z.ElementAt(1)));
               liste.Add(new Tuple<int, int>(z.ElementAt(1), z.ElementAt(0)));
           });
        }
    }

    public partial class Yönetici
    {
        public ICommand İlaveKoltukEkle => new İlaveKoltukEkle();
    }
}