using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientRecordSystem.Persistence.Migrations
{
    public partial class fixPatientOId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_OffcialId",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "OffcialId",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_OffcialId",
                table: "Patients",
                column: "OffcialId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_OffcialId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "OffcialId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Patients_OffcialId",
                table: "Patients",
                column: "OffcialId",
                unique: true,
                filter: "[OffcialId] IS NOT NULL");
        }
    }
}
