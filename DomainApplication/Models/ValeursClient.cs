//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainApplication.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class ValeursClient
    {
        public string valeur { get; set; }
        public int id_client { get; set; }
        public string qte { get; set; }
        public string cod_valeur { get; set; }
    
        [JsonIgnore]
        public virtual Client client { get; set; }
    }
}
