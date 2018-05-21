using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class SeedFirstCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "FirstName", "GeneralRegistration", "MaritalState", "Sex", "Surname", "Address_City", "Address_Complement", "Address_Neighborhood", "Address_Number", "Address_State", "Address_Street", "Address_ZipCode", "CellPhone_AreaCode", "CellPhone_Number", "Phone_AreaCode", "Phone_Number" },
                values: new object[] { new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", "Renata", "", "Engaged", "F", "Oliveira", "San Junipero", "End of Street", "Junipero Coast", "99", "VR", "1st", "05037001", "11", "959463856", "11", "954546666" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Notes", "PersonalDataId" },
                values: new object[] { new Guid("a8c46259-ee81-4206-8ab8-134d64c01df8"), "My Fist Lady Customer!", new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("a8c46259-ee81-4206-8ab8-134d64c01df8"));

            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3"));
        }
    }
}
