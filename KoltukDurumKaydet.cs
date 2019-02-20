using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public partial class Koltuklar
    {
        public class KoltukDurumKaydet : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => İzinTipleri.KoltukAyarla.İzinVarmı();

            public void Execute(object parameter)
            {
                try
                {
                    var db = SinemaModel.entities;
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