using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class AggreementLegalPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agreement",
                columns: table => new
                {
                    AgreementId = table.Column<Guid>(nullable: false),
                    SaleId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Payment_EntranceFee = table.Column<decimal>(nullable: false),
                    Payment_TotalValue = table.Column<decimal>(nullable: false),
                    Payment_InstallmentsCount = table.Column<int>(nullable: false),
                    Payment_Comission = table.Column<decimal>(nullable: false),
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
                    table.PrimaryKey("PK_Agreement", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK_Agreement_SalesPipeline_SaleId",
                        column: x => x.SaleId,
                        principalTable: "SalesPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiary",
                columns: table => new
                {
                    AgreementId = table.Column<Guid>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    Plan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiary_Agreement_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreement",
                        principalColumn: "AgreementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_SaleId",
                table: "Agreement",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_AgreementId",
                table: "Beneficiary",
                column: "AgreementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiary");

            migrationBuilder.DropTable(
                name: "Agreement");
        }
    }
}
