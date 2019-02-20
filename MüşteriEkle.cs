using C1.WPF;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sinema
{
    public class MüşteriEkle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.MüşteriEkle.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var db = SinemaModel.entities;
                var form = parameter as MüşteriEkleWindow;
                var SeansID = (form.CbFilmAdı.SelectedItem as Seanslar)?.SeansID; 

                if (db.Seanslar.FirstOrDefault(z=>z.SeansID==SeansID).FilmSaati < Seanslar.SeansKapanışSaati)
                {
                    MessageBox.Show("Bu Seansın Süresi Dolmuştur. Giriş Yapamazsınız.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                if (db.Seanslar.FirstOrDefault(z => z.SeansID == SeansID).Musteriler.Any(z =>
                      z.KoltukID == Convert.ToInt32(form.TxKoltukID.Text)))
                {
                    MessageBox.Show("Bu Seansta Bu Koltuğa Daha Önce Giriş Yapılmıştır.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrWhiteSpace(form.TxAdı.Text) || string.IsNullOrWhiteSpace(form.TxSoyadı.Text)
                    || string.IsNullOrWhiteSpace(form.TxYaşı.Text))
                {
                    MessageBox.Show("Kişinin Adını Soyadını ve Yaşını Girmeniz Gerekir.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }          


                var Müşteri = new Musteriler
                {
                    MusteriAdi = form.TxAdı.Text,
                    MusteriSoyadi = form.TxSoyadı.Text,
                    MusteriYasi = Convert.ToInt32(form.TxYaşı.Text),
                    KoltukID = Convert.ToInt32(form.TxKoltukID.Text),
                    SeansID = (form.CbFilmAdı.SelectedItem as Seanslar)?.SeansID,
                    KoltukGrup = form.TxKoltukSıra.Text,
                    KullaniciID = (Application.Current.Windows[0].Tag as Kullanicilar)?.KullaniciID,
                    Resim = ResimYükleme(form.C1MüşteriResimYükle)
                };

                Müşteri.Tahsilatlar.Add(new Tahsilatlar
                {
                    TahsilatTarihi = DateTime.Now,
                    Tutar = form.C1TahsilEdilen.Value
                });

                db.Musteriler.Add(Müşteri);
                Task.Factory.StartNew(() => db.SaveChanges()).ContinueWith(task => EkranTemizle(form), TaskScheduler.FromCurrentSynchronizationContext());
            }        

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }


        }

        private static void EkranTemizle(MüşteriEkleWindow form)
        {
            form.CbFilmAdı.SelectedIndex = -1;
            form.UgKişiBilgileri.Children.OfType<TextBox>().Where(z => z.IsEnabled).ToList().ForEach(z => z.Clear());
        }

        private static byte[] ResimYükleme(C1FilePicker C1MüşteriResim)
        {
            var resim = C1MüşteriResim.SelectedFile;
            if (resim == null) return null;
            {
                if (resim.Extension.ToLower() == ".webp") return File.ReadAllBytes(resim.FullName);

                var bmp = new Bitmap(resim.FullName);         

                using (var webp = new WebP())
                {
                    return webp.EncodeLossy(bmp, 50);
                }
            }
        }
    }
}