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
    [Migration("20180430182535_PersonRequiredOnCustomer")]
    partial class PersonRequiredOnCustomer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("server.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<Guid>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Customers");
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

                    b.Property<string>("CompanyName");

                    b.Property<string>("CompanyRegistration");

                    b.Property<string>("StateRegistration");

                    b.ToTable("LegalPersons");

                    b.HasDiscriminator().HasValue("LegalPerson");
                });

            modelBuilder.Entity("server.Models.PhysicalPerson", b =>
                {
                    b.HasBaseType("server.Models.Person");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("DocumentNumber");

                    b.Property<string>("GeneralRegistration");

                    b.Property<string>("MaritalState");

                    b.Property<string>("Name");

                    b.Property<string>("Sex");

                    b.Property<string>("Surname");

                    b.ToTable("PhysicalPersons");

                    b.HasDiscriminator().HasValue("PhysicalPerson");
                });

            modelBuilder.Entity("server.Models.Customer", b =>
                {
                    b.HasOne("server.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("server.Models.Person", b =>
                {
                    b.OwnsOne("server.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid?>("PersonId");

                            b1.Property<string>("City");

                            b1.Property<string>("Complement");

                            b1.Property<string>("Neighborhood");

                            b1.Property<string>("Number");

                            b1.Property<string>("State");

                            b1.Property<string>("Street");

                            b1.Property<string>("ZipCode");

                            b1.ToTable("Persons");

                            b1.HasOne("server.Models.Person")
                                .WithOne("Address")
                                .HasForeignKey("server.Models.Address", "PersonId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
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