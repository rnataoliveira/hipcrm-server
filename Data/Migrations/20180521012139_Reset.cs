using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class Reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("4470c028-a1d0-479a-aa81-8feaf8f1638a"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("d6e4633c-7904-4b36-9e59-57d4661b787c"));

            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("b0a9e3f5-6651-473f-91eb-070d1994692c"));

            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("5aba30bb-62ca-4276-b84f-1dbaa1c472ae"));

            migrationBuilder.AddColumn<string>(
                name: "CellPhone_AreaCode",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CellPhone_Number",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_AreaCode",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                table: "PersonalData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellPhone_AreaCode",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "CellPhone_Number",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Phone_AreaCode",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Phone_Number",
                table: "PersonalData");

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "Discriminator", "CompanyName", "CompanyRegistration", "StateRegistration" },
                values: new object[] { new Guid("b0a9e3f5-6651-473f-91eb-070d1994692c"), "LegalPerson", "Corretora Lopes", "02.915.465/0001-06", null });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "FirstName", "GeneralRegistration", "MaritalState", "Sex", "Surname" },
                values: new object[] { new Guid("5aba30bb-62ca-4276-b84f-1dbaa1c472ae"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", "Renata", null, "Engaged", "Female", "Oliveira" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Notes", "PersonalDataId" },
                values: new object[] { new Guid("d6e4633c-7904-4b36-9e59-57d4661b787c"), "My Company Customer!", new Guid("b0a9e3f5-6651-473f-91eb-070d1994692c") });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Notes", "PersonalDataId" },
                values: new object[] { new Guid("4470c028-a1d0-479a-aa81-8feaf8f1638a"), "My First Customer!", new Guid("5aba30bb-62ca-4276-b84f-1dbaa1c472ae") });
        }
    }
}
