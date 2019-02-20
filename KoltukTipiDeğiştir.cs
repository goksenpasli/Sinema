using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class KoltukTipiDeğiştir : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.KoltukAyarla.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                if (parameter != null)
                {
                    var values = (object[])parameter;
                    if (values.All(z => z != null))
                    {
                        var Koltuk = (Koltuklar)values[0];
                        var KoltukTipleri = (KoltukTipleri)values[1];
                        var db = SinemaModel.entities;
                        if (Koltuk.KoltukTipi == KoltukTipleri.KoltukTipleriID) return;
                        Koltuk.KoltukTipi = KoltukTipleri.KoltukTipleriID;
                        db.SaveChanges();                       
                        CollectionViewSource.GetDefaultView(SinemaModel.SalonlarCollection).Refresh();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}