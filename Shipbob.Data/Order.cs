//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shipbob.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderID { get; set; }
        public string TrackingID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public int Qty { get; set; }
        public string RecipFirstName { get; set; }
        public string RecipLastName { get; set; }
        public string RecipAddress { get; set; }
        public string RecipCity { get; set; }
        public string RecipState { get; set; }
        public string RecipCountry { get; set; }
        public string RecipZip { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
