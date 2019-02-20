using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Linq;
namespace Sinema
{
    public class SeansSil : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.SeansSil.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var form = Application.Current.Windows[0] as MainWindow;
                var seans = (Seanslar)form.DgSeanslar.SelectedItem;

                if (seans.FilmSaati < Seanslar.SeansKapanışSaati)
                {
                    MessageBox.Show(seans.Filmler.FilmAdi + " Adlı Filmin Tarihi Geçmiştir. Silemezsin.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (MessageBox.Show(
                        seans.Filmler.FilmAdi +
                        " Adlı Filmi Bu Salondan Silmek İstiyor Musun? Dikkat Seansı Silerseniz Bu Seanstaki Müşteriler, Tahsilatlar, Siparişler de Silinecektir.",
                        "Sinema", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No)
                    != MessageBoxResult.Yes) return;

                var db = SinemaModel.entities;
                var silinecek =db.Seanslar.FirstOrDefault(z=>z.SeansID==seans.SeansID);
                db.Seanslar.Remove(silinecek);
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