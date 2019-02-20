using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class KoltukGüncelle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.KoltukGüncelle.İzinVarmı();

        public void Execute(object parameter)
        {
            if (!Doğrula.Form.DgKoltukTürleri.Geçerli())
            {
                MessageBox.Show("Hatalı Girişleri Düzeltin.", "Sinema", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            try
            {
                var db = SinemaModel.entities;
                var Koltuk = parameter as KoltukTipleri;
                var güncellenecek = db.Set<KoltukTipleri>()
                    .FirstOrDefault(z => z.KoltukTipleriID == Koltuk.KoltukTipleriID);
                güncellenecek.KoltukFiyati = Koltuk.KoltukFiyati;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public partial class Koltuklar
    {
        public ICommand Güncelle => new KoltukGüncelle();
        public ICommand KoltukAyarla => new KoltukAyarla();
        public ICommand KoltukDurum => new KoltukDurumKaydet();
        public ICommand KoltukTipiDeğiştir => new KoltukTipiDeğiştir();
        public ICommand MüşteriEkle => new MüşteriEkle();
        public ICommand WebCamResimKaydet => new WebCamResimKaydet();
        public ICommand MüşteriDialog => new MüşteriDialogShow();

    }
}