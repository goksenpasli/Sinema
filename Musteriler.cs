//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sinema
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Musteriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Musteriler()
        {
            this.Tahsilatlar = new ObservableCollection<Tahsilatlar>();
            this.Siparisler = new ObservableCollection<Siparisler>();
        }
    
        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public Nullable<int> MusteriYasi { get; set; }
        public Nullable<int> SeansID { get; set; }
        public Nullable<int> KoltukID { get; set; }
        public string KoltukGrup { get; set; }
        public Nullable<int> KullaniciID { get; set; }
        public byte[] Resim { get; set; }
    
        public virtual Koltuklar Koltuklar { get; set; }
        public virtual Seanslar Seanslar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Tahsilatlar> Tahsilatlar { get; set; }
        public virtual Kullanicilar Kullanicilar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Siparisler> Siparisler { get; set; }
    }
}