using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientRecordSystem.Persistence.Migrations
{
    public partial class PatientReportView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[AvgBillWithoutOutliers]
                    (
	                    -- Add the parameters for the function here
	                    @Id int
                    )
                    RETURNS float
                    AS
                    BEGIN
	                    -- Declare the return variable here
	                    DECLARE @ResultVar float

	                    -- Add the T-SQL statements to compute the return value here
	                    select @ResultVar= Avg(r.Amount)
                        from dbo.Records r
                        join ( select d.PatientId, Avg(d.Amount) as AvgWeight,
                                      STDEVP(d.Amount) StdDeviation
                                 from dbo.Records d
                                group by d.PatientId
                             ) d
                          on r.PatientId = d.PatientId
                         and r.Amount between d.AvgWeight- d.StdDeviation
                                          and d.AvgWeight+ d.StdDeviation
                       where  r.PatientId=@Id

	                    -- Return the result of the function
	                    RETURN @ResultVar

                    END
                    GO");
            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[MostVisitMounth]
                    (
	                    -- Add the parameters for the function here
	                    @Id int
                    )
                    RETURNS varchar(50)
                    AS
                    BEGIN
	                    -- Declare the return variable here
	                    DECLARE @ResultVar varchar(50)

	                    -- Add the T-SQL statements to compute the return value here
                    SELECT TOP 1 @ResultVar=FORMAT (r.TimeOfEntry,'MMMM')
                    from dbo.Records r
                    WHERE r.PatientId=1
                    GROUP BY FORMAT (r.TimeOfEntry,'MMMM')
                    ORDER BY count(FORMAT (r.TimeOfEntry,'MMMM')) desc

	                    -- Return the result of the function
	                    RETURN @ResultVar

                    END
                    GO");

            migrationBuilder.Sql(@"CREATE VIEW [dbo].[PatientReportView] AS SELECT        Id, Name,
                             (SELECT        CAST(FLOOR(DATEDIFF(DAY, p.DateOfBirth, GETDATE()) / 365.25) AS int) AS Expr1) AS Age,
                             (SELECT        AVG(Amount) AS Expr1
                                FROM            dbo.Records AS r
                                    WHERE        (PatientId = p.Id)) AS Avg, dbo.AvgBillWithoutOutliers(Id) AS AvgW, dbo.MostVisitMounth(Id) AS MostVisitMounth
                                FROM            dbo.Patients AS p");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION [dbo].[AvgBillWithoutOutliers];");
            migrationBuilder.Sql("DROP FUNCTION [dbo].[MostVisitMounth];");
            migrationBuilder.Sql("DROP VIEW [dbo].[PatientReportView];");
        }
    }
}