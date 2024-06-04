using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandwrittenTextRecognitionSystem.Migrations
{
    public partial class addPathColToAssign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Assignments");
        }
    }
}
