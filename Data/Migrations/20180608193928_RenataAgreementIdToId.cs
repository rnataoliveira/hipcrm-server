using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Data.Migrations
{
    public partial class RenataAgreementIdToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgreementId",
                table: "Agreement",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agreement",
                newName: "AgreementId");
        }
    }
}
