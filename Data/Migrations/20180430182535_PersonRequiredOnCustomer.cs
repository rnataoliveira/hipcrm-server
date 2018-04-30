using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class PersonRequiredOnCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
