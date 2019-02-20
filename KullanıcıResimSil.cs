using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class KullanıcıResimSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                if (!(parameter is Kullanicilar kullanicilar)) return;

                if (MessageBox.Show("Seçili Resmi Silmek İstiyor Musun?", "Sinema", MessageBoxButton.YesNo,
                        MessageBoxImage.Exclamation, MessageBoxResult.No) != MessageBoxResult.Yes) return;
                var db = SinemaModel.entities;
                db.Kullanicilar.FirstOrDefault(z => z.KullaniciID == kullanicilar.KullaniciID).Resim = null;
                (Application.Current.Windows[0] as MainWindow).ImgKullanıcı.Source = null;
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}