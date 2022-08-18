using PatientRecordSystem.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Duende.IdentityServer.EntityFramework.Options;
using PatientRecordSystem.Domain.Models;

namespace PatientRecordSystem.Persistence.Contexts
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MetaData> MetaData { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<MetaDataStat> metaDataStats { get; set; }
        public DbSet<TopMetaData> TopMetaData { get; set; }
        public DbSet<PatientReport> PatientReport { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(x => x.PatientName).HasColumnName("Name").HasMaxLength(256).IsRequired();
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.OffcialId).IsRequired();
                entity.HasIndex(x => x.OffcialId).IsUnique();
                entity.Property<string>(x => x.Email)
                     .HasColumnType("nvarchar(256)")
                     .HasMaxLength(256);
                entity.HasMany<MetaData>(g => g.MetaData)
                .WithOne(s => s.Patient)
                .HasForeignKey(s => s.PatientId);
                entity.ToTable("Patients");
                entity.HasData(
                    new Patient
                    {
                        Id = 1,
                        OffcialId = 1,
                        PatientName = "Ahmad",
                        DateOfBirth = new DateTime(1992, 2, 1),
                        Email = "ahmad@tt.com"
                    },
                    new Patient
                    {
                        Id = 2,
                        OffcialId = 2,
                        PatientName = "Sami",
                        DateOfBirth = new DateTime(1997, 2, 1),
                        Email = "Sami@tt.com"
                    },
                    new Patient
                    {
                        Id = 3,
                        OffcialId = 3,
                        PatientName = "Mohammad",
                        DateOfBirth = new DateTime(1998, 2, 1),
                        Email = "Mohammad@tt.com"
                    }, new Patient
                    {
                        Id = 4,
                        OffcialId = 4,
                        PatientName = "Jane",
                        DateOfBirth = new DateTime(1996, 8, 1),
                    }
                    , new Patient
                    {
                        Id = 5,
                        OffcialId = 5,
                        PatientName = "Ameen",
                        DateOfBirth = new DateTime(2000, 2, 1),
                        Email = "ahmad@tt.com"
                    }

                    ); ;
            });
            modelBuilder.Entity<MetaData>(entity =>
            {
                entity.HasKey(x => new { x.PatientId, x.Key });
                entity.Property(x => x.Key).HasMaxLength(255);
                entity.Property(x => x.Value).HasMaxLength(255).IsRequired();
                entity.HasData(
                    new MetaData
                    {
                        PatientId = 1,
                        Key = "Age",
                        Value = "56"
                    }, new MetaData
                    {
                        PatientId = 1,
                        Key = "Diabetes",
                        Value = "yes"
                    }, new MetaData
                    {
                        PatientId = 1,
                        Key = "city",
                        Value = "Salfeet"
                    }, new MetaData
                    {
                        PatientId = 1,
                        Key = "heart",
                        Value = "open surgery"
                    }, new MetaData
                    {
                        PatientId = 2,
                        Key = "Age",
                        Value = "35"
                    }, new MetaData
                    {
                        PatientId = 2,
                        Key = "City",
                        Value = "Ramallah"
                    }, new MetaData
                    {
                        PatientId = 3,
                        Key = "Age",
                        Value = "20"
                    }, new MetaData
                    {
                        PatientId = 3,
                        Key = "City",
                        Value = "Jenin"
                    }, new MetaData
                    {
                        PatientId = 3,
                        Key = "Diabetes",
                        Value = "yes"
                    }, new MetaData
                    {
                        PatientId = 4,
                        Key = "Age",
                        Value = "60"
                    }, new MetaData
                    {
                        PatientId = 4,
                        Key = "Cancer",
                        Value = "yes"
                    }, new MetaData
                    {
                        PatientId = 4,
                        Key = "Asthma",
                        Value = "yes"
                    }, new MetaData
                    {
                        PatientId = 5,
                        Key = "City",
                        Value = "Ramallah"
                    }, new MetaData
                    {
                        PatientId = 5,
                        Key = "Age",
                        Value = "28"
                    }
                    );
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.DiseaseName).HasMaxLength(255).IsRequired();
                entity.Property<DateTime>(x => x.TimeOfEntry).HasDefaultValueSql("getdate()");
                entity.Property<float>(x => x.Amount).HasColumnType("float").HasDefaultValue(0);
                entity.HasOne<Patient>(g => g.Patient)
                .WithMany(s => s.Records)
                .HasForeignKey(s => s.PatientId);
                entity.ToTable("Records");
                entity.HasData(
                    new Record
                    {
                        Id = 1,
                        PatientId = 1,
                        Amount = 50f,
                        DiseaseName = "Allergies",
                        TimeOfEntry = DateTime.Now
                    },
                     new Record
                     {
                         Id = 2,
                         PatientId = 1,
                         Amount = 100f,
                         DiseaseName = "ER",
                         TimeOfEntry = DateTime.Now
                     },
                      new Record
                      {
                          Id = 3,
                          PatientId = 1,
                          Amount = 60f,
                          DiseaseName = "Eye",
                      }, new Record
                      {
                          Id = 4,
                          PatientId = 1,
                          Amount = 30000f,
                          DiseaseName = "Surgery",
                      }, new Record
                      {
                          Id = 5,
                          PatientId = 5,
                          Amount = 50,
                          DiseaseName = "Allergies",
                      }, new Record
                      {
                          Id = 6,
                          PatientId = 1,
                          Amount = 70,
                          DiseaseName = "Asthma",
                      }, new Record
                      {
                          Id = 7,
                          PatientId = 5,
                          Amount = 70,
                          DiseaseName = "Asthma",
                      }, new Record
                      {
                          Id = 8,
                          PatientId = 2,
                          Amount = 50,
                          DiseaseName = "Allergies",
                      }
                    );
            });

            modelBuilder.Entity<MetaDataStat>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("MetaDataStat");
                entity.Property(x => x.AveragePerPatient).HasColumnName("avg");
                entity.Property(x => x.MaxPerPatient).HasColumnName("max");
            });
            modelBuilder.Entity<TopMetaData>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("TopMetaData");
            });
            modelBuilder.Entity<PatientReport>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("PatientReportView");
                entity.Property(x => x.PatientName).HasColumnName("Name");
            });
        }
    }
}