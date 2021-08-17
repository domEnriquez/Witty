using Microsoft.EntityFrameworkCore.Migrations;

namespace Witty.Migrations
{
    public partial class AddedWittyEntryNavigationInResponseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Response_WittyEntries_WittyEntryId",
                table: "Response");

            migrationBuilder.AlterColumn<int>(
                name: "WittyEntryId",
                table: "Response",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Response_WittyEntries_WittyEntryId",
                table: "Response",
                column: "WittyEntryId",
                principalTable: "WittyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Response_WittyEntries_WittyEntryId",
                table: "Response");

            migrationBuilder.AlterColumn<int>(
                name: "WittyEntryId",
                table: "Response",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Response_WittyEntries_WittyEntryId",
                table: "Response",
                column: "WittyEntryId",
                principalTable: "WittyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
