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
    
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            this.Siparisler = new ObservableCollection<Siparisler>();
        }
    
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<double> BirimFiyati { get; set; }
        public Nullable<bool> UrunSatilabilirmi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Siparisler> Siparisler { get; set; }
    }
}