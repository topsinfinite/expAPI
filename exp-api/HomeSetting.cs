//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace exp_api
{
    using System;
    using System.Collections.Generic;
    
    public partial class HomeSetting
    {
        public int ID { get; set; }
        public Nullable<int> EventID { get; set; }
        public string Title { get; set; }
        public string SmileyImage { get; set; }
        public Nullable<int> SmileyType { get; set; }
        public string Label { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> OrgID { get; set; }
    
        public virtual Event Event { get; set; }
    }
}
