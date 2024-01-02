using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class updatedbcontextvol4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Shoes_ShoesId",
                table: "RunningSession");

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Shoes_ShoesId",
                table: "RunningSession",
                column: "ShoesId",
                principalTable: "Shoes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Shoes_ShoesId",
                table: "RunningSession");

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Shoes_ShoesId",
                table: "RunningSession",
                column: "ShoesId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
