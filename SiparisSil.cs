using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;
namespace Sinema
{
    public partial class Siparisler
    {
        public ICommand SiparisSil => new SiparisSil();
    }

    public class SiparisSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                if (MessageBox.Show("Seçili Siparişi Silmek İstiyor Musun?", "Sinema", MessageBoxButton.YesNo,
                        MessageBoxImage.Exclamation,
                        MessageBoxResult.No) != MessageBoxResult.Yes)
                    return;
                var Sipariş = parameter as Siparisler;
                var db = SinemaModel.entities;
                var silinecek = db.Siparisler.FirstOrDefault(z => z.SiparisID == Sipariş.SiparisID);
                db.Siparisler.Remove(silinecek);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}