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
    
    public partial class Filmler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Filmler()
        {
            this.Seanslar = new ObservableCollection<Seanslar>();
        }
    
        public int FilmID { get; set; }
        public string FilmAdi { get; set; }
        public string FilmTipi { get; set; }
        public string Yonetmeni { get; set; }
        public string Oyuncular { get; set; }
        public Nullable<int> Sure { get; set; }
        public string Dili { get; set; }
        public string CekimTipi { get; set; }
        public string VideoYolu { get; set; }
        public string ResimYolu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Seanslar> Seanslar { get; set; }
    }
}
