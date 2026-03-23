using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodSafety.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Premises",
                columns: new[] { "Id", "Address", "RiskRating", "Town", "name" },
                values: new object[,]
                {
                    { 1, "12 Main St", 2, "Cork", "The Golden Spoon" },
                    { 2, "45 Oak Ave", 1, "Cork", "Burger Barn" },
                    { 3, "78 River Rd", 0, "Cork", "Pizza Palace" },
                    { 4, "23 Elm St", 2, "Cork", "Sushi Stop" },
                    { 5, "9 Hill Rd", 1, "Dublin", "The Breakfast Club" },
                    { 6, "34 Lake Dr", 0, "Dublin", "Noodle House" },
                    { 7, "56 Park Ln", 2, "Dublin", "Taco Town" },
                    { 8, "11 Beach Rd", 1, "Dublin", "The Green Leaf" },
                    { 9, "67 North St", 0, "Galway", "Curry Corner" },
                    { 10, "89 West Ave", 2, "Galway", "Fish & Chips Co" },
                    { 11, "14 East Rd", 1, "Galway", "The Sandwich Bar" },
                    { 12, "30 South St", 0, "Galway", "Sweet Treats" }
                });

            migrationBuilder.InsertData(
                table: "Inspections",
                columns: new[] { "Id", "InspectionDate", "Notes", "OutCome", "PremisesId", "Score" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 3, 1), "Hygiene issues found", 1, 1, 45 },
                    { 2, new DateOnly(2026, 3, 3), "Minor issues noted", 0, 2, 82 },
                    { 3, new DateOnly(2026, 3, 5), "All clear", 0, 3, 91 },
                    { 4, new DateOnly(2026, 3, 7), "Multiple violations", 1, 4, 38 },
                    { 5, new DateOnly(2026, 3, 10), "Adequate standards", 0, 5, 74 },
                    { 6, new DateOnly(2026, 3, 12), "Storage problems", 1, 6, 55 },
                    { 7, new DateOnly(2026, 3, 15), "Good overall", 0, 7, 88 },
                    { 8, new DateOnly(2026, 3, 18), "Minor temperature issues", 0, 8, 62 },
                    { 9, new DateOnly(2026, 2, 5), "Pest control needed", 1, 9, 41 },
                    { 10, new DateOnly(2026, 2, 10), "Satisfactory", 0, 10, 78 },
                    { 11, new DateOnly(2026, 2, 14), "Serious violations", 1, 11, 33 },
                    { 12, new DateOnly(2026, 2, 20), "Excellent", 0, 12, 95 },
                    { 13, new DateOnly(2026, 1, 8), "Improved since last visit", 0, 1, 60 },
                    { 14, new DateOnly(2026, 1, 15), "Cross contamination risk", 1, 2, 49 },
                    { 15, new DateOnly(2026, 1, 22), "Good hygiene", 0, 3, 85 },
                    { 16, new DateOnly(2025, 12, 3), "Acceptable", 0, 4, 72 },
                    { 17, new DateOnly(2025, 12, 10), "Temperature violations", 1, 5, 44 },
                    { 18, new DateOnly(2025, 12, 18), "Very good", 0, 6, 90 },
                    { 19, new DateOnly(2025, 11, 5), "Deep clean required", 1, 7, 37 },
                    { 20, new DateOnly(2025, 11, 12), "Well maintained", 0, 8, 81 },
                    { 21, new DateOnly(2025, 11, 20), "Minor issues", 0, 9, 66 },
                    { 22, new DateOnly(2025, 10, 8), "Drainage problems", 1, 10, 53 },
                    { 23, new DateOnly(2025, 10, 15), "Satisfactory", 0, 11, 77 },
                    { 24, new DateOnly(2025, 10, 22), "Critical violations", 1, 12, 29 },
                    { 25, new DateOnly(2025, 9, 10), "All standards met", 0, 1, 88 }
                });

            migrationBuilder.InsertData(
                table: "FollowUps",
                columns: new[] { "Id", "ClosedDate", "DueDate", "InspectionId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateOnly(2026, 2, 1), 1, 0 },
                    { 2, null, new DateOnly(2026, 1, 15), 4, 0 },
                    { 3, null, new DateOnly(2026, 1, 20), 9, 0 },
                    { 4, null, new DateOnly(2026, 2, 10), 11, 0 },
                    { 5, null, new DateOnly(2026, 2, 28), 14, 0 },
                    { 6, null, new DateOnly(2025, 12, 20), 17, 0 },
                    { 7, null, new DateOnly(2025, 12, 1), 19, 0 },
                    { 8, new DateOnly(2026, 3, 15), new DateOnly(2026, 3, 10), 6, 1 },
                    { 9, new DateOnly(2025, 11, 20), new DateOnly(2025, 11, 1), 22, 1 },
                    { 10, new DateOnly(2025, 12, 1), new DateOnly(2025, 11, 15), 24, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FollowUps",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Inspections",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Premises",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
