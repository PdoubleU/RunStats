using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class allownullablesforFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes");

            migrationBuilder.AlterColumn<int>(
                name: "ShoesTypeId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes",
                column: "ShoesTypeId",
                principalTable: "ShoesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes");

            migrationBuilder.AlterColumn<int>(
                name: "ShoesTypeId",
                table: "Shoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes",
                column: "ShoesTypeId",
                principalTable: "ShoesType",
                principalColumn: "Id");
        }
    }
}
