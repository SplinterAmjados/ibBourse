﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IBEntitiesConnection : DbContext
    {
        public IBEntitiesConnection()
            : base("name=IBEntitiesConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Valeur> valeurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrdreAchat> OrdreAchats { get; set; }
        public DbSet<OrdreVente> OrdreVentes { get; set; }
        public DbSet<ValeursClient> ValeursClients1 { get; set; }
    }
}
