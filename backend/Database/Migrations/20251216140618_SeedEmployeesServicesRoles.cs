using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployeesServicesRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Email", "FirstName", "LastName", "LocationId", "PasswordHash", "PhoneNumber", "Salt", "Status" },
                values: new object[,]
                {
                    { 1, "john@gmail.com", "John", "Pork", 2, "JcDy7FOzBeRm3MvQpgahatB2YWAGMorZoh/U+s5rWu8=", "+53247865", "PcXPMJdOXdI8jaO51iElcQ==", 0 },
                    { 2, "bean@gmail.com", "Mr", "Bean", 2, "tPUom03r7Y6JKDBnlcqwacCXvKK1GHIWFwkf1A96wtg=", "+532324565", "ts6OSzF2JBMKDIFQo4KiCQ==", 0 },
                    { 3, "jazz@gmail.com", "Jazz", "Singsanong", 1, "yotfvuH/qPE5/d8RknKRJM1Km086Pe7IgT2Z0MUqXoE=", "+43278963", "BpclPWmfJoKhQ0n57VcWNA==", 0 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Employee" },
                    { 2, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "DurationMinutes", "LocationId", "ProductId", "Status" },
                values: new object[,]
                {
                    { 1, 60, 2, 4, 0 },
                    { 2, 60, 2, 5, 0 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "EmployeeId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeServiceQualifications",
                columns: new[] { "EmployeeId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumns: new[] { "EmployeeId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumns: new[] { "EmployeeId", "RoleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumns: new[] { "EmployeeId", "RoleId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeServiceQualifications",
                keyColumns: new[] { "EmployeeId", "ServiceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeServiceQualifications",
                keyColumns: new[] { "EmployeeId", "ServiceId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2);
        }
    }
}
