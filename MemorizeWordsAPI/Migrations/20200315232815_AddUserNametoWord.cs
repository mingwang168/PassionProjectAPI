using Microsoft.EntityFrameworkCore.Migrations;

namespace MemorizeWordsAPI.Migrations
{
    public partial class AddUserNametoWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Words",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Words");
        }
    }
}
