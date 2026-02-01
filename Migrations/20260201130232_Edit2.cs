using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Clinic_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class Edit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Roles",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Doctor" },
                    { 2, "Patient" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "Created_At", "Email", "IsEmailConfirmed", "Name", "Password", "Phone_Number", "Updated_At", "isActive" },
                values: new object[,]
                {
                    { 1000, new DateTime(2003, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abood@Gmail.com", true, "Abood321", "123456789@", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 1001, new DateTime(1999, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmadali@Gmail.com", true, "Dr.Ahmad Ali", "AhmadAli@123", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 1002, new DateTime(1998, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "alihmad@Gmail.com", true, "Dr.Ali Ahmad", "AliAhmad@1234", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role_Id", "User_Id", "Id" },
                values: new object[,]
                {
                    { 3, 1000, 0 },
                    { 2, 1001, 0 },
                    { 2, 1002, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 3, 1000 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 2, 1001 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 2, 1002 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "Description");
        }
    }
}
