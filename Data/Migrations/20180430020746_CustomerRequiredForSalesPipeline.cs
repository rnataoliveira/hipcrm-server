using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace server.Data.Migrations
{
    public partial class CustomerRequiredForSalesPipeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPipelines_Customers_CustomerId",
                table: "SalesPipelines");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "SalesPipelines",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPipelines_Customers_CustomerId",
                table: "SalesPipelines",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPipelines_Customers_CustomerId",
                table: "SalesPipelines");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "SalesPipelines",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPipelines_Customers_CustomerId",
                table: "SalesPipelines",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
