using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class PhysicalPersonAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Plan",
                table: "AgreementData",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dependent",
                columns: table => new
                {
                    AgreementId = table.Column<Guid>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DocumentNumber = table.Column<string>(nullable: true),
                    GeneralRegistration = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    MothersName = table.Column<string>(nullable: true),
                    MaritalState = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependent_AgreementData_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "AgreementData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependent_AgreementId",
                table: "Dependent",
                column: "AgreementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependent");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "AgreementData");
        }
    }
}
