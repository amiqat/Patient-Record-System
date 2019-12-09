using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientRecordSystem.Persistence.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "DateOfBirth", "Email", "OffcialId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1992, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmad@tt.com", 1, "Ahmad" },
                    { 2, new DateTime(1997, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sami@tt.com", 2, "Sami" },
                    { 3, new DateTime(1998, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohammad@tt.com", 3, "Mohammad" },
                    { 4, new DateTime(1996, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, "Jane" },
                    { 5, new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmad@tt.com", 5, "Ameen" }
                });

            migrationBuilder.InsertData(
                table: "MetaData",
                columns: new[] { "PatientId", "Key", "Value" },
                values: new object[,]
                {
                    { 1, "Age", "56" },
                    { 5, "Age", "28" },
                    { 5, "City", "Ramallah" },
                    { 4, "Asthma", "yes" },
                    { 4, "Cancer", "yes" },
                    { 4, "Age", "60" },
                    { 3, "Diabetes", "yes" },
                    { 3, "City", "Jenin" },
                    { 3, "Age", "20" },
                    { 2, "City", "Ramallah" },
                    { 1, "heart", "open surgery" },
                    { 1, "city", "Salfeet" },
                    { 1, "Diabetes", "yes" },
                    { 2, "Age", "35" }
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "Amount", "Discription", "DiseaseName", "PatientId", "TimeOfEntry" },
                values: new object[,]
                {
                    { 5, 50.0, null, "Allergies", 5, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6416) },
                    { 8, 50.0, null, "Allergies", 2, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6426) },
                    { 6, 70.0, null, "Asthma", 1, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6419) },
                    { 4, 30000.0, null, "Surgery", 1, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6412) },
                    { 3, 60.0, null, "Eye", 1, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6403) },
                    { 2, 100.0, null, "ER", 1, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6299) },
                    { 1, 50.0, null, "Allergies", 1, new DateTime(2019, 12, 8, 18, 37, 53, 556, DateTimeKind.Local).AddTicks(6867) },
                    { 7, 70.0, null, "Asthma", 5, new DateTime(2019, 12, 8, 18, 37, 53, 560, DateTimeKind.Local).AddTicks(6423) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 1, "Age" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 1, "city" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 1, "Diabetes" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 1, "heart" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 2, "Age" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 2, "City" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 3, "Age" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 3, "City" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 3, "Diabetes" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 4, "Age" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 4, "Asthma" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 4, "Cancer" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 5, "Age" });

            migrationBuilder.DeleteData(
                table: "MetaData",
                keyColumns: new[] { "PatientId", "Key" },
                keyValues: new object[] { 5, "City" });

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
