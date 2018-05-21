using Microsoft.EntityFrameworkCore;
using server.Models;
using System;

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

            modelBuilder.Entity<PersonalData>(builder => {
                builder.OwnsOne(x => x.Address);
                builder.ToTable("PersonalData");
            });

            modelBuilder.Entity<LegalPerson>();
            modelBuilder.Entity<PhysicalPerson>(builder => {
                builder.OwnsOne(x => x.CellPhone);
                builder.OwnsOne(x => x.Phone);
            });
            
            modelBuilder.Entity<SalePipeline>().ToTable("SalesPipeline");

            var personalId = new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3");
            modelBuilder.Entity<PhysicalPerson>(builder => {
                builder.HasData(new {
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
                    new { 
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
            modelBuilder.Entity<Customer>(builder => {
                builder.HasData(
                    new { Id = new Guid("a8c46259-ee81-4206-8ab8-134d64c01df8"), PersonalDataId = personalId, Notes = "My Fist Lady Customer!" }
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
    }
}