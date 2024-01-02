using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class updatedbcontextvol2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes",
                column: "ShoesTypeId",
                principalTable: "ShoesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes",
                column: "ShoesTypeId",
                principalTable: "ShoesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
