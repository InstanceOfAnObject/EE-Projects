﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyProject.Website.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EFMultipleResultSetEntities : DbContext
    {
        public EFMultipleResultSetEntities()
            : base("name=EFMultipleResultSetEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    
        public virtual ObjectResult<GetAllData_Result> GetAllData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllData_Result>("GetAllData");
        }
    }
}
