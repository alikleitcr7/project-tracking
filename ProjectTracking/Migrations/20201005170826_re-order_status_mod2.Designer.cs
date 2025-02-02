﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectTracking.Data;

namespace ProjectTracking.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201005170826_re-order_status_mod2")]
    partial class reorder_status_mod2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjectTracking.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<short?>("EmploymentTypeCode");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<float?>("HourlyRate");

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.Property<float?>("MonthlySalary");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<short>("RoleCode");

                    b.Property<string>("SecurityStamp");

                    b.Property<int?>("TeamId");

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575", ConcurrencyStamp = "ca03ea89-c88c-489b-9f4a-ed1ac4ba79c4", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "admin@sys.com", EmailConfirmed = true, FirstName = "Admin", NormalizedEmail = "ADMIN@SYS.COM", NormalizedUserName = "ADMIN", PasswordHash = "AQAAAAEAACcQAAAAEPar/ZotfPZiBbTehtwmmZy6qiZkX5zB9PYSWbgeXa1Vh9sbZigq7Eaz11YqgzSXaQ==", RoleCode = (short)2, SecurityStamp = "", Title = "Admin", UserName = "admin" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e576", ConcurrencyStamp = "5b4e15c5-28cb-4cee-b52b-7107298d4838", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "alikleitcr7@gmail.com", EmailConfirmed = true, FirstName = "Ali", LastName = "Kleit", NormalizedEmail = "ALIKLEITCR7@GMAIL.COM", NormalizedUserName = "ALIKLEIT", PasswordHash = "AQAAAAEAACcQAAAAEAMBuQWlCkmP/88upcDV7NPCeQV8LI+ZVgoI30ewmT1KC62QGsv1lWkF+49d1bJ6bA==", RoleCode = (short)1, SecurityStamp = "", Title = "Developer", UserName = "alikleit" }
                    );
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Broadcast", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateSent");

                    b.Property<string>("FromUserId");

                    b.Property<string>("Message");

                    b.Property<short>("NotificationTypeCode");

                    b.Property<int>("ToTeamId");

                    b.HasKey("ID");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToTeamId");

                    b.ToTable("Broadcasts");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.IpAddress", b =>
                {
                    b.Property<string>("Address")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Address");

                    b.ToTable("IpAddresses");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActualEnd");

                    b.Property<string>("AddedByUserId");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("PlannedEnd");

                    b.Property<DateTime?>("StartDate");

                    b.Property<short>("StatusCode");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectStatusModification", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<DateTime>("DateModified");

                    b.Property<short>("StatusCode");

                    b.HasKey("ProjectId", "DateModified");

                    b.ToTable("ProjectStatusModifications");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectTask", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActualEnd");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("PlannedEnd");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<short>("StatusCode");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectTaskStatusModification", b =>
                {
                    b.Property<int>("ProjectTaskId");

                    b.Property<DateTime>("DateModified");

                    b.Property<short>("StatusCode");

                    b.HasKey("ProjectTaskId", "DateModified");

                    b.ToTable("ProjectTaskStatusModifications");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Superviser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedByUserId");

                    b.Property<DateTime>("DateAssigned");

                    b.Property<int>("TeamId");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("AssignedByUserId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("Supervisor");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddedByUserId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.HasIndex("AddedByUserId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TeamsProjects", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("TeamId");

                    b.HasKey("ProjectId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamsProjects");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddedByUserId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("FromDate");

                    b.Property<DateTime>("ToDate");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeSheets");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheetActivity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnName("IpAdd");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Message");

                    b.Property<int>("TimeSheetTaskId");

                    b.Property<DateTime?>("ToDate");

                    b.HasKey("ID");

                    b.HasIndex("Address");

                    b.HasIndex("TimeSheetTaskId");

                    b.ToTable("TimeSheetActivities");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheetActivityLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnName("IpAdd");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Message");

                    b.Property<int>("TimeSheetActivityId");

                    b.Property<DateTime?>("ToDate");

                    b.HasKey("ID");

                    b.HasIndex("Address");

                    b.HasIndex("TimeSheetActivityId");

                    b.ToTable("TimeSheetActivityLogs");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheetTask", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectTaskId");

                    b.Property<int>("TimeSheetId");

                    b.HasKey("ID");

                    b.HasIndex("ProjectTaskId");

                    b.HasIndex("TimeSheetId");

                    b.ToTable("TimeSheetTasks");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnName("IpAdd");

                    b.Property<DateTime>("FromDate");

                    b.Property<short>("LogStatusCode");

                    b.Property<DateTime?>("ToDate");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("Address");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogging");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserNotification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateSent");

                    b.Property<string>("FromUserId");

                    b.Property<string>("Message");

                    b.Property<short>("NotificationTypeCode");

                    b.Property<string>("ToUserId");

                    b.HasKey("ID");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.ApplicationUser", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Broadcast", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "FromUser")
                        .WithMany("Broadcasts")
                        .HasForeignKey("FromUserId");

                    b.HasOne("ProjectTracking.Data.DataSets.Team", "ToTeam")
                        .WithMany()
                        .HasForeignKey("ToTeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Project", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AddedByUser")
                        .WithMany("Projects")
                        .HasForeignKey("AddedByUserId");

                    b.HasOne("ProjectTracking.Data.DataSets.Category", "Category")
                        .WithMany("Projects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectStatusModification", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.Project", "Project")
                        .WithMany("ProjectStatusModifications")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectTask", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectTaskStatusModification", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.ProjectTask", "ProjectTask")
                        .WithMany("ProjectTaskStatusModifications")
                        .HasForeignKey("ProjectTaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Superviser", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AssignedByUser")
                        .WithMany("AssignedSupervisors")
                        .HasForeignKey("AssignedByUserId");

                    b.HasOne("ProjectTracking.Data.DataSets.Team", "Team")
                        .WithMany("Supervisers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectTracking.ApplicationUser", "User")
                        .WithMany("Supervising")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Team", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AddedByUser")
                        .WithMany()
                        .HasForeignKey("AddedByUserId");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TeamsProjects", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.Project", "Project")
                        .WithMany("TeamsProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectTracking.Data.DataSets.Team", "Team")
                        .WithMany("TeamsProjects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheet", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AddedByUser")
                        .WithMany("AddedByUserTimeSheets")
                        .HasForeignKey("AddedByUserId");

                    b.HasOne("ProjectTracking.ApplicationUser", "User")
                        .WithMany("TimeSheets")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheetActivity", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.IpAddress", "IpAddress")
                        .WithMany("TimeSheetActivities")
                        .HasForeignKey("Address");

                    b.HasOne("ProjectTracking.Data.DataSets.TimeSheetTask", "TimeSheetTask")
                        .WithMany("Activities")
                        .HasForeignKey("TimeSheetTaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheetActivityLog", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.IpAddress", "IpAddress")
                        .WithMany("TimeSheetActivityLogs")
                        .HasForeignKey("Address");

                    b.HasOne("ProjectTracking.Data.DataSets.TimeSheetActivity", "TimeSheetActivity")
                        .WithMany("TimeSheetActivityLogs")
                        .HasForeignKey("TimeSheetActivityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.TimeSheetTask", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.ProjectTask", "ProjectTask")
                        .WithMany("TimeSheetTasks")
                        .HasForeignKey("ProjectTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectTracking.Data.DataSets.TimeSheet", "TimeSheet")
                        .WithMany("TimeSheetTasks")
                        .HasForeignKey("TimeSheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserLog", b =>
                {
                    b.HasOne("ProjectTracking.Data.DataSets.IpAddress", "IpAddress")
                        .WithMany("UserLogs")
                        .HasForeignKey("Address");

                    b.HasOne("ProjectTracking.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserNotification", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("ProjectTracking.ApplicationUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
