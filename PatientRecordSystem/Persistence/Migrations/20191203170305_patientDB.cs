using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientRecordSystem.Persistence.Migrations
{
    public partial class patientDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffcialId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(maxLength: 255, nullable: false),
                    Value = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => new { x.PatientId, x.Key });
                    table.ForeignKey(
                        name: "FK_MetaData_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    DiseaseName = table.Column<string>(maxLength: 255, nullable: false),
                    TimeOfEntry = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Discription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_OffcialId",
                table: "Patients",
                column: "OffcialId",
                unique: true,
                filter: "[OffcialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Records_PatientId",
                table: "Records",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaData");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
