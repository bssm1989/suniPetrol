//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Enroll_Emp_Pos = new HashSet<Enroll_Emp_Pos>();
        }
    
        public string EmpTitle { get; set; }
        public string EmpName { get; set; }
        public string EmpLname { get; set; }
        public Nullable<System.DateTime> EmpBirthday { get; set; }
        public Nullable<int> EmpIdcard { get; set; }
        public Nullable<int> EmpPsis_id { get; set; }
        public Nullable<int> EmpStatus { get; set; }
        public Nullable<System.DateTime> EmpTimestamp { get; set; }
        public string EmpTelephone { get; set; }
        public string Empbank_id { get; set; }
        public int EmpId { get; set; }
        public Nullable<int> EmpGender { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enroll_Emp_Pos> Enroll_Emp_Pos { get; set; }
    }
}
