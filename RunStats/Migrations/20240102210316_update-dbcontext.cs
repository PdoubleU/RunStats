using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class updatedbcontext : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_ExerciseType_ExerciseTypeId",
                table: "RunningSession",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_RunningSession_ExerciseType_ExerciseTypeId",
                table: "RunningSession");

            migrationBuilder.DropForeignKey(
                name: "FK_RunningSession_Weather_WeatherId",
                table: "RunningSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoesType_ShoesTypeId",
                table: "Shoes");

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
    }
}
