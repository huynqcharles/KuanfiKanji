using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuanfiKanjiWeb.Migrations
{
    public partial class Add_LearnedCount_To_Table_RelatedWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LearnedCount",
                table: "RelatedWords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LearnedCount",
                table: "RelatedWords");
        }
    }
}
