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
    
    public partial class Kullanicilar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanicilar()
        {
            this.Musteriler = new ObservableCollection<Musteriler>();
        }
    
        public string KullaniciAdi { get; set; }
        public string KullaniciSifre { get; set; }
        public Nullable<int> KullaniciYetkisi { get; set; }
        public Nullable<bool> SifreEtkinMi { get; set; }
        public int KullaniciID { get; set; }
        public string KullaniciTc { get; set; }
        public byte[] Resim { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Musteriler> Musteriler { get; set; }
    }
}
