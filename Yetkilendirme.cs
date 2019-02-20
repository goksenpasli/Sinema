using System;
using System.Linq;
using System.Windows.Input;

namespace Sinema
{
    [Flags]
    public enum İzinTipleri
    {
        MüşteriEkle = 1,
        MüşteriSil = 2,
        FilmEkle = 4,
        SeansEkle = 8,
        SeansSil = 16,
        KoltukAyarla = 32,
        KoltukTipiEkle = 64,
        SalonAktifPasifYap = 128,
        SalonOluştur = 256,
        Yöneticiİşlemleri = 512,
        BiletYazdır = 1024,
        ÜrünEkle = 2048,
        ÜrünSat = 4096,
        KoltukGüncelle=8192,
        ÜrünFiyatıGüncelle=16384,
        ResimKaydet=32768

    }

    public static class Yetkilendir
    {
        public static Kullanicilar Kullanıcı { get; set; }

        public static bool İzinKontrolü<T>(this Enum type, T value)
        {
            try
            {
                return ((int)(object)type & (int)(object)value) == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static bool İzinVarmı(this Enum type) => ((İzinTipleri)Kullanıcı.KullaniciYetkisi).İzinKontrolü(type) && new Entities().Kullanicilar.FirstOrDefault(x => x.KullaniciID == Kullanıcı.KullaniciID).SifreEtkinMi == true;
    }

    public partial class SinemaModel
    {
        public ICommand TümMüşteriResimleriniKaydet => new TümMüşteriResimleriniKaydet();
        public ICommand TümMüşteriResimleriniSil => new TümMüşteriResimleriniSil();
        public ICommand YetkiGöster => new YetkiGöster();
    }
}