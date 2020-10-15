using Microsoft.EntityFrameworkCore.Migrations;

namespace Church.Web.Migrations
{
    public partial class modifyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_churches_districts_DistrictId",
                table: "churches");

            migrationBuilder.DropForeignKey(
                name: "FK_districts_campuses_CampusId",
                table: "districts");

            migrationBuilder.RenameColumn(
                name: "CampusId",
                table: "districts",
                newName: "CampusesId");

            migrationBuilder.RenameIndex(
                name: "IX_districts_CampusId",
                table: "districts",
                newName: "IX_districts_CampusesId");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "churches",
                newName: "DistrictId1");

            migrationBuilder.RenameIndex(
                name: "IX_churches_DistrictId",
                table: "churches",
                newName: "IX_churches_DistrictId1");

            migrationBuilder.AddForeignKey(
                name: "FK_churches_districts_DistrictId1",
                table: "churches",
                column: "DistrictId1",
                principalTable: "districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_districts_campuses_CampusesId",
                table: "districts",
                column: "CampusesId",
                principalTable: "campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_churches_districts_DistrictId1",
                table: "churches");

            migrationBuilder.DropForeignKey(
                name: "FK_districts_campuses_CampusesId",
                table: "districts");

            migrationBuilder.RenameColumn(
                name: "CampusesId",
                table: "districts",
                newName: "CampusId");

            migrationBuilder.RenameIndex(
                name: "IX_districts_CampusesId",
                table: "districts",
                newName: "IX_districts_CampusId");

            migrationBuilder.RenameColumn(
                name: "DistrictId1",
                table: "churches",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_churches_DistrictId1",
                table: "churches",
                newName: "IX_churches_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_churches_districts_DistrictId",
                table: "churches",
                column: "DistrictId",
                principalTable: "districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_districts_campuses_CampusId",
                table: "districts",
                column: "CampusId",
                principalTable: "campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
