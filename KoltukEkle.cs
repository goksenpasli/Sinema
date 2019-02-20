using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public partial class KoltukTipleri
    {
        public ICommand Ekle => new KoltukEkle();

        public class KoltukEkle : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => İzinTipleri.KoltukTipiEkle.İzinVarmı();

            public void Execute(object parameter)
            {
                try
                {
                    if (!Doğrula.Form.UgKoltukGirişi.Geçerli())
                    {
                        MessageBox.Show("Hatalı Girişleri Düzeltin.", "Sinema", MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);
                        return;
                    }

                    var Koltuk = parameter as KoltukTipleri;
                    var db = SinemaModel.entities;
                    if (db.Set<KoltukTipleri>().Any(z => z.KoltukTipi == Koltuk.KoltukTipi))
                    {
                        MessageBox.Show("Bu İsimde Koltuk Tipi Oluşturulmuştur. Başka Bir İsim Girin.", "Sinema",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    var koltuk = new KoltukTipleri
                    {
                        KoltukTipi = Koltuk.KoltukTipi,
                        KoltukFiyati = Koltuk.KoltukFiyati,
                        KoltukRenk = 0
                    };

                    db.KoltukTipleri.Add(koltuk);
                    db.SaveChanges();
                    CollectionViewSource.GetDefaultView(SinemaModel.SalonlarCollection).Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
            }
        }
    }
}