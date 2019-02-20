using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class SeansEkle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.SeansEkle.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var form = Application.Current.Windows[0] as MainWindow;

                if ((form.CbSalon.SelectedItem as Salonlar)?.AktifMi == false)
                {
                    MessageBox.Show("Bu Salon Pasiftir. Seans Oluşturamazsın.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                if (form.CbSalon.SelectedIndex == -1 || form.CbFilmler.SelectedIndex == -1)
                {
                    MessageBox.Show("Film Veya Salon Seçmediniz.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                if (form.C1SeansTarih.DateTime.Value<DateTime.Now)
                {
                    MessageBox.Show("Seansın Saati Şu Anki Saatten Küçük Olamaz.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                const double DakikaAralığı = 30;
                var Sonraki = form.C1SeansTarih.DateTime.Value.AddMinutes(DakikaAralığı);
                var Önceki = form.C1SeansTarih.DateTime.Value.AddMinutes(-DakikaAralığı);
                var SalonId = (form.CbSalon.SelectedItem as Salonlar)?.SalonID;
                var db = SinemaModel.entities;
                if (db.Set<Seanslar>().Any(z => z.SalonID == SalonId && z.FilmSaati < Sonraki && z.FilmSaati > Önceki))
                {
                    MessageBox.Show($"Bu Salonda Bu Tarihten {DakikaAralığı} Dakika Öncesinde Veya Sonrasında Seans Vardır.",
                        "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var seans = new Seanslar
                {
                    FilmID = (form.CbFilmler.SelectedItem as Filmler)?.FilmID,
                    SalonID = (form.CbSalon.SelectedItem as Salonlar)?.SalonID,
                    FilmSaati = form.C1SeansTarih.DateTime.Value,
                    Renk = RenkOluştur()
                };
                db.Seanslar.Add(seans);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        private static int? RenkOluştur()
        {
            Random rd = new Random();
            var BilinenRenkİsimleri = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor[] HariçRenkler = { KnownColor.Black };
            var Renkler = BilinenRenkİsimleri.Except(HariçRenkler).Skip(27).ToArray();
            var RastgeleRenk = Renkler[rd.Next(Renkler.Count())];
            return Color.FromKnownColor(RastgeleRenk).ToArgb();
        }
    }

    public partial class Seanslar
    {

        public static DateTime SeansKapanışSaati = DateTime.Now.AddMinutes(-30); //Seansın Kaç Dakika Geç Kapanmasını Belirtir. Varsayılan Olarak 30 Dakikadır. Seansın Başlamasından 30 Dakika Sonra Seans Pasif Olur Giriş Yapılmaz.

        public ICommand Ekle => new SeansEkle();
        public ICommand FilmPosterAç => new FilmPosterAç();
        public ICommand Sil => new SeansSil();
        public ICommand TopluGirişYap => new TopluGirişYap();
        public ICommand WebdenGetir => new WebdenGetir();
        public ICommand Yazdır => new SeanslarGridYazdır();
    }
}