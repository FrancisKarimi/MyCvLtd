//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCVLtd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Job
    {
        public int Job_ID { get; set; }
        public string Title { get; set; }
        public System.DateTime Date_Posted { get; set; }
        public Nullable<System.DateTime> Time_Posted { get; set; }
        public string Description { get; set; }
        public int company_ID { get; set; }
        public string Comments { get; set; }
        public Nullable<int> Likes { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
