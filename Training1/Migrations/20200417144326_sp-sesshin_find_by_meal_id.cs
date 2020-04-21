using Microsoft.EntityFrameworkCore.Migrations;

namespace Training1.Migrations
{
    public partial class spsesshin_find_by_meal_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                    DROP PROCEDURE IF EXISTS [dbo].[SESSHIN_FIND_BY_MEAL_ID]
                    GO
                    CREATE PROCEDURE[dbo].[SESSHIN_FIND_BY_MEAL_ID]
                        @mealId int
                    AS
                    BEGIN
                        SET NOCOUNT ON;
                        SELECT Sesshin.*
                        FROM

                            Meal
                            INNER JOIN DaysOfSesshin ON DaysOfSesshin.DayOfSesshinId = Meal.DayOfSesshinId

                            INNER JOIN Sesshin ON DaysOfSesshin.SesshinId = Sesshin.SesshinId

                        WHERE Meal.MealId = @mealId
                    END";

            migrationBuilder.Sql(sp);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
