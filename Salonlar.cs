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
    
    public partial class Salonlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salonlar()
        {
            this.Koltuklar = new ObservableCollection<Koltuklar>();
            this.Seanslar = new ObservableCollection<Seanslar>();
        }
    
        public int SalonID { get; set; }
        public string SalonAdi { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public Nullable<int> En { get; set; }
        public Nullable<int> Boy { get; set; }
        public Nullable<bool> KoltukDetay { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Koltuklar> Koltuklar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Seanslar> Seanslar { get; set; }
    }
}
