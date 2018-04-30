﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using server.Data;

namespace server.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("server.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Complement");

                    b.Property<string>("Neighborhood");

                    b.Property<string>("Number");

                    b.Property<Guid>("PersonId");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("server.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<Guid>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Customers");

                    b.HasData(
                        new { Id = new Guid("ba6d4b15-fdee-4978-9b6f-a5591b27a9b3"), Notes = "My First Customer!", PersonId = new Guid("faa1a244-b0aa-40c6-a584-ba5530264453") }
                    );
                });

            modelBuilder.Entity("server.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("server.Models.SalePipeline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("SalesPipelines");
                });

            modelBuilder.Entity("server.Models.LegalPerson", b =>
                {
                    b.HasBaseType("server.Models.Person");

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("CompanyRegistration")
                        .IsRequired();

                    b.Property<string>("StateRegistration");

                    b.ToTable("LegalPersons");

                    b.HasDiscriminator().HasValue("LegalPerson");
                });

            modelBuilder.Entity("server.Models.PhysicalPerson", b =>
                {
                    b.HasBaseType("server.Models.Person");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("DocumentNumber")
                        .IsRequired();

                    b.Property<string>("GeneralRegistration");

                    b.Property<string>("MaritalState")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Sex")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.ToTable("PhysicalPersons");

                    b.HasDiscriminator().HasValue("PhysicalPerson");

                    b.HasData(
                        new { Id = new Guid("faa1a244-b0aa-40c6-a584-ba5530264453"), BirthDate = new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), DocumentNumber = "01046387294", MaritalState = "Engaged", Name = "Renata", Sex = "Female", Surname = "Oliveira" }
                    );
                });

            modelBuilder.Entity("server.Models.Address", b =>
                {
                    b.HasOne("server.Models.Person", "Person")
                        .WithOne("Address")
                        .HasForeignKey("server.Models.Address", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("server.Models.Customer", b =>
                {
                    b.HasOne("server.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("server.Models.SalePipeline", b =>
                {
                    b.HasOne("server.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
