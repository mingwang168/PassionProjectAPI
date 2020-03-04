using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemorizeWordsAPI.Migrations
{
    public partial class addScheduleIDNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordLists",
                columns: table => new
                {
                    WordListID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordListName = table.Column<string>(nullable: true),
                    WordNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordLists", x => x.WordListID);
                });

            migrationBuilder.CreateTable(
                name: "LearningSchedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordNumberPerDay = table.Column<int>(nullable: false),
                    NumberOfDay = table.Column<int>(nullable: false),
                    WordListID = table.Column<int>(nullable: false),
                    DaysHaveLearned = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningSchedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_LearningSchedules_WordLists_WordListID",
                        column: x => x.WordListID,
                        principalTable: "WordLists",
                        principalColumn: "WordListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnglishWord = table.Column<string>(nullable: true),
                    PhoneticSymbols = table.Column<string>(nullable: true),
                    ChineseMeaning = table.Column<string>(nullable: true),
                    times = table.Column<int>(nullable: false),
                    time1 = table.Column<bool>(nullable: false),
                    time2 = table.Column<bool>(nullable: false),
                    time3 = table.Column<bool>(nullable: false),
                    time4 = table.Column<bool>(nullable: false),
                    time5 = table.Column<bool>(nullable: false),
                    time6 = table.Column<bool>(nullable: false),
                    time7 = table.Column<bool>(nullable: false),
                    time8 = table.Column<bool>(nullable: false),
                    WordListID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordID);
                    table.ForeignKey(
                        name: "FK_Words_WordLists_WordListID",
                        column: x => x.WordListID,
                        principalTable: "WordLists",
                        principalColumn: "WordListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    taskID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    date = table.Column<DateTime>(nullable: false),
                    beginingTime = table.Column<DateTime>(nullable: false),
                    endingTime = table.Column<DateTime>(nullable: false),
                    newWordNumber = table.Column<int>(nullable: false),
                    reviewWordNumber = table.Column<int>(nullable: false),
                    ScheduleIDNavigationScheduleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.taskID);
                    table.ForeignKey(
                        name: "FK_Tasks_LearningSchedules_ScheduleIDNavigationScheduleID",
                        column: x => x.ScheduleIDNavigationScheduleID,
                        principalTable: "LearningSchedules",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "WordLists",
                columns: new[] { "WordListID", "WordListName", "WordNumber" },
                values: new object[] { 1, "A small word list for test", 100 });

            migrationBuilder.InsertData(
                table: "LearningSchedules",
                columns: new[] { "ScheduleID", "DaysHaveLearned", "NumberOfDay", "WordListID", "WordNumberPerDay" },
                values: new object[] { 1, 2, 5, 1, 20 });

            migrationBuilder.CreateIndex(
                name: "IX_LearningSchedules_WordListID",
                table: "LearningSchedules",
                column: "WordListID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ScheduleIDNavigationScheduleID",
                table: "Tasks",
                column: "ScheduleIDNavigationScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordListID",
                table: "Words",
                column: "WordListID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "LearningSchedules");

            migrationBuilder.DropTable(
                name: "WordLists");
        }
    }
}
