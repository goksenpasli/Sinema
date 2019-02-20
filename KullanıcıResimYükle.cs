using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class KullanıcıResimYükle : ICommand
    {
        private static GirişWindow Form;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                Form = parameter as GirişWindow;

                if (!((Form.CbKullaniciAdi.SelectedItem as Kullanicilar)?.KullaniciSifre
                      == KullanıcıEkle.Sha256(Form.PbKullaniciSifre.Password)))
                {
                    MessageBox.Show("Seçili Kullanıcının Mevcut Şifresi Yanlıştır. Doğru Şifreyi Üste Girin.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (Form.C1ResimYükle.SelectedFile == null)
                {
                    MessageBox.Show("Resim Seçmediniz.", "Sinema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var db = SinemaModel.entities;
                var Kullanıcı = Form.CbKullaniciAdi.SelectedItem as Kullanicilar;
                var güncellenecek = db.Set<Kullanicilar>().FirstOrDefault(z => z.KullaniciID == Kullanıcı.KullaniciID);
                güncellenecek.Resim = ResimYükleme();
                db.SaveChanges();
                Form.CbKullaniciAdi.SelectedItem = null;
                Form.CbKullaniciAdi.SelectedItem = Kullanıcı;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        private static byte[] ResimYükleme()
        {
            var resim = Form.C1ResimYükle.SelectedFile;
            if (resim.Extension.ToLower() == ".webp") return File.ReadAllBytes(resim.FullName);
            var bmp = new Bitmap(resim.FullName);
            using (var webp = new WebP())
            {
                return webp.EncodeLossy(bmp, (int)GirişWindow.Kalite);
            }
        }
    }
}