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
    
    public partial class Notification
    {
        public int Notification_ID { get; set; }
        public int Sender { get; set; }
        public int Recipient { get; set; }
        public string Notification_Body { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Member Member1 { get; set; }
    }
}
