﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testLogin.Models
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
    
        public virtual DbSet<Enroll_paid> Enroll_paid { get; set; }
        public virtual DbSet<Enroll_student_class> Enroll_student_class { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Monthly> Monthly { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Enroll_pay> Enroll_pay { get; set; }
        public virtual DbSet<attendance_day> attendance_day { get; set; }
        public virtual DbSet<Enroll_student_all_event> Enroll_student_all_event { get; set; }
        public virtual DbSet<student2561> student2561 { get; set; }
        public virtual DbSet<attendance> attendance { get; set; }
        public virtual DbSet<C_enrollattstudent> C_enrollattstudent { get; set; }
        public virtual DbSet<student2561_copy1> student2561_copy1 { get; set; }
        public virtual DbSet<EnrollStudentAttdance> EnrollStudentAttdance { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<student2561_food> student2561_food { get; set; }
        public virtual DbSet<ContestEducationSchool> ContestEducationSchool { get; set; }
        public virtual DbSet<Enroll_EduContest> Enroll_EduContest { get; set; }
        public virtual DbSet<studentEduContest2561> studentEduContest2561 { get; set; }
        public virtual DbSet<ViewEmpPos> ViewEmpPos { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Enroll_Emp_Pos> Enroll_Emp_Pos { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<ClassInSchool> ClassInSchool { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EnrollCouse> EnrollCouse { get; set; }
        public virtual DbSet<YearEdu> YearEdu { get; set; }
        public virtual DbSet<EnrollStudentCouse> EnrollStudentCouse { get; set; }
        public virtual DbSet<View_Couse> View_Couse { get; set; }
        public virtual DbSet<EnrollFinishStudent> EnrollFinishStudent { get; set; }
        public virtual DbSet<FinishType> FinishType { get; set; }
        public virtual DbSet<EnrollFamilyStudent> EnrollFamilyStudent { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
    }
}
