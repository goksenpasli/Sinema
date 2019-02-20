using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class ŞifreDeğiştir : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var Form = parameter as GirişWindow;

                if ((Form.CbKullaniciAdi.SelectedItem as Kullanicilar)?.SifreEtkinMi == false)
                {
                    MessageBox.Show("Bu Kullanıcı Şifresi Pasiftir. Yöneticinize Başvurun.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Form.PbKullaniciYeniŞifre.Password))
                {
                    MessageBox.Show("Yeni Şifrenizi Girin.", "Sinema",MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if ((Form.CbKullaniciAdi.SelectedItem as Kullanicilar)?.KullaniciSifre
                    != KullanıcıEkle.Sha256(Form.PbKullaniciSifre.Password))
                {
                    MessageBox.Show("Seçili Kullanıcının Mevcut Şifresi Yanlıştır. Doğru Şifreyi Üste Girin.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var db = SinemaModel.entities;
                var KullanıcıID = (Form.CbKullaniciAdi.SelectedItem as Kullanicilar)?.KullaniciID;
                var Güncelleneck = db.Set<Kullanicilar>().FirstOrDefault(z => z.KullaniciID == KullanıcıID);
                Güncelleneck.KullaniciSifre = KullanıcıEkle.Sha256(Form.PbKullaniciYeniŞifre.Password);
                db.SaveChanges();
                MessageBox.Show("Şifre Değiştirilmiştir Yeni Şifrenizle Giriş Yapın.", "Sinema", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}