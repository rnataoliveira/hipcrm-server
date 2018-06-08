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

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}