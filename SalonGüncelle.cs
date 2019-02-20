using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class SalonGüncelle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.SalonAktifPasifYap.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var db = SinemaModel.entities;
                var Salonlar = parameter as Salonlar;

                var güncellenecek = db.Set<Salonlar>().FirstOrDefault(z => z.SalonID == Salonlar.SalonID);
                güncellenecek.AktifMi = Salonlar.AktifMi;
                db.SaveChanges();
                CollectionViewSource.GetDefaultView(SinemaModel.SalonlarCollection).Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public partial class Salonlar
    {
        public ICommand Güncelle => new SalonGüncelle();
        public ICommand SeçimiKaldır => new SeçimiKaldır();
        public ICommand VideoResimKaydet => new VideoResimKaydet();
    }
}