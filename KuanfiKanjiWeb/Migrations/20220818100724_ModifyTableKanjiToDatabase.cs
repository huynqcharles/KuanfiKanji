using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuanfiKanjiWeb.Migrations
{
    public partial class ModifyTableKanjiToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chi_Vie_Meaning",
                table: "Kanji",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chi_Vie_Meaning",
                table: "Kanji");
        }
    }
}
