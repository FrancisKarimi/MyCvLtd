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
    
    public partial class Member
    {
        public Member()
        {
            this.EducationalBackgrounds = new HashSet<EducationalBackground>();
            this.hobbies = new HashSet<hobby>();
            this.Member_Company = new HashSet<Member_Company>();
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Notifications = new HashSet<Notification>();
            this.Notifications1 = new HashSet<Notification>();
            this.Transactions = new HashSet<Transaction>();
            this.WorkExperiences = new HashSet<WorkExperience>();
        }
    
        public int Member_No { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string ID_Number { get; set; }
        public string Phone_Number { get; set; }
        public string E_Mail { get; set; }
        public System.DateTime Date_of_Birth { get; set; }
        public string Member_Type { get; set; }
        public string Physical_Location { get; set; }
        public string profession { get; set; }
        public string password { get; set; }
        public string passwordCon { get; set; }
        public bool verified { get; set; }
        public string Activation_Code { get; set; }
    
        public virtual ICollection<EducationalBackground> EducationalBackgrounds { get; set; }
        public virtual ICollection<hobby> hobbies { get; set; }
        public virtual ICollection<Member_Company> Member_Company { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Notification> Notifications1 { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}
