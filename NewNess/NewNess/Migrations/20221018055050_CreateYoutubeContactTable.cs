using Microsoft.EntityFrameworkCore.Migrations;

namespace NewNess.Migrations
{
    public partial class CreateYoutubeContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YoutubeLink",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubemName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeLink",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "YoutubemName",
                table: "Contacts");
        }
    }
}
