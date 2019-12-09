using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientRecordSystem.Persistence.Migrations
{
    public partial class metadataViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[MetaDataStat] AS WITH cte AS (SELECT        COUNT(PatientId) AS count
                             FROM  dbo.MetaData AS md
                             GROUP BY PatientId)
                             SELECT AVG(count) AS avg, MAX(count) AS max
                               FROM cte AS cte_1; ");
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[TopMetaData] AS SELECT TOP 3  LOWER(md.[Key])[Key] ,count(LOWER(md.[Key])) count
                    from dbo.MetaData md
                    GROUP BY md.[Key]
                    ORDER BY  count(md.[Key]) DESC;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[MetaDataStat];");
            migrationBuilder.Sql("DROP VIEW [dbo].[TopMetaData];");
        }
    }
}