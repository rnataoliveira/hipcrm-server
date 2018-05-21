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
            
            // modelBuilder.Entity<PersonalData>().ToTable("PersonalData")
            //     .HasOne(p => p.Address)
            //     .WithOne(p => p.Person)
            //     .HasForeignKey<Address>(a => a.PersonId);

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