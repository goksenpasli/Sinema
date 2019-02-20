using System;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class FilmVideoResimGüncelle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.FilmEkle.İzinVarmı();

        public void Execute(object parameter)
        {

            if (!Doğrula.Form.DgFilmler.Geçerli())
            {
                MessageBox.Show("Hatalı Girişleri Düzeltin.", "Sinema", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            try
            {
                var db = SinemaModel.entities;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}