using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class KullanıcıSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.Yöneticiİşlemleri.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Kullanıcı = parameter as Kullanicilar;

                if (MessageBox.Show($"{Kullanıcı.KullaniciAdi} Adlı Kullanıcıyı Silmek İstiyor Musun?", "Sinema",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No)
                    != MessageBoxResult.Yes) return;

                var db = SinemaModel.entities;
                var silinecek = db.Kullanicilar.FirstOrDefault(z => z.KullaniciID == Kullanıcı.KullaniciID);
                db.Kullanicilar.Remove(silinecek);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}