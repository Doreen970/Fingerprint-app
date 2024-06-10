using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeviceRelationshipStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_StaffId",
                table: "Devices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fecb081-a7fc-4c23-8866-b02dbc012459");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8af4326-938a-40f6-8769-beacff922dd9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0bdd8187-5433-4144-b461-e18d5694596a", null, "Staff", "STAFF" },
                    { "e19da598-4234-498b-843d-44b2b2fb4cde", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 1,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7211), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7214) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 2,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7216), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7217) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 3,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7218), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7218) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 4,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7219), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7220) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 5,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7221), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7222) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 6,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7223), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7223) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 7,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7224), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7225) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 8,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7226), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7226) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 9,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7227), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7228) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 10,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7229), new DateTime(2024, 6, 10, 9, 17, 0, 561, DateTimeKind.Utc).AddTicks(7229) });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_StaffId",
                table: "Devices",
                column: "StaffId",
                unique: true,
                filter: "[StaffId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_StaffId",
                table: "Devices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdd8187-5433-4144-b461-e18d5694596a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e19da598-4234-498b-843d-44b2b2fb4cde");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9fecb081-a7fc-4c23-8866-b02dbc012459", null, "Staff", "STAFF" },
                    { "e8af4326-938a-40f6-8769-beacff922dd9", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 1,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8498), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8501) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 2,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8502), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8503) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 3,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8504), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8504) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 4,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8505), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8506) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 5,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8508), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8508) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 6,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8509), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 7,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8511), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8511) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 8,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8513), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8513) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 9,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8514), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8514) });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: 10,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8515), new DateTime(2024, 6, 10, 8, 41, 8, 78, DateTimeKind.Utc).AddTicks(8515) });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_StaffId",
                table: "Devices",
                column: "StaffId");
        }
    }
}
