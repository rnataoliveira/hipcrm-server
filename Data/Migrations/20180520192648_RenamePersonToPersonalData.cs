using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class RenamePersonToPersonalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Persons_PersonId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPipelines_Customers_CustomerId",
                table: "SalesPipelines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesPipelines",
                table: "SalesPipelines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

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

            migrationBuilder.RenameTable(
                name: "SalesPipelines",
                newName: "SalesPipeline");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "PersonalData");

            migrationBuilder.RenameIndex(
                name: "IX_SalesPipelines_CustomerId",
                table: "SalesPipeline",
                newName: "IX_SalesPipeline_CustomerId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Customer",
                newName: "PersonalDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_PersonId",
                table: "Customer",
                newName: "IX_Customer_PersonalDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesPipeline",
                table: "SalesPipeline",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalData",
                table: "PersonalData",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Address_PersonalData_PersonId",
                table: "Address",
                column: "PersonId",
                principalTable: "PersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_PersonalData_PersonalDataId",
                table: "Customer",
                column: "PersonalDataId",
                principalTable: "PersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPipeline_Customer_CustomerId",
                table: "SalesPipeline",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_PersonalData_PersonId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_PersonalData_PersonalDataId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPipeline_Customer_CustomerId",
                table: "SalesPipeline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesPipeline",
                table: "SalesPipeline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalData",
                table: "PersonalData");

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

            migrationBuilder.RenameTable(
                name: "SalesPipeline",
                newName: "SalesPipelines");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "PersonalData",
                newName: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_SalesPipeline_CustomerId",
                table: "SalesPipelines",
                newName: "IX_SalesPipelines_CustomerId");

            migrationBuilder.RenameColumn(
                name: "PersonalDataId",
                table: "Customers",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_PersonalDataId",
                table: "Customers",
                newName: "IX_Customers_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesPipelines",
                table: "SalesPipelines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Persons_PersonId",
                table: "Address",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPipelines_Customers_CustomerId",
                table: "SalesPipelines",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
