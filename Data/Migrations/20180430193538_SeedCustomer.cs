using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class SeedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "GeneralRegistration", "MaritalState", "Name", "Sex", "Surname" },
                values: new object[] { new Guid("faa1a244-b0aa-40c6-a584-ba5530264453"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", null, "Engaged", "Renata", "Female", "Oliveira" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Notes", "PersonId" },
                values: new object[] { new Guid("ba6d4b15-fdee-4978-9b6f-a5591b27a9b3"), "My First Customer!", new Guid("faa1a244-b0aa-40c6-a584-ba5530264453") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("ba6d4b15-fdee-4978-9b6f-a5591b27a9b3"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("faa1a244-b0aa-40c6-a584-ba5530264453"));
        }
    }
}
