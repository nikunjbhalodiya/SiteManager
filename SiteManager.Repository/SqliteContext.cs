using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository
{
    public class SqliteContext : DbContext, IDisposable
    {
        public SqliteContext() : base("EfDbContext")
        {
            
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
              .HasRequired(s => s.Site)
              .WithMany(s => s.Customers)
              .HasForeignKey(s => s.SiteId).WillCascadeOnDelete(false);

            modelBuilder.Entity<SupervisorEntity>().
                HasRequired(c => c.Site).
                WithMany(s => s.Supervisors).
                HasForeignKey(s => s.SiteId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialEntity>().
                HasRequired(c => c.Site).
                WithMany(s => s.Materials).
                HasForeignKey(c => c.SiteId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialEntity>().
                HasRequired(x => x.Vendor).
                WithMany(s => s.Materials).
                HasForeignKey(c => c.VendorId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialEntity>().
                HasRequired(x => x.MaterialType).
                WithMany(s => s.Materials).
                HasForeignKey(c => c.MaterialTypeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialEntity>().
                HasRequired(x => x.Unit).
                WithMany(x => x.Materials).
                HasForeignKey(x => x.UnitId).WillCascadeOnDelete(false);

            modelBuilder.Entity<LabourEntity>().
                HasRequired(x => x.Site).
                WithMany(x => x.LaboursPayment).
                HasForeignKey(x => x.SiteId).WillCascadeOnDelete(false);

            modelBuilder.Entity<LabourEntity>().
                HasRequired(x => x.Contractor).
                WithMany(x => x.LaboursPayment).
                HasForeignKey(x => x.ContractorId).WillCascadeOnDelete(false);

            modelBuilder.Entity<LabourEntity>().
                HasRequired(x => x.WorkType).
                WithMany(x => x.Labours).
                HasForeignKey(x => x.WorkTypeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<DebitCreditEntity>()
                .HasRequired(x => x.EntityType)
                .WithMany(x => x.DebitCreditInfo)
                .HasForeignKey(x => x.EntityTypeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<DebitCreditEntity>()
                .HasRequired(x => x.Site)
                .WithMany(x => x.DebitCreditInfo)
                .HasForeignKey(x => x.SiteId).WillCascadeOnDelete(false);

            modelBuilder.Entity<DebitCreditEntity>()
                .HasRequired(x => x.PaymentMode)
                .WithMany(x => x.DebitCreditInfo)
                .HasForeignKey(x => x.PaymentModeId).WillCascadeOnDelete(false);
        }
    }
}
