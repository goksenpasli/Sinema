using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sinema
{
    public class SeçimiKaldır : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var Datagrid = parameter as DataGrid;
                Datagrid.SelectedItem = null;
            }
            catch (Exception)
            {
            }
        }
    }
}