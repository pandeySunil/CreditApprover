﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreditApprover.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CreditApproverDbEntities1 : DbContext
    {
        public CreditApproverDbEntities1()
            : base("name=CreditApproverDbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public virtual DbSet<CreditRequest> CreditRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
