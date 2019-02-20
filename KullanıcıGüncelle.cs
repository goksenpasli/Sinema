using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class KullanıcıGüncelle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.Yöneticiİşlemleri.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Kullanıcı = parameter as Kullanicilar;
                var db = SinemaModel.entities;
                var güncellenecek = db.Set<Kullanicilar>().FirstOrDefault(z => z.KullaniciID == Kullanıcı.KullaniciID);
                güncellenecek.KullaniciYetkisi = (Application.Current.Windows.OfType<Window>().FirstOrDefault(z => z.Title == "Yönetici") as Yönetici)?.C1ChkListYetkiler.SelectedItems
                    .OfType<İzinTipleri>().Sum(z => (int)z);
                güncellenecek.SifreEtkinMi = Kullanıcı.SifreEtkinMi;
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}