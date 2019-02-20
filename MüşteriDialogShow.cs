using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class MüşteriDialogShow : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var müşteriwindow = new MüşteriEkleWindow
                {
                    Owner = Application.Current.Windows[0]
                };
                
                SinemaModel.entities.Musteriler.Load();
                SinemaModel.entities.Siparisler.Load();
                müşteriwindow.DataContext = parameter as Koltuklar;
                müşteriwindow.C1MusteriKayıtlar.ItemsSource = (parameter as Koltuklar)?.Musteriler;
                müşteriwindow.SourceInitialized += (x, y) => müşteriwindow.HideMinimizeButtons();
                müşteriwindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}