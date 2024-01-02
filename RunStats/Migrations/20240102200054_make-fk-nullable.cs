using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class makefknullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_ExerciseType_ExerciseTypeId",
                table: "RunningSession");

            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession");

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

            migrationBuilder.AlterColumn<int>(
                name: "WeatherId",
                table: "RunningSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseTypeId",
                table: "RunningSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_ExerciseType_ExerciseTypeId",
                table: "RunningSession",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes",
                column: "ShoesTypeId",
                principalTable: "ShoesType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_ExerciseType_ExerciseTypeId",
                table: "RunningSession");

            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession");

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

            migrationBuilder.AlterColumn<int>(
                name: "WeatherId",
                table: "RunningSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseTypeId",
                table: "RunningSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_ExerciseType_ExerciseTypeId",
                table: "RunningSession",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
