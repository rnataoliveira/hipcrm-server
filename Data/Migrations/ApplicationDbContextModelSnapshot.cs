﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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

            modelBuilder.Entity("server.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<Guid>("PersonalDataId");

                    b.HasKey("Id");

                    b.HasIndex("PersonalDataId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("server.Models.PersonalData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PersonalData");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PersonalData");
                });

            modelBuilder.Entity("server.Models.SalePipeline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("SalesPipeline");
                });

            modelBuilder.Entity("server.Models.LegalPerson", b =>
                {
                    b.HasBaseType("server.Models.PersonalData");

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("CompanyRegistration")
                        .IsRequired();

                    b.Property<string>("StateRegistration");

                    b.ToTable("LegalPersonData");

                    b.HasDiscriminator().HasValue("LegalPerson");
                });

            modelBuilder.Entity("server.Models.PhysicalPerson", b =>
                {
                    b.HasBaseType("server.Models.PersonalData");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("DocumentNumber")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("GeneralRegistration");

                    b.Property<string>("MaritalState")
                        .IsRequired();

                    b.Property<string>("Sex")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.ToTable("PhysicalPersonData");

                    b.HasDiscriminator().HasValue("PhysicalPerson");
                });

            modelBuilder.Entity("server.Models.Customer", b =>
                {
                    b.HasOne("server.Models.PersonalData", "PersonalData")
                        .WithMany()
                        .HasForeignKey("PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("server.Models.PersonalData", b =>
                {
                    b.OwnsOne("server.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid?>("PersonalDataId");

                            b1.Property<string>("City");

                            b1.Property<string>("Complement");

                            b1.Property<string>("Neighborhood");

                            b1.Property<string>("Number");

                            b1.Property<string>("State");

                            b1.Property<string>("Street");

                            b1.Property<string>("ZipCode");

                            b1.ToTable("PersonalData");

                            b1.HasOne("server.Models.PersonalData")
                                .WithOne("Address")
                                .HasForeignKey("server.Models.Address", "PersonalDataId")
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

            modelBuilder.Entity("server.Models.PhysicalPerson", b =>
                {
                    b.OwnsOne("server.Models.PhoneNumber", "CellPhone", b1 =>
                        {
                            b1.Property<Guid>("PhysicalPersonId");

                            b1.Property<string>("AreaCode");

                            b1.Property<string>("Number");

                            b1.ToTable("PersonalData");

                            b1.HasOne("server.Models.PhysicalPerson")
                                .WithOne("CellPhone")
                                .HasForeignKey("server.Models.PhoneNumber", "PhysicalPersonId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("server.Models.PhoneNumber", "Phone", b1 =>
                        {
                            b1.Property<Guid>("PhysicalPersonId");

                            b1.Property<string>("AreaCode");

                            b1.Property<string>("Number");

                            b1.ToTable("PersonalData");

                            b1.HasOne("server.Models.PhysicalPerson")
                                .WithOne("Phone")
                                .HasForeignKey("server.Models.PhoneNumber", "PhysicalPersonId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
