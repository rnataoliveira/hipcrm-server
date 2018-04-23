using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
    
        public DbSet<Client> Clients { get; set; }

        public DbSet<PhysicalPerson> PhysicalPerson { get; set; }

        public DbSet<LegalPerson> LegalPerson { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PhysicalPerson>().ToTable("PhysicalPerson");
            modelBuilder.Entity<LegalPerson>().ToTable("LegalPerson");
            modelBuilder.Entity<Client>().ToTable("Clients");
        }
    }
}
