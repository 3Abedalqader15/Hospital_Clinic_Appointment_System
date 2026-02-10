using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Clinic_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "CreatedAt", "Email", "IsEmailConfirmed", "Name", "Password", "Phone_Number", "UpdatedAt", "isActive" },
                values: new object[] { 1000, new DateTime(2003, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abood@Gmail.com", true, "AboodAlfaqeeh", "123456789@Abcd", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role_Id", "User_Id" },
                values: new object[] { 3, 1000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 3, 1000 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1000);
        }
    }
}
