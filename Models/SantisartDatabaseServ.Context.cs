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
    
    public partial class santisar_Entities : DbContext
    {
        public santisar_Entities()
            : base("name=santisar_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Enroll_paid> Enroll_paid { get; set; }
        public virtual DbSet<Enroll_student_class> Enroll_student_class { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Monthly> Monthly { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Enroll_pay> Enroll_pay { get; set; }
        public virtual DbSet<attendance> attendance { get; set; }
    }
}
