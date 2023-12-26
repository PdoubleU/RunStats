using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class fixupdatedatabaseerror : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession");

            migrationBuilder.DropColumn(
                name: "WheatherId",
                table: "RunningSession");

            migrationBuilder.AlterColumn<int>(
                name: "WeatherId",
                table: "RunningSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession");

            migrationBuilder.AlterColumn<int>(
                name: "WeatherId",
                table: "RunningSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WheatherId",
                table: "RunningSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id");
        }
    }
}
