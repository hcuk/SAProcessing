//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace SAP.DataModel
{
  public class sentiment_queue_error
  {
    public int id { get; set; }

    public int sentiment_queue_id { get; set; }

    public string message { get; set; }

    public string stacktrace { get; set; }

    public DateTime date_created { get; set; }

    public virtual sentiment_queue sentiment_queue { get; set; }
  }
}
