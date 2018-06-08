using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class RemodelingAgreementData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Agreement_AgreementId",
                table: "Beneficiary");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "DentalCare_Plan",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "Modality",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_City",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_Complement",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_Neighborhood",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_Number",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_State",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_Street",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "MailingAddress_ZipCode",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "Phone_AreaCode",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "Phone_Number",
                table: "Agreement");

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Beneficiary",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Beneficiary",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDataId",
                table: "Agreement",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgreementData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Phone_AreaCode = table.Column<string>(nullable: true),
                    Phone_Number = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    MailingAddress_ZipCode = table.Column<string>(nullable: true),
                    MailingAddress_Street = table.Column<string>(nullable: true),
                    MailingAddress_Number = table.Column<string>(nullable: true),
                    MailingAddress_Complement = table.Column<string>(nullable: true),
                    MailingAddress_Neighborhood = table.Column<string>(nullable: true),
                    MailingAddress_City = table.Column<string>(nullable: true),
                    MailingAddress_State = table.Column<string>(nullable: true),
                    Modality = table.Column<int>(nullable: true),
                    DentalCare_Plan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_PersonalDataId",
                table: "Agreement",
                column: "PersonalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_AgreementData_PersonalDataId",
                table: "Agreement",
                column: "PersonalDataId",
                principalTable: "AgreementData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_AgreementData_AgreementId",
                table: "Beneficiary",
                column: "AgreementId",
                principalTable: "AgreementData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_AgreementData_PersonalDataId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_AgreementData_AgreementId",
                table: "Beneficiary");

            migrationBuilder.DropTable(
                name: "AgreementData");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_PersonalDataId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "Agreement");

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Beneficiary",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Beneficiary",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Agreement",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DentalCare_Plan",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Modality",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_City",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_Complement",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_Neighborhood",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_Number",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_State",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_Street",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress_ZipCode",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_AreaCode",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Agreement_AgreementId",
                table: "Beneficiary",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
