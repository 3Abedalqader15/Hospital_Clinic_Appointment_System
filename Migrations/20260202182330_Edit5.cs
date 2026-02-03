using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Clinic_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class Edit5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 3, 1000 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 2, 1001 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 2, 1002 },
                column: "Id",
                value: 0);
        }
    }
}
