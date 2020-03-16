using Microsoft.EntityFrameworkCore.Migrations;

namespace MemorizeWordsAPI.Migrations
{
    public partial class AddUserNametoWordlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "WordLists",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "WordLists",
                keyColumn: "WordListID",
                keyValue: 1,
                column: "UserName",
                value: "mingwang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "WordLists");
        }
    }
}
