//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAP.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class sentiment_batch
    {
        public sentiment_batch()
        {
            this.sentiment_queue = new HashSet<sentiment_queue>();
        }
    
        public int id { get; set; }
        public int batch_limit { get; set; }
        public int batch_size { get; set; }
        public System.DateTime date_start { get; set; }
        public Nullable<System.DateTime> date_finish { get; set; }
    
        public virtual ICollection<sentiment_queue> sentiment_queue { get; set; }
    }
}
