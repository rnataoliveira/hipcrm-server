using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class AddCompanyCustomerToSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("24eab5f3-1db3-47dd-bb6a-6b624053459b"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6607f387-d41c-43cb-b474-82f0266898ef"));

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "CompanyName", "CompanyRegistration", "StateRegistration" },
                values: new object[] { new Guid("e0a4f7af-cbe5-44b3-a51c-f422d45d8806"), "LegalPerson", "Corretora Lopes", "02.915.465/0001-06", null });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "FirstName", "GeneralRegistration", "MaritalState", "Sex", "Surname" },
                values: new object[] { new Guid("627a66ef-3275-40a6-92ac-6d84e620cf1c"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", "Renata", null, "Engaged", "Female", "Oliveira" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Notes", "PersonId" },
                values: new object[] { new Guid("bf9740ca-b75d-4221-bbd0-c98f41713ff2"), "My Company Customer!", new Guid("e0a4f7af-cbe5-44b3-a51c-f422d45d8806") });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Notes", "PersonId" },
                values: new object[] { new Guid("0538ecb4-5808-44b7-ae13-cb6607328484"), "My First Customer!", new Guid("627a66ef-3275-40a6-92ac-6d84e620cf1c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("0538ecb4-5808-44b7-ae13-cb6607328484"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("bf9740ca-b75d-4221-bbd0-c98f41713ff2"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("e0a4f7af-cbe5-44b3-a51c-f422d45d8806"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("627a66ef-3275-40a6-92ac-6d84e620cf1c"));

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "FirstName", "GeneralRegistration", "MaritalState", "Sex", "Surname" },
                values: new object[] { new Guid("6607f387-d41c-43cb-b474-82f0266898ef"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", "Renata", null, "Engaged", "Female", "Oliveira" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Notes", "PersonId" },
                values: new object[] { new Guid("24eab5f3-1db3-47dd-bb6a-6b624053459b"), "My First Customer!", new Guid("6607f387-d41c-43cb-b474-82f0266898ef") });
        }
    }
}
