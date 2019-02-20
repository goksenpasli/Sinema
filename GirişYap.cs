using System;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class GirişYap : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var GirişWindow = parameter as GirişWindow;
                Yetkilendir.Kullanıcı = GirişWindow.CbKullaniciAdi.SelectedItem as Kullanicilar;

                if ((GirişWindow.CbKullaniciAdi.SelectedItem as Kullanicilar)?.SifreEtkinMi == false)
                {
                    MessageBox.Show("Bu Kullanıcı Şifresi Pasiftir. Yöneticinize Başvurun.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if ((GirişWindow.CbKullaniciAdi.SelectedItem as Kullanicilar)?.KullaniciSifre == KullanıcıEkle.Sha256(GirişWindow.PbKullaniciSifre.Password))
                {
                    var window = new MainWindow
                    {
                        Tag = GirişWindow.CbKullaniciAdi.SelectedItem as Kullanicilar
                    };
                    var A = new A(); 
                    if (A.Check(false))
                    {
                        window.Show();
                        GirişWindow.Close();
                    }
                    else
                    {
                        window.Close();
                        GirişWindow.Close();
                    }                       

                }
                else
                {
                    MessageBox.Show("Girilen Şifre Hatalıdır.", "Sinema", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}