using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }
        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<LegalPerson>().ToTable("LegalPersons");
            modelBuilder.Entity<PhysicalPerson>().ToTable("PhysicalPersons");

            modelBuilder.Entity<Person>().OwnsOne(p => p.Address);

            base.OnModelCreating(modelBuilder);
        }
    }
}