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
    
    public partial class KoltukTipleri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KoltukTipleri()
        {
            this.Koltuklar = new ObservableCollection<Koltuklar>();
        }
    
        public int KoltukTipleriID { get; set; }
        public string KoltukTipi { get; set; }
        public Nullable<double> KoltukFiyati { get; set; }
        public Nullable<int> KoltukRenk { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Koltuklar> Koltuklar { get; set; }
    }
}
