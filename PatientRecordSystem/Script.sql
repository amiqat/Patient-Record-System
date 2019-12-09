IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [DeviceCodes] (
    [UserCode] nvarchar(200) NOT NULL,
    [DeviceCode] nvarchar(200) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DeviceCodes] PRIMARY KEY ([UserCode])
);

GO

CREATE TABLE [PersistedGrants] (
    [Key] nvarchar(200) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PersistedGrants] PRIMARY KEY ([Key])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_DeviceCodes_DeviceCode] ON [DeviceCodes] ([DeviceCode]);

GO

CREATE INDEX [IX_DeviceCodes_Expiration] ON [DeviceCodes] ([Expiration]);

GO

CREATE INDEX [IX_PersistedGrants_Expiration] ON [PersistedGrants] ([Expiration]);

GO

CREATE INDEX [IX_PersistedGrants_SubjectId_ClientId_Type] ON [PersistedGrants] ([SubjectId], [ClientId], [Type]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'3.1.0');

GO

CREATE TABLE [Patients] (
    [Id] int NOT NULL IDENTITY,
    [OffcialId] nvarchar(450) NULL,
    [Name] nvarchar(256) NOT NULL,
    [DateOfBirth] datetime2 NULL,
    [Email] nvarchar(256) NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [MetaData] (
    [PatientId] int NOT NULL,
    [Key] nvarchar(255) NOT NULL,
    [Value] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_MetaData] PRIMARY KEY ([PatientId], [Key]),
    CONSTRAINT [FK_MetaData_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Records] (
    [Id] int NOT NULL IDENTITY,
    [PatientId] int NOT NULL,
    [Amount] float NOT NULL DEFAULT 0.0E0,
    [DiseaseName] nvarchar(255) NOT NULL,
    [TimeOfEntry] datetime2 NOT NULL DEFAULT (getdate()),
    [Discription] nvarchar(max) NULL,
    CONSTRAINT [PK_Records] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Records_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_Patients_OffcialId] ON [Patients] ([OffcialId]) WHERE [OffcialId] IS NOT NULL;

GO

CREATE INDEX [IX_Records_PatientId] ON [Records] ([PatientId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191203170305_patientDB', N'3.1.0');

GO

DROP INDEX [IX_Patients_OffcialId] ON [Patients];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Patients]') AND [c].[name] = N'OffcialId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Patients] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Patients] ALTER COLUMN [OffcialId] int NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Patients_OffcialId] ON [Patients] ([OffcialId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191203171247_fixPatientOId', N'3.1.0');

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateOfBirth', N'Email', N'OffcialId', N'Name') AND [object_id] = OBJECT_ID(N'[Patients]'))
    SET IDENTITY_INSERT [Patients] ON;
INSERT INTO [Patients] ([Id], [DateOfBirth], [Email], [OffcialId], [Name])
VALUES (1, '1992-02-01T00:00:00.0000000', N'ahmad@tt.com', 1, N'Ahmad'),
(2, '1997-02-01T00:00:00.0000000', N'Sami@tt.com', 2, N'Sami'),
(3, '1998-02-01T00:00:00.0000000', N'Mohammad@tt.com', 3, N'Mohammad'),
(4, '1996-08-01T00:00:00.0000000', NULL, 4, N'Jane'),
(5, '2000-02-01T00:00:00.0000000', N'ahmad@tt.com', 5, N'Ameen');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateOfBirth', N'Email', N'OffcialId', N'Name') AND [object_id] = OBJECT_ID(N'[Patients]'))
    SET IDENTITY_INSERT [Patients] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PatientId', N'Key', N'Value') AND [object_id] = OBJECT_ID(N'[MetaData]'))
    SET IDENTITY_INSERT [MetaData] ON;
INSERT INTO [MetaData] ([PatientId], [Key], [Value])
VALUES (1, N'Age', N'56'),
(5, N'Age', N'28'),
(5, N'City', N'Ramallah'),
(4, N'Asthma', N'yes'),
(4, N'Cancer', N'yes'),
(4, N'Age', N'60'),
(3, N'Diabetes', N'yes'),
(3, N'City', N'Jenin'),
(3, N'Age', N'20'),
(2, N'City', N'Ramallah'),
(1, N'heart', N'open surgery'),
(1, N'city', N'Salfeet'),
(1, N'Diabetes', N'yes'),
(2, N'Age', N'35');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PatientId', N'Key', N'Value') AND [object_id] = OBJECT_ID(N'[MetaData]'))
    SET IDENTITY_INSERT [MetaData] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'Discription', N'DiseaseName', N'PatientId', N'TimeOfEntry') AND [object_id] = OBJECT_ID(N'[Records]'))
    SET IDENTITY_INSERT [Records] ON;
INSERT INTO [Records] ([Id], [Amount], [Discription], [DiseaseName], [PatientId], [TimeOfEntry])
VALUES (5, 50.0E0, NULL, N'Allergies', 5, '2019-12-08T18:37:53.5606416+02:00'),
(8, 50.0E0, NULL, N'Allergies', 2, '2019-12-08T18:37:53.5606426+02:00'),
(6, 70.0E0, NULL, N'Asthma', 1, '2019-12-08T18:37:53.5606419+02:00'),
(4, 30000.0E0, NULL, N'Surgery', 1, '2019-12-08T18:37:53.5606412+02:00'),
(3, 60.0E0, NULL, N'Eye', 1, '2019-12-08T18:37:53.5606403+02:00'),
(2, 100.0E0, NULL, N'ER', 1, '2019-12-08T18:37:53.5606299+02:00'),
(1, 50.0E0, NULL, N'Allergies', 1, '2019-12-08T18:37:53.5566867+02:00'),
(7, 70.0E0, NULL, N'Asthma', 5, '2019-12-08T18:37:53.5606423+02:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'Discription', N'DiseaseName', N'PatientId', N'TimeOfEntry') AND [object_id] = OBJECT_ID(N'[Records]'))
    SET IDENTITY_INSERT [Records] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191208163754_seedData', N'3.1.0');

GO

CREATE VIEW [dbo].[MetaDataStat] AS WITH cte AS (SELECT        COUNT(PatientId) AS count
                             FROM  dbo.MetaData AS md
                             GROUP BY PatientId)
                             SELECT AVG(count) AS avg, MAX(count) AS max
                               FROM cte AS cte_1; 

GO

CREATE VIEW [dbo].[TopMetaData] AS SELECT TOP 3  LOWER(md.[Key])[Key] ,count(LOWER(md.[Key])) count
                    from dbo.MetaData md
                    GROUP BY md.[Key]
                    ORDER BY  count(md.[Key]) DESC;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191208171447_metadata-Views', N'3.1.0');

GO

CREATE FUNCTION [dbo].[AvgBillWithoutOutliers]
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

GO

CREATE FUNCTION [dbo].[MostVisitMounth]
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

GO

CREATE VIEW [dbo].[PatientReportView] AS SELECT        Id, Name,
                             (SELECT        CAST(FLOOR(DATEDIFF(DAY, p.DateOfBirth, GETDATE()) / 365.25) AS int) AS Expr1) AS Age,
                             (SELECT        AVG(Amount) AS Expr1
                                FROM            dbo.Records AS r
                                    WHERE        (PatientId = p.Id)) AS Avg, dbo.AvgBillWithoutOutliers(Id) AS AvgW, dbo.MostVisitMounth(Id) AS MostVisitMounth
                                FROM            dbo.Patients AS p

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191208191437_PatientReportView', N'3.1.0');

GO

