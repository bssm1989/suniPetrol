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
    
    public partial class TestCourseInstructor
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
    
        public virtual TestCourse TestCourse { get; set; }
        public virtual TestInstructor TestInstructor { get; set; }
    }
}
