using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunStats.Migrations
{
    public partial class revertallowdisabledfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitDisabledControls",
                table: "RunningSession");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SubmitDisabledControls",
                table: "RunningSession",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
