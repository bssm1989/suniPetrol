﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace santisart_app.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class suniPetrolEntities1 : DbContext
    {
        public suniPetrolEntities1()
            : base("name=suniPetrolEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<company> companies { get; set; }
        public virtual DbSet<Enroll_reportTruckKpi> Enroll_reportTruckKpi { get; set; }
        public virtual DbSet<Enroll_truck> Enroll_truck { get; set; }
        public virtual DbSet<payPetrol> payPetrols { get; set; }
        public virtual DbSet<truck> trucks { get; set; }
    }
}
