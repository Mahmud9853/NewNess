using Microsoft.EntityFrameworkCore.Migrations;

namespace NewNess.Migrations
{
    public partial class CreateContactImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsImages",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsImages",
                table: "Contacts");
        }
    }
}
