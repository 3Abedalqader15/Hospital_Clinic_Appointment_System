using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Clinic_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class Edit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "BirthDay", "CreatedAt", "Email", "IsEmailConfirmed", "Name", "Password", "Phone_Number", "UpdatedAt", "isActive" },
                values: new object[,]
                {
                    { 1000, new DateTime(2003, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abood@Gmail.com", true, "Abood321", "123456789@", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 1001, new DateTime(1999, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmadali@Gmail.com", true, "Dr.Ahmad Ali", "AhmadAli@123", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 1002, new DateTime(1998, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "alihmad@Gmail.com", true, "Dr.Ali Ahmad", "AliAhmad@1234", "0779875103", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Bio", "CreatedAt", "ExperienceYears", "LicenseNumber", "Name", "Specialization", "UpdatedAt", "User_Id", "isActive", "profilePictureUrl" },
                values: new object[,]
                {
                    { 1, "Cardiac surgeon", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "DOC-001", "Dr. Ahmad Ali", "Cardiology", new DateTime(2026, 2, 9, 20, 10, 5, 396, DateTimeKind.Utc).AddTicks(5223), 1001, true, "/images/doctors/ahmad.jpg" },
                    { 2, "orthopedic specialist", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "DOC-002", "Dr. Ali Ahmad", "Orthopedics", new DateTime(2026, 2, 9, 20, 10, 5, 397, DateTimeKind.Utc).AddTicks(2795), 1002, true, "/images/doctors/ali.jpg" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Role_Id", "User_Id" },
                values: new object[,]
                {
                    { 3, 1000 },
                    { 2, 1001 },
                    { 2, 1002 }
                });
        }
    }
}
