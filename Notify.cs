using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Input;

namespace Sinema
{
    public partial class Koltuklar : INotifyPropertyChanged
    {
        private int? _koltukÖnizlemeKolonSayısı = 3;
        private double _videoşeffaflık = 1;

        [NotMapped]
        public int? KoltukÖnizlemeKolonSayısı
        {
            get => _koltukÖnizlemeKolonSayısı;

            set
            {
                if (_koltukÖnizlemeKolonSayısı != value)
                {
                    _koltukÖnizlemeKolonSayısı = value;
                    OnPropertyChanged(nameof(KoltukÖnizlemeKolonSayısı));
                }
            }
        }

        [NotMapped]
        public double VideoŞeffaflık
        {
            get => _videoşeffaflık;

            set
            {
                if (_videoşeffaflık != value)
                {
                    _videoşeffaflık = value;
                    OnPropertyChanged(nameof(VideoŞeffaflık));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public partial class KoltukTipleri : IDataErrorInfo
    {
        public string Error => "";

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "KoltukFiyati")
                {
                    if (KoltukFiyati == null || KoltukFiyati <= 0)
                        result = "Koltuk Fiyatını Yazın.";
                }

                if (columnName == "KoltukTipi" && string.IsNullOrWhiteSpace(KoltukTipi))
                {
                    result = "Koltuk Tipini Yazın.";
                }

                return result;
            }
        }
    }

    public partial class Filmler : IDataErrorInfo
    {
        public string Error => "";

        public string this[string columnName]
        {
            get
            {
                string result = null;      

                if (columnName == "FilmAdi" && string.IsNullOrWhiteSpace(FilmAdi))
                {
                    result = "Film Adını Yazın.";
                }

                return result;
            }
        }
    }

    public partial class Salonlar : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _başlangıç = 1;
        private int _bitiş = 1;
        private bool _dikdörtgenuygula;
        private bool _dikeyUygula;
        private bool _koltukEtkinMi = true;
        private bool _koltukGörünmez;
        private object _koltukTipi;
        private double _SesYüksekliği = 1;
        private IEnumerable<int> _toplamkoltuk;
        private bool _yatayuygula = true;

        [NotMapped]
        public int Başlangıç
        {
            get => _başlangıç;
            set
            {
                if (_başlangıç != value)
                {
                    _başlangıç = value;
                    OnPropertyChanged(nameof(Başlangıç));
                    OnPropertyChanged(nameof(Bitiş));
                }
            }
        }

        [NotMapped]
        public int Bitiş
        {
            get => _bitiş;

            set
            {
                if (_bitiş != value)
                {
                    _bitiş = value;
                    OnPropertyChanged(nameof(Bitiş));
                    OnPropertyChanged(nameof(Başlangıç));
                }
            }
        }

        [NotMapped]
        public bool Dikdörtgenuygula
        {
            get => _dikdörtgenuygula;
            set
            {
                if (_dikdörtgenuygula != value)
                {
                    _dikdörtgenuygula = value;
                    OnPropertyChanged(nameof(Dikdörtgenuygula));
                }
            }
        }

        [NotMapped]
        public bool DikeyUygula
        {
            get => _dikeyUygula;
            set
            {
                if (_dikeyUygula != value)
                {
                    _dikeyUygula = value;
                    OnPropertyChanged(nameof(DikeyUygula));
                }
            }
        }

        public ICommand Ekle => new SalonEkle();

        public string Error => "";

        [NotMapped]
        public bool KoltukEtkinMi
        {
            get => _koltukEtkinMi;
            set
            {
                if (_koltukEtkinMi != value)
                {
                    _koltukEtkinMi = value;
                    OnPropertyChanged(nameof(KoltukEtkinMi));
                }
            }
        }

        [NotMapped]
        public bool KoltukGörünmez
        {
            get => _koltukGörünmez;
            set
            {
                if (_koltukGörünmez != value)
                {
                    _koltukGörünmez = value;
                    OnPropertyChanged(nameof(KoltukGörünmez));
                }
            }
        }

        [NotMapped]
        public object KoltukTipi
        {
            get => _koltukTipi;

            set
            {
                if (_koltukTipi != value)
                {
                    _koltukTipi = value;
                    OnPropertyChanged(nameof(KoltukTipi));
                }
            }
        }

        [NotMapped]
        public double SesYüksekliği
        {
            get => _SesYüksekliği;
            set
            {
                if (_SesYüksekliği != value)
                {
                    _SesYüksekliği = value;
                    OnPropertyChanged(nameof(SesYüksekliği));
                }
            }
        }

        public IEnumerable<int> Toplamkoltuk
        {
            get
            {
                if (En != null && Boy != null) return Enumerable.Range(1, (int)En * (int)Boy);
                return Enumerable.Range(1, 0);
            }

            set
            {
                _toplamkoltuk = value;
                OnPropertyChanged(nameof(Toplamkoltuk));
            }
        }

        [NotMapped]
        public bool YatayUygula
        {
            get => _yatayuygula;
            set
            {
                if (_yatayuygula != value)
                {
                    _yatayuygula = value;
                    OnPropertyChanged(nameof(YatayUygula));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "SalonAdi" && string.IsNullOrWhiteSpace(SalonAdi))
                {
                    return "Salon Adını Yazın.";
                }

                if (columnName == "Başlangıç" && Başlangıç <= 0)
                {
                    return "Başlangıç Değerini 0 dan Büyük Girin.";
                }

                if (columnName == "Başlangıç" && Başlangıç > En * Boy)
                {
                    return "Başlangıç Değerini Salonun Maksimum Koltuk Sayısından Küçük Girin.";
                }

                if (columnName == "Bitiş" && Bitiş <= 0)
                {
                    return "Bitiş Değerini 0 dan Büyük Girin.";
                }

                if (columnName == "Bitiş" && Bitiş > En * Boy)
                {
                    return "Bitiş Değerini Salonun Maksimum Koltuk Sayısından Küçük Girin.";
                }

                if (columnName == "Başlangıç" && Başlangıç > Bitiş)
                {
                    return "Başlangıç Bitişten Büyük Olamaz.";
                }

                if (columnName == "Bitiş" && Bitiş < Başlangıç)
                {
                    return "Bitiş Başlangıçtan Küçük Olamaz.";
                }

                return "";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public partial class Seanslar : INotifyPropertyChanged
    {
        private object _başlangıç;
        private object _bitiş;
        private string _TopluListe;

        [NotMapped]
        public object Başlangıç
        {
            get => _başlangıç;
            set
            {
                if (_başlangıç != value)
                {
                    _başlangıç = value;
                    OnPropertyChanged(nameof(Başlangıç));
                }
            }
        }

        [NotMapped]
        public object Bitiş
        {
            get => _bitiş;

            set
            {
                if (_bitiş != value)
                {
                    _bitiş = value;
                    OnPropertyChanged(nameof(Bitiş));
                }
            }
        }

        [NotMapped]
        public string TopluListe
        {
            get => _TopluListe;
            set
            {
                _TopluListe = value;
                OnPropertyChanged(nameof(TopluListe));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}