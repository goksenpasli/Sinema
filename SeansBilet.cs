using System;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class SalonBilet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.BiletYazdır.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                new Raporla("Sinema.Rapor.salonbilet.frx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public class SeansBilet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.BiletYazdır.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Seans = parameter as Seanslar;

                new Raporla("Sinema.Rapor.seansbilet.frx", "SeansID", Seans.SeansID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}