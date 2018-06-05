using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address_ZipCode = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_Number = table.Column<string>(nullable: true),
                    Address_Complement = table.Column<string>(nullable: true),
                    Address_Neighborhood = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyRegistration = table.Column<string>(nullable: true),
                    StateRegistration = table.Column<string>(nullable: true),
                    Phone_AreaCode = table.Column<string>(nullable: true),
                    Phone_Number = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DocumentNumber = table.Column<string>(nullable: true),
                    GeneralRegistration = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalState = table.Column<string>(nullable: true),
                    PhoneNumber_Phone_AreaCode = table.Column<string>(nullable: true),
                    PhoneNumber_Phone_Number = table.Column<string>(nullable: true),
                    CellPhone_AreaCode = table.Column<string>(nullable: true),
                    CellPhone_Number = table.Column<string>(nullable: true),
                    PhysicalPerson_Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PersonalDataId = table.Column<Guid>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesPipeline",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CalendarId = table.Column<string>(nullable: true),
                    FolderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPipeline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesPipeline_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "Discriminator", "BirthDate", "DocumentNumber", "PhysicalPerson_Email", "FirstName", "GeneralRegistration", "MaritalState", "Sex", "Surname", "Address_City", "Address_Complement", "Address_Neighborhood", "Address_Number", "Address_State", "Address_Street", "Address_ZipCode", "CellPhone_AreaCode", "CellPhone_Number", "PhoneNumber_Phone_AreaCode", "PhoneNumber_Phone_Number" },
                values: new object[] { new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3"), "PhysicalPerson", new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "01046387294", "renatatest@gmail.com", "Renata", "", "Engaged", "F", "Oliveira", "San Junipero", "End of Street", "Junipero Coast", "99", "VR", "1st", "05037001", "11", "959463856", "11", "954546666" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "Discriminator", "CompanyName", "CompanyRegistration", "Email", "StateRegistration", "Address_City", "Address_Complement", "Address_Neighborhood", "Address_Number", "Address_State", "Address_Street", "Address_ZipCode", "Phone_AreaCode", "Phone_Number" },
                values: new object[] { new Guid("9b6e2f53-2a34-4128-97f5-8056545aed76"), "LegalPerson", "Lopes Corretora", "120.239.123/0001", "lopes@hotmail.com", "123456789-10", "Jão Pietro", "White House", "St Coast", "300", "KL", "2st", "02089111", "11", "3535-2058" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Notes", "PersonalDataId", "Status" },
                values: new object[] { new Guid("a8c46259-ee81-4206-8ab8-134d64c01df8"), "My Fist Lady Customer!", new Guid("cd9fbd0d-aecd-4a8e-b924-37be674709e3"), 0 });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Notes", "PersonalDataId", "Status" },
                values: new object[] { new Guid("9c9c0642-cd86-4cee-af0c-be3cd67750f4"), "Bitch!", new Guid("9b6e2f53-2a34-4128-97f5-8056545aed76"), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PersonalDataId",
                table: "Customer",
                column: "PersonalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPipeline_CustomerId",
                table: "SalesPipeline",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesPipeline");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "PersonalData");
        }
    }
}
