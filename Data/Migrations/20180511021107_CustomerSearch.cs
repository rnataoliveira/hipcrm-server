using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class CustomerSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("ba6d4b15-fdee-4978-9b6f-a5591b27a9b3"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("faa1a244-b0aa-40c6-a584-ba5530264453"));

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "FirstName", "GeneralRegistration", "MaritalState", "Sex", "Surname" },
                values: new object[] { new Guid("6607f387-d41c-43cb-b474-82f0266898ef"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", "Renata", null, "Engaged", "Female", "Oliveira" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Notes", "PersonId" },
                values: new object[] { new Guid("24eab5f3-1db3-47dd-bb6a-6b624053459b"), "My First Customer!", new Guid("6607f387-d41c-43cb-b474-82f0266898ef") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("24eab5f3-1db3-47dd-bb6a-6b624053459b"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6607f387-d41c-43cb-b474-82f0266898ef"));

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Persons",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "GeneralRegistration", "MaritalState", "Name", "Sex", "Surname" },
                values: new object[] { new Guid("faa1a244-b0aa-40c6-a584-ba5530264453"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", null, "Engaged", "Renata", "Female", "Oliveira" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Notes", "PersonId" },
                values: new object[] { new Guid("ba6d4b15-fdee-4978-9b6f-a5591b27a9b3"), "My First Customer!", new Guid("faa1a244-b0aa-40c6-a584-ba5530264453") });
        }
    }
}
