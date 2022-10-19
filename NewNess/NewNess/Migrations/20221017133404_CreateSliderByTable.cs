using Microsoft.EntityFrameworkCore.Migrations;

namespace NewNess.Migrations
{
    public partial class CreateSliderByTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "By",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "By",
                table: "Sliders");
        }
    }
}
