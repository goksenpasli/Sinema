using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class SalonEkle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.SalonOluştur.İzinVarmı();

        public void Execute(object parameter)
        {
            var db = SinemaModel.entities;
            try
            {
                var Salonlar = parameter as Salonlar;

                if (string.IsNullOrWhiteSpace(Salonlar.SalonAdi) || Salonlar.En == null || Salonlar.Boy == null)
                {
                    MessageBox.Show("Salon Adını Girmediniz veya Oluşturulacak Salon Boyutunda Hata Var.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (db.Salonlar.Any(z => z.SalonAdi == Salonlar.SalonAdi))
                {
                    MessageBox.Show("Bu İsimde Salon Oluşturulmuştur. Başka Bir İsim Girin.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                db.Configuration.AutoDetectChangesEnabled = false;
                var salon = new Salonlar
                {
                    SalonAdi = Salonlar.SalonAdi,
                    En = Salonlar.En,
                    Boy = Salonlar.Boy,
                    AktifMi = true,
                    Başlangıç = 1,
                    Bitiş = 1,
                    KoltukEtkinMi = true,
                    KoltukDetay = false
                };
                var salonlar = new List<Salonlar>();
                for (var i = 1; i <= Salonlar.En * Salonlar.Boy; i++)
                {
                    salon.Koltuklar.Add(new Koltuklar
                    {
                        KoltukAktifMi = true,
                        KoltukNo = i,
                        KoltukTipi = db.KoltukTipleri.Local[0].KoltukTipleriID,
                        KoltukGorunmezMi = false
                    });
                    salonlar.Add(salon);
                }

                db.Salonlar.AddRange(salonlar);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Configuration.AutoDetectChangesEnabled = true;
            }
        }
    }
}