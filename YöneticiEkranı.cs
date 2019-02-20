using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sinema
{
    public class YöneticiEkranı : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => İzinTipleri.Yöneticiİşlemleri.İzinVarmı();

        public void Execute(object parameter)
        {
                var yönetici = new Yönetici
                {
                    Owner = Application.Current.Windows[0],
                    DataContext = parameter
                };
                yönetici.ShowDialog();
       
        }
    }

}