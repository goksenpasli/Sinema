using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Sinema
{
    public partial class SinemaModel
    {
        public static Entities entities;


        public static ObservableCollection<Filmler> FilmlerCollection { get; set; }

        public static ObservableCollection<KoltukTipleri> KoltukTipleriCollection { get; set; }

        public static ObservableCollection<Kullanicilar> KullanicilarCollection { get; set; }

        public static ObservableCollection<Salonlar> SalonlarCollection { get; set; }

        public static ObservableCollection<Seanslar> SeanslarCollection { get; set; }

        public static ObservableCollection<Siparisler> SiparislerCollection { get; set; }

        public static ObservableCollection<Urunler> UrunlerCollection { get; set; }

        public static ObservableCollection<Musteriler> MusterilerCollection { get; set; }


        public SinemaModel()
        {
            //this.Database.Connection.ConnectionString += ";Password='1951753939'";
            
            entities = new Entities();
            
            entities.Salonlar.Load();
            entities.KoltukTipleri.Load();
            entities.Filmler.Load();
            entities.Seanslar.Load();
            entities.Kullanicilar.Load();
            entities.Siparisler.Load();
            entities.Urunler.Load();
            entities.Musteriler.Load();
            entities.Koltuklar.Load();

            KoltukTipleriCollection = entities.KoltukTipleri.Local;
            SalonlarCollection = entities.Salonlar.Local;
            FilmlerCollection = entities.Filmler.Local;
            SeanslarCollection = entities.Seanslar.Local;
            KullanicilarCollection = entities.Kullanicilar.Local;
            SiparislerCollection = entities.Siparisler.Local;
            UrunlerCollection = entities.Urunler.Local;
            MusterilerCollection = entities.Musteriler.Local;
        }
    }
 

}