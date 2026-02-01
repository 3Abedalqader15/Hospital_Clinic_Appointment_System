using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Clinic_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class Edit3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Bio", "Created_At", "ExperienceYears", "LicenseNumber", "Name", "Specialization", "User_Id", "isActive", "profilePictureUrl" },
                values: new object[,]
                {
                    { 1, "Cardiac surgeon", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "DOC-001", "Dr. Ahmad Ali", "Cardiology", 1001, true, "/images/doctors/ahmad.jpg" },
                    { 2, "orthopedic specialist", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "DOC-002", "Dr. Ali Ahmad", "Orthopedics", 1002, true, "/images/doctors/ali.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Doctors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
