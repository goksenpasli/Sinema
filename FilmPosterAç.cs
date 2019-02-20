using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class FilmPosterAç : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                if (MessageBox.Show("Resmi Açmak İstiyor Musun?",
                     "Sinema", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No)
                 != MessageBoxResult.Yes) return;
                var element = parameter as System.Xml.XmlElement;
                var posteradress = element.SelectSingleNode("/root/movie/@poster").Value;
                Process.Start(posteradress);
            }
            catch (Exception)
            {
            }
        }
    }

}