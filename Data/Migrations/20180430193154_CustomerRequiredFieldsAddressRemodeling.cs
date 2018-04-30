using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class CustomerRequiredFieldsAddressRemodeling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_Complement",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_Neighborhood",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Persons",
                newName: "BirthDate");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<Guid>(nullable: false),
                    ZipCode = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Persons",
                newName: "DateOfBirth");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Complement",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Neighborhood",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Persons",
                nullable: true);
        }
    }
}
