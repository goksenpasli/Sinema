using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class MüşteriSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.MüşteriSil.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var db = SinemaModel.entities;
                var Müşteri = parameter as Musteriler;

                if (db.Seanslar.FirstOrDefault(z => z.SeansID == Müşteri.SeansID).FilmSaati < Seanslar.SeansKapanışSaati)
                {
                    MessageBox.Show("Bu Seansın Süresi Dolmuştur. Silme Yapamazsınız.", "Sinema", MessageBoxButton.OK,
                       MessageBoxImage.Exclamation);
                    return;
                }

                if (MessageBox.Show($"{Müşteri.MusteriAdi} {Müşteri.MusteriSoyadi} Adlı Müşteriyi Silmek İstiyor Musun? Müşteriyle Birlikte Tahsilat da Silinecektir.",
                        "Sinema", MessageBoxButton.YesNo,
                        MessageBoxImage.Exclamation,
                        MessageBoxResult.No) != MessageBoxResult.Yes)
                    return;

                var silinecek = db.Musteriler.FirstOrDefault(z => z.MusteriID == Müşteri.MusteriID);
                db.Musteriler.Remove(silinecek);

                db.SaveChanges();
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}