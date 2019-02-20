using System;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class AylıkTahsilatGrafik : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                new Raporla("Sinema.Rapor.aylıkgrafik.frx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public class GünlükTahsilatGrafik : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                new Raporla("Sinema.Rapor.günlükgrafik.frx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public partial class Tahsilatlar
    {
        public ICommand AylıkTahsilatGrafik => new AylıkTahsilatGrafik();
        public ICommand GünlükTahsilatGrafik => new GünlükTahsilatGrafik();
    }
}