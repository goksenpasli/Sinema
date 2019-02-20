using System;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public partial class Musteriler
    {
        public ICommand MüşteriBilet => new MüşteriBilet();
        public ICommand MüşteriKoltukNoDeğiştir => new MüşteriKoltukNoDeğiştir();
        public ICommand MüşteriSil => new MüşteriSil();
        public ICommand SalonBilet => new SalonBilet();
        public ICommand SeansBilet => new SeansBilet();
        public ICommand SiparişEkle => new SiparişEkle();

    }

    public class MüşteriBilet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.BiletYazdır.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Müşteri = parameter as Musteriler;

                new Raporla("Sinema.Rapor.bilet.frx", "MusteriID", Müşteri.MusteriID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}