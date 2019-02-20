using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class TopluGirişYap : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.MüşteriEkle.İzinVarmı();

        public void Execute(object parameter)
        {
            var db = SinemaModel.entities;
            try
            {
                var Seanslar = parameter as Seanslar;
                var başlangıçkoltuk = Seanslar.Başlangıç as Koltuklar;
                var bitişkoltuk = Seanslar.Bitiş as Koltuklar;

                if (db.Seanslar.FirstOrDefault(z => z.SeansID == Seanslar.SeansID).FilmSaati < Seanslar.SeansKapanışSaati)
                {
                    MessageBox.Show("Bu Seansın Süresi Dolmuştur. Giriş Yapamazsınız.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                if (başlangıçkoltuk.KoltukNo >= bitişkoltuk.KoltukNo)
                {
                    MessageBox.Show("Başlangıç Koltuk Numarası Bitiş Koltuk Numarasından Büyük Veya Eşit Olamaz.", "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    KoltukEkranTemizle(Seanslar);
                    return;
                }

                var KoltukAralığı = Enumerable.Range((int)başlangıçkoltuk.KoltukNo,
                    (int)bitişkoltuk.KoltukNo - (int)başlangıçkoltuk.KoltukNo + 1);


                if (db.Set<Musteriler>().Any(z =>
                    KoltukAralığı.Contains((int)z.Koltuklar.KoltukNo) && z.SeansID == Seanslar.SeansID
                    && (z.Koltuklar.KoltukGorunmezMi == true || z.Koltuklar.KoltukAktifMi == false)))
                {
                    MessageBox.Show("Belirtilen Aralıkta Pasif Veya Gizli Koltuk Vardır. Koltuk Seçiminizi Değiştirin.", "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    KoltukEkranTemizle(Seanslar);
                    return;
                }

                if (db.Set<Musteriler>().Any(z =>
                    KoltukAralığı.Contains((int)z.Koltuklar.KoltukNo) && z.SeansID == Seanslar.SeansID))
                {
                    MessageBox.Show("Belirtilen Aralıkta Kayıtlı Müşteri Vardır. Koltuk Seçiminizi Değiştirin.", "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    KoltukEkranTemizle(Seanslar);
                    return;
                }

                var AdSoyadYaşListesi = TopluGirişVerileri.Liste(Seanslar.TopluListe);
                if (AdSoyadYaşListesi == null)
                {
                    MessageBox.Show("Ad Soyad Yaş Bölümünde Hatalı Girişler Var Düzeltin.", "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (AdSoyadYaşListesi.Count != KoltukAralığı.Count())
                {
                    MessageBox.Show("Listeye Eklenen İsim Sayısı İle Seçim Yapılan Koltuk Aralık Sayısı Birbirine Eşit Değildir.", "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                MüşteriGirişi(Seanslar, başlangıçkoltuk, db, AdSoyadYaşListesi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
         
        }

        private static void KoltukEkranTemizle(Seanslar seanslar)
        {
            seanslar.Başlangıç = null;
            seanslar.Bitiş = null;
        }

        private static string KoltukGrup(Koltuklar başlangıçkoltuk, int i) => KoltukNo(başlangıçkoltuk, i) % başlangıçkoltuk.Salonlar.En == 0
                        ? Salonlar.SalonHarfleri().Take((int)(başlangıçkoltuk.Salonlar.En * başlangıçkoltuk.Salonlar.Boy)).ElementAtOrDefault((int)başlangıçkoltuk.Salonlar.En - 1)
                        : Salonlar.SalonHarfleri().Take((int)(başlangıçkoltuk.Salonlar.En * başlangıçkoltuk.Salonlar.Boy)).ElementAtOrDefault(
                            (int)(KoltukNo(başlangıçkoltuk, i) % başlangıçkoltuk.Salonlar.En) - 1);

        private static int KoltukID(Koltuklar başlangıçkoltuk, int i) => başlangıçkoltuk.KoltukID + i;

        private static int KoltukNo(Koltuklar başlangıçkoltuk, int i) => (int)başlangıçkoltuk.KoltukNo + i;

        private static int? KoltukTipi(Seanslar seanslar, Koltuklar başlangıçkoltuk, int i) => seanslar.Salonlar.Koltuklar.FirstOrDefault(z => z.KoltukID == KoltukID(başlangıçkoltuk, i))
                ?.KoltukTipi;

        private void MüşteriGirişi(Seanslar seanslar, Koltuklar başlangıçkoltuk, Entities db,
            ICollection<TopluGirişVerileri> AdSoyadYaşListesi)
        {
            var Müşteriler = new List<Musteriler>();
            for (var i = 0; i < AdSoyadYaşListesi.Count; i++)
            {
                var Müşteri = new Musteriler
                {
                    MusteriAdi = AdSoyadYaşListesi.ElementAtOrDefault(i)?.Ad,
                    MusteriSoyadi = AdSoyadYaşListesi.ElementAtOrDefault(i)?.Soyad,
                    MusteriYasi = AdSoyadYaşListesi.ElementAtOrDefault(i)?.Yaş,
                    KoltukID = KoltukID(başlangıçkoltuk, i),
                    SeansID = seanslar.SeansID,
                    KoltukGrup = KoltukGrup(başlangıçkoltuk, i),
                    KullaniciID = (Application.Current.Windows[0].Tag as Kullanicilar)?.KullaniciID
                };

                var KoltukTipi = TopluGirişYap.KoltukTipi(seanslar, başlangıçkoltuk, i);
                Müşteri.Tahsilatlar.Add(new Tahsilatlar
                {
                    TahsilatTarihi = DateTime.Now,
                    Tutar = SinemaModel.entities.KoltukTipleri.FirstOrDefault(x => x.KoltukTipleriID == KoltukTipi).KoltukFiyati
                });
                Müşteriler.Add(Müşteri);
            }

            db.Musteriler.AddRange(Müşteriler);
            db.SaveChanges();
            KoltukEkranTemizle(seanslar);
            seanslar.TopluListe = "";
        }
    }
}