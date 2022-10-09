using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strainth.DataService.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSplits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSplits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    ProgramSplitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDetails_ProgramSplits_ProgramSplitId",
                        column: x => x.ProgramSplitId,
                        principalTable: "ProgramSplits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramExcercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepsRangeLower = table.Column<int>(type: "int", nullable: false),
                    RepsRangeUpper = table.Column<int>(type: "int", nullable: false),
                    SetsRangeLower = table.Column<int>(type: "int", nullable: false),
                    SetsRangeUpper = table.Column<int>(type: "int", nullable: false),
                    RepsThreshold = table.Column<int>(type: "int", nullable: false),
                    WeightIncrement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ProgramDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramExcercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramExcercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramExcercises_ProgramDetails_ProgramDetailId",
                        column: x => x.ProgramDetailId,
                        principalTable: "ProgramDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Abs" },
                    { 2, "Calves" },
                    { 3, "Curl" },
                    { 4, "Extend" },
                    { 5, "Hinge" },
                    { 6, "Press" },
                    { 7, "Pull" },
                    { 8, "Push" },
                    { 9, "Row" },
                    { 10, "Shoulders" },
                    { 11, "Squat" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Ab Wheel" },
                    { 2, 1, "Bicycle Crunch" },
                    { 3, 2, "Seated Barbell Calf Raise" },
                    { 4, 2, "Standing Calf Raise" },
                    { 5, 3, "Incline Dumbbell Curl" },
                    { 6, 3, "Dumbbell Spider Curl" },
                    { 7, 4, "Rope Pushdown" },
                    { 8, 4, "Standing OH Cable Extension" },
                    { 9, 5, "Deadlift - Sumo" },
                    { 10, 5, "Back Extension" },
                    { 11, 5, "Floor Hamstring Curl" },
                    { 12, 5, "Pull Through" },
                    { 13, 6, "Seated OH Press" },
                    { 14, 6, "Arnold Press" },
                    { 15, 7, "Chinup" },
                    { 16, 7, "Neutral Grip Pulldown" },
                    { 17, 7, "Underhand Cable Pullover" },
                    { 18, 8, "Incline Bench Press" },
                    { 19, 8, "Incline Dumbbell Press Fly" },
                    { 20, 8, "Close Grip Bench Press" },
                    { 21, 9, "Chest-supported Dumbbell Row" },
                    { 22, 9, "Cable Upright Row" },
                    { 23, 10, "Lateral Raise" },
                    { 24, 10, "Prone Rear Delt Raise" },
                    { 25, 10, "Skiers" },
                    { 26, 11, "Slantboard Front Squat" },
                    { 27, 11, "ATG Split Squat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetails_ProgramSplitId",
                table: "ProgramDetails",
                column: "ProgramSplitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramExcercises_ExerciseId",
                table: "ProgramExcercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramExcercises_ProgramDetailId",
                table: "ProgramExcercises",
                column: "ProgramDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramExcercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "ProgramDetails");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProgramSplits");
        }
    }
}
