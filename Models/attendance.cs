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
    
    public partial class attendance
    {
        public int att_id { get; set; }
        public Nullable<int> Student_id { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
        public Nullable<int> Staff_id { get; set; }
        public Nullable<int> qira_id { get; set; }
        public Nullable<int> page { get; set; }
    }
}
