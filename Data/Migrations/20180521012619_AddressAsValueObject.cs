using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class AddressAsValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Complement",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Neighborhood",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "PersonalData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Address_Complement",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Address_Neighborhood",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "PersonalData");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_PersonalData_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId",
                unique: true);
        }
    }
}
