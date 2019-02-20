using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class KoltukAyarla : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.KoltukAyarla.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {

                var Salonlar = parameter as Salonlar;
                if (Salonlar.KoltukTipi == null)
                {
                    MessageBox.Show("Koltuk Seçimi Yapın.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                if (Salonlar.Başlangıç > Salonlar.Bitiş)
                {
                    MessageBox.Show("Başlangıç Numarası Bitiş Numarasından Büyük Olamaz.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (Salonlar.Bitiş < Salonlar.Başlangıç)
                {
                    MessageBox.Show("Bitiş Numarası Başlangıç Numarasından Küçük Olamaz.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (Salonlar.Bitiş > Salonlar.En * Salonlar.Boy)
                {
                    MessageBox.Show(
                        $"Salonun Toplam Koltuk Sayısı {Salonlar.En * Salonlar.Boy} Girdiğiniz Rakam Daha Yüksektir.",
                        "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var db = SinemaModel.entities;
                var seanssaati = Seanslar.SeansKapanışSaati;

                if (Salonlar.Dikdörtgenuygula)
                {
                    if (Salonlar.Başlangıç > Salonlar.Bitiş)
                    {
                        MessageBox.Show("Başlangıç Numarası Bitiş Numarasından Büyük Olamaz.", "Sinema",
                       MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    for (int j = 0; j <= (Salonlar.Bitiş - Salonlar.Başlangıç) / Salonlar.En; j++)
                    {
                        for (int i = 0; i <= ((Salonlar.Bitiş - Salonlar.Başlangıç) % Salonlar.En); i++)
                        {
                            var koltuk = db.Set<Koltuklar>()
                            .Where(z => z.SalonID == Salonlar.SalonID && z.KoltukNo == (i + Salonlar.Başlangıç + j * Salonlar.En)).FirstOrDefault();
                            koltuk.KoltukTipi = (Salonlar.KoltukTipi as KoltukTipleri)?.KoltukTipleriID;
                            if (!db.Set<Musteriler>().Any(z =>
                                z.Koltuklar.KoltukNo == (i + Salonlar.Başlangıç + j * Salonlar.En) && z.Koltuklar.SalonID == Salonlar.SalonID
                                && z.Seanslar.FilmSaati > seanssaati))
                            {
                                koltuk.KoltukAktifMi = Salonlar.KoltukEtkinMi;
                                koltuk.KoltukGorunmezMi = Salonlar.KoltukGörünmez;
                            }
                        }
                    }
                }

                if (Salonlar.DikeyUygula)
                {
                    if ((Salonlar.Bitiş - Salonlar.Başlangıç) % Salonlar.En != 0)
                    {
                        MessageBox.Show("Seçilen Koltuk Aralığı Dikey Bir Sıra Belirtimiyor. Dikey Sıra Belirten Koltuk Seçin.", "Sinema",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    for (var i = Salonlar.Başlangıç; i <= Salonlar.Bitiş; i += (int)Salonlar.En)
                    {
                        var koltuk = db.Set<Koltuklar>()
                            .Where(z => z.SalonID == Salonlar.SalonID && z.KoltukNo == i).FirstOrDefault();
                        koltuk.KoltukTipi = (Salonlar.KoltukTipi as KoltukTipleri)?.KoltukTipleriID;
                        if (!db.Set<Musteriler>().Any(z =>
                            z.Koltuklar.KoltukNo == i && z.Koltuklar.SalonID == Salonlar.SalonID
                            && z.Seanslar.FilmSaati > seanssaati))
                        {
                            koltuk.KoltukAktifMi = Salonlar.KoltukEtkinMi;
                            koltuk.KoltukGorunmezMi = Salonlar.KoltukGörünmez;
                        }
                    }
                }

                if (Salonlar.YatayUygula)
                {
                    foreach (var koltuklar in db.Set<Koltuklar>().Where(z =>
                        z.SalonID == Salonlar.SalonID && z.KoltukNo >= Salonlar.Başlangıç
                        && z.KoltukNo <= Salonlar.Bitiş))
                    {
                        koltuklar.KoltukTipi = (Salonlar.KoltukTipi as KoltukTipleri)?.KoltukTipleriID;
                        if (!koltuklar.Musteriler.Any(z => z.Seanslar.FilmSaati > seanssaati))
                        {
                            koltuklar.KoltukAktifMi = Salonlar.KoltukEtkinMi;
                            koltuklar.KoltukGorunmezMi = Salonlar.KoltukGörünmez;
                        }
                    }
                }

                db.SaveChanges();
                CollectionViewSource.GetDefaultView(SinemaModel.SalonlarCollection).Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}