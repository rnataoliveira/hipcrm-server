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

            var personalId = new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3");

            modelBuilder.Entity<PhysicalPerson>(builder =>
            {
                builder.HasData(new
                {
                    Id = personalId,
                    FirstName = "Renata",
                    Surname = "Oliveira",
                    DocumentNumber = "01046387294",
                    GeneralRegistration = "",
                    BirthDate = new DateTime(1994, 06, 23),
                    Sex = "F",
                    MaritalState = "Engaged",
                    Email = "renatatest@gmail.com"
                });
                builder.OwnsOne(x => x.CellPhone).HasData(
              new { PhysicalPersonId = personalId, AreaCode = "11", Number = "959463856" }
          );
                builder.OwnsOne(x => x.Phone).HasData(
              new { PhysicalPersonId = personalId, AreaCode = "11", Number = "954546666" }
          );
                builder.OwnsOne(x => x.Address).HasData(
              new
              {
                  PersonalDataId = personalId,
                  ZipCode = "05037001",
                  Street = "1st",
                  Number = "99",
                  Neighborhood = "Junipero Coast",
                  Complement = "End of Street",
                  City = "San Junipero",
                  State = "VR"
              }
          );
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.HasData(
                    new
                    {
                        Id = new Guid("a8c46259-ee81-4206-8ab8-134d64c01df8"),
                        Status = CustomerStatus.Prospect,
                        PersonalDataId = personalId,
                        Notes = "My Fist Lady Customer!"
                    }
                );
            });


            modelBuilder.Entity<LegalPerson>(builder =>
            {
                builder.OwnsOne(x => x.Phone);
            });

            var legalPersonId = new Guid("9b6e2f53-2a34-4128-97f5-8056545aed76");

            modelBuilder.Entity<LegalPerson>(builder =>
            {
                builder.HasData(new
                {
                    Id = legalPersonId,
                    CompanyName = "Lopes Corretora",
                    CompanyRegistration = "120.239.123/0001",
                    StateRegistration = "123456789-10",
                    Email = "lopes@hotmail.com"
                });

                builder.OwnsOne(x => x.Phone).HasData(
                      new { LegalPersonId = legalPersonId, AreaCode = "11", Number = "3535-2058" }
                  );
                builder.OwnsOne(x => x.Address).HasData(
              new
              {
                  PersonalDataId = legalPersonId,
                  ZipCode = "02089111",
                  Street = "2st",
                  Number = "300",
                  Neighborhood = "St Coast",
                  Complement = "White House",
                  City = "Jão Pietro",
                  State = "KL"
              }
          );
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.HasData(
                    new
                    {
                        Id = new Guid("9c9c0642-cd86-4cee-af0c-be3cd67750f4"),
                        Status = CustomerStatus.Prospect,
                        PersonalDataId = legalPersonId,
                        Notes = "Bitch!"
                    }
                );
            });


            // var personId = Guid.NewGuid();
            // var person2Id = Guid.NewGuid();
            // modelBuilder.Entity<PhysicalPerson>().HasData(new PhysicalPerson
            // {
            //     Id = personId,
            //     FirstName = "Renata",
            //     Surname = "Oliveira",
            //     DocumentNumber = "01046387294",
            //     BirthDate = new DateTime(1994, 06, 23),
            //     Sex = "Female",
            //     MaritalState = "Engaged"
            // });

            // modelBuilder.Entity<LegalPerson>().HasData(new LegalPerson{
            //     Id = person2Id,
            //     CompanyName = "Corretora Lopes",
            //     CompanyRegistration = "02.915.465/0001-06"
            // });

            // modelBuilder.Entity<Customer>().HasData(
            //     new
            //     {
            //         Id = Guid.NewGuid(),
            //         Notes = "My First Customer!",
            //         PersonalDataId = personId
            //     },
            //     new {
            //         Id = Guid.NewGuid(),
            //         Notes = "My Company Customer!",
            //         PersonalDataId = person2Id
            //     }
            // );

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