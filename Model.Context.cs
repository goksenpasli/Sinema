﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
            this.Database.Connection.ConnectionString += ";Password='1951753939'";
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Filmler> Filmler { get; set; }
        public virtual DbSet<Koltuklar> Koltuklar { get; set; }
        public virtual DbSet<KoltukTipleri> KoltukTipleri { get; set; }
        public virtual DbSet<Musteriler> Musteriler { get; set; }
        public virtual DbSet<Salonlar> Salonlar { get; set; }
        public virtual DbSet<Seanslar> Seanslar { get; set; }
        public virtual DbSet<Tahsilatlar> Tahsilatlar { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilar { get; set; }
        public virtual DbSet<Siparisler> Siparisler { get; set; }
        public virtual DbSet<Urunler> Urunler { get; set; }
    }
}
