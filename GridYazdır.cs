using C1.WPF.DataGrid;
using System;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class SeanslarGridYazdır : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var C1DataGridSeansStylelar = parameter as C1DataGrid;
            var scaleMode = (ScaleMode)Enum.Parse(typeof(ScaleMode), "PageWidth", false);
            C1DataGridSeansStylelar.Print("Dosya", scaleMode, new Thickness(20), int.MaxValue);
        }
    }

    
}