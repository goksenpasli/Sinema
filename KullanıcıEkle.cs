using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Sinema
{
    public class KullanıcıEkle : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public static string Sha256(string şifre)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            foreach (var theByte in crypt.ComputeHash(Encoding.UTF8.GetBytes(şifre)))
                hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public bool CanExecute(object parameter) => İzinTipleri.Yöneticiİşlemleri.İzinVarmı();

        public void Execute(object parameter)
        {
            try
            {
                var Form = parameter as Yönetici;
                if (string.IsNullOrWhiteSpace(Form?.PbŞifre.Password)
                    || string.IsNullOrWhiteSpace(Form.TxKullaniciAdi.Text)
                    || string.IsNullOrWhiteSpace(Form.TxKullaniciTc.Text))
                {
                    MessageBox.Show("Ad Tc Şifre Alanlarının Tümünün Dolu Olması Gerekir.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var db = SinemaModel.entities;
                if (db.Set<Kullanicilar>().Any(z => z.KullaniciTc == Form.TxKullaniciTc.Text))
                {
                    MessageBox.Show("Bu Tc Kimlik Numarasıyla Kullanıcı Kayıtlıdır. Başka Bir Tc Girin.", "Sinema",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                var VarsayılanYetkiler = (int)(İzinTipleri.MüşteriEkle | İzinTipleri.MüşteriSil | İzinTipleri.FilmEkle | İzinTipleri.SeansEkle | İzinTipleri.BiletYazdır | İzinTipleri.ÜrünSat);
                var kullanıcı = new Kullanicilar
                {
                    KullaniciAdi = Form.TxKullaniciAdi.Text,
                    KullaniciSifre = Sha256(Form?.PbŞifre.Password),
                    KullaniciTc = Form.TxKullaniciTc.Text,
                    KullaniciYetkisi = VarsayılanYetkiler,
                    SifreEtkinMi = true
                };

                db.Kullanicilar.Add(kullanıcı);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Sinema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }

    public partial class Kullanicilar
    {
        public ICommand GirişYap => new GirişYap();
        public ICommand KullanıcıEkle => new KullanıcıEkle();
        public ICommand KullanıcıGüncelle => new KullanıcıGüncelle();
        public ICommand KullanıcıResimYükle => new KullanıcıResimYükle();
        public ICommand KullanıcıSil => new KullanıcıSil();
        public ICommand ResimDrop => new ResimDrop();
        public ICommand ResimKaydet => new ResimKaydet();
        public ICommand KullanıcıResimSil => new KullanıcıResimSil();
        public ICommand ŞifreDeğiştir => new ŞifreDeğiştir();
        public ICommand YöneticiEkranı => new YöneticiEkranı();
    }
}