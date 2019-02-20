using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class YetkiGöster : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var Form = parameter as Yönetici;
                if (!(Form.DgKullanicilar.SelectedItem is Kullanicilar Kullanici)) return;
                Form.C1ChkListYetkiler.SelectedItems = Form.C1ChkListYetkiler.Items.Cast<İzinTipleri>().Where(yetki =>
                    (Kullanici.KullaniciYetkisi & (int)yetki) == (int)yetki).ToList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}