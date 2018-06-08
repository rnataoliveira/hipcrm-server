using Microsoft.EntityFrameworkCore;
using server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PersonalData> PersonalsData { get; set; }
        public DbSet<LegalPerson> LegalPersonsData { get; set; }
        public DbSet<PhysicalPerson> PhysicalsPersonsData { get; set; }
        public DbSet<SalePipeline> SalesPipelines { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<LegalPersonAgreement> LegalPersonAgreements { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<PersonalData>(builder =>
            {
                builder.OwnsOne(x => x.Address);
                builder.ToTable("PersonalData");
            });

            modelBuilder.Entity<PhysicalPerson>(builder =>
            {
                builder.OwnsOne(x => x.CellPhone);
                builder.OwnsOne(x => x.Phone);
            });

            modelBuilder.Entity<LegalPerson>(builder =>
            {
                builder.OwnsOne(x => x.Phone);
            });

            modelBuilder.Entity<SalePipeline>().ToTable("SalesPipeline");

            modelBuilder.Entity<Agreement>(builder =>
            {
                builder.OwnsOne(a => a.Payment);
                builder.HasOne(a => a.Sale);
                builder.HasOne(a => a.PersonalData);
                builder.ToTable("Agreement");
            });
            
            modelBuilder.Entity<AgreementData>().ToTable("AgreementData");

            modelBuilder.Entity<LegalPersonAgreement>(builder =>
            {
                builder.OwnsOne(l => l.MailingAddress);
                builder.OwnsOne(l => l.Phone);
                builder.OwnsOne(l => l.DentalCare);

                builder.HasMany(l => l.Beneficiaries)
                    .WithOne(b => b.Agreement);
            });

            modelBuilder.Entity<PhysicalPerson>(builder =>
            {
                builder.OwnsOne(x => x.CellPhone);
                builder.OwnsOne(x => x.Phone);
                builder.OwnsOne(x => x.Address);
            });

            modelBuilder.Entity<LegalPerson>(builder =>
            {
                builder.OwnsOne(x => x.Phone);
            });

            modelBuilder.Entity<LegalPerson>(builder =>
            {
                builder.OwnsOne(x => x.Phone);
                builder.OwnsOne(x => x.Address);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ChangeTracker.Entries()
              .Where(entry =>
                entry.Entity is PersonalData &&
                entry.State == EntityState.Added)
              .ToList()
              .ForEach(entry =>
              {
                  if (entry.Reference(nameof(PersonalData.Address)).CurrentValue == null)
                      entry.Reference(nameof(PersonalData.Address)).CurrentValue = new Address();
              });

            ChangeTracker.Entries()
              .Where(entry =>
                entry.Entity is LegalPerson &&
                entry.State == EntityState.Added)
              .ToList()
              .ForEach(entry =>
              {
                  if (entry.Reference(nameof(LegalPerson.Phone)).CurrentValue == null)
                      entry.Reference(nameof(LegalPerson.Phone)).CurrentValue = new PhoneNumber();
              });

            ChangeTracker.Entries()
             .Where(entry =>
               entry.Entity is PhysicalPerson &&
               entry.State == EntityState.Added)
             .ToList()
             .ForEach(entry =>
             {
                 if (entry.Reference(nameof(PhysicalPerson.Phone)).CurrentValue == null)
                     entry.Reference(nameof(PhysicalPerson.Phone)).CurrentValue = new PhoneNumber();

                 if (entry.Reference(nameof(PhysicalPerson.CellPhone)).CurrentValue == null)
                     entry.Reference(nameof(PhysicalPerson.CellPhone)).CurrentValue = new PhoneNumber();
             });

             ChangeTracker.Entries()
             .Where(entry =>
               entry.Entity is Agreement &&
               entry.State == EntityState.Added)
             .ToList()
             .ForEach(entry =>
             {
                 if (entry.Reference(nameof(Agreement.Payment)).CurrentValue == null)
                     entry.Reference(nameof(Agreement.Payment)).CurrentValue = new PaymentInfo();
             });

             ChangeTracker.Entries()
             .Where(entry =>
               entry.Entity is LegalPersonAgreement &&
               entry.State == EntityState.Added)
             .ToList()
             .ForEach(entry =>
             {
                 if (entry.Reference(nameof(LegalPersonAgreement.Phone)).CurrentValue == null)
                     entry.Reference(nameof(LegalPersonAgreement.Phone)).CurrentValue = new PhoneNumber();

                if (entry.Reference(nameof(LegalPersonAgreement.MailingAddress)).CurrentValue == null)
                     entry.Reference(nameof(LegalPersonAgreement.MailingAddress)).CurrentValue = new Address();

                if (entry.Reference(nameof(LegalPersonAgreement.DentalCare)).CurrentValue == null)
                     entry.Reference(nameof(LegalPersonAgreement.DentalCare)).CurrentValue = new DentalCare();
             });

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}