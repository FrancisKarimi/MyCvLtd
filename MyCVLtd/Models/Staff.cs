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
    
    public partial class Staff
    {
        public int Staff_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Position { get; set; }
        public Nullable<System.DateTime> Date_of_Birth { get; set; }
        public string ID_Number { get; set; }
        public string Residence { get; set; }
        public string Phone_Number { get; set; }
        public string Alternative_Phone_Number { get; set; }
        public string E_mail { get; set; }
        public Nullable<bool> super_Admin { get; set; }
        public Nullable<bool> Admin { get; set; }
        public Nullable<bool> Manager { get; set; }
        public Nullable<bool> Regular { get; set; }
        public Nullable<bool> Intern { get; set; }
    }
}