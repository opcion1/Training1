using Microsoft.EntityFrameworkCore.Migrations;

namespace Training1.Migrations
{
    public partial class DbSetInProductContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayOfSesshin_Sesshin_SesshinId",
                table: "DayOfSesshin");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_DayOfSesshin_DayOfSesshinId",
                table: "Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayOfSesshin",
                table: "DayOfSesshin");

            migrationBuilder.RenameTable(
                name: "DayOfSesshin",
                newName: "DaysOfSesshin");

            migrationBuilder.RenameIndex(
                name: "IX_DayOfSesshin_SesshinId",
                table: "DaysOfSesshin",
                newName: "IX_DaysOfSesshin_SesshinId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaysOfSesshin",
                table: "DaysOfSesshin",
                column: "DayOfSesshinId");

            migrationBuilder.AddForeignKey(
                name: "FK_DaysOfSesshin_Sesshin_SesshinId",
                table: "DaysOfSesshin",
                column: "SesshinId",
                principalTable: "Sesshin",
                principalColumn: "SesshinId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_DaysOfSesshin_DayOfSesshinId",
                table: "Meal",
                column: "DayOfSesshinId",
                principalTable: "DaysOfSesshin",
                principalColumn: "DayOfSesshinId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysOfSesshin_Sesshin_SesshinId",
                table: "DaysOfSesshin");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_DaysOfSesshin_DayOfSesshinId",
                table: "Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaysOfSesshin",
                table: "DaysOfSesshin");

            migrationBuilder.RenameTable(
                name: "DaysOfSesshin",
                newName: "DayOfSesshin");

            migrationBuilder.RenameIndex(
                name: "IX_DaysOfSesshin_SesshinId",
                table: "DayOfSesshin",
                newName: "IX_DayOfSesshin_SesshinId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayOfSesshin",
                table: "DayOfSesshin",
                column: "DayOfSesshinId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayOfSesshin_Sesshin_SesshinId",
                table: "DayOfSesshin",
                column: "SesshinId",
                principalTable: "Sesshin",
                principalColumn: "SesshinId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_DayOfSesshin_DayOfSesshinId",
                table: "Meal",
                column: "DayOfSesshinId",
                principalTable: "DayOfSesshin",
                principalColumn: "DayOfSesshinId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
