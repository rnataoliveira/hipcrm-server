using Microsoft.EntityFrameworkCore;
using server.Models;
using System;

namespace server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }
        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }
        public DbSet<SalePipeline> SalesPipelines { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            
            modelBuilder.Entity<Person>().ToTable("Persons")
                .HasOne(p => p.Address)
                .WithOne(p => p.Person)
                .HasForeignKey<Address>(a => a.PersonId);

            modelBuilder.Entity<LegalPerson>().ToTable("LegalPersons");
            modelBuilder.Entity<PhysicalPerson>().ToTable("PhysicalPersons");
            modelBuilder.Entity<SalePipeline>().ToTable("SalesPipelines");

            var personId = Guid.NewGuid();
            modelBuilder.Entity<PhysicalPerson>().HasData(new PhysicalPerson
            {
                Id = personId,
                Name = "Renata",
                Surname = "Oliveira",
                DocumentNumber = "01046387294",
                BirthDate = new DateTime(1994, 06, 23),
                Sex = "Female",
                MaritalState = "Engaged"
            });

            modelBuilder.Entity<Customer>().HasData(
                new
                {
                    Id = Guid.NewGuid(),
                    Notes = "My First Customer!",
                    PersonId = personId
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}