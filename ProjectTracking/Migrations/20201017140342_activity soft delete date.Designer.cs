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
    [Migration("20201017140342_activity soft delete date")]
    partial class activitysoftdeletedate
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
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<float?>("HourlyRate");

                    b.Property<string>("LastName")
                        .IsRequired()
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
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575", ConcurrencyStamp = "c3cb29ab-1fff-4f84-b762-49d1dd789d7c", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "admin@sys.com", EmailConfirmed = true, FirstName = "Sys", LastName = "Admin", NormalizedEmail = "ADMIN@SYS.COM", NormalizedUserName = "ADMIN", PasswordHash = "AQAAAAEAACcQAAAAEBQgpJCfsbeXohr79Be4u4gy1EoUYK7JmRoSKJT4OR9ZvdbzXlUJ4mLQoZS3tmB3bw==", RoleCode = (short)2, SecurityStamp = "", Title = "Admin", UserName = "admin" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e576", ConcurrencyStamp = "4de62068-4f0a-421d-a1b8-f79f522a935f", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "alikleitcr7@gmail.com", EmailConfirmed = true, FirstName = "Ali", LastName = "Kleit", NormalizedEmail = "ALIKLEITCR7@GMAIL.COM", NormalizedUserName = "ALIKLEIT", PasswordHash = "AQAAAAEAACcQAAAAEB9VzNsQyQnbd4hBDc3sZiUTCExJ21+xjueuULCiGxhbbdLxHGVXPJJjt5enHvSqFw==", RoleCode = (short)1, SecurityStamp = "", Title = "Developer", UserName = "alikleit" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e581", ConcurrencyStamp = "0a420ad6-d0eb-4db3-8fd6-6c53b14f839d", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "mark@project-tracking.com", EmailConfirmed = true, FirstName = "Mark", LastName = "Goldman", NormalizedEmail = "MARK@PROJECT-TRACKING.COM", NormalizedUserName = "MARK", PasswordHash = "AQAAAAEAACcQAAAAELGCCPF1tUH86qIAypLae37KAb0RKtx1CYXsazwZEk1cx3jf5XY+HwiiIDn/vrww1Q==", RoleCode = (short)1, SecurityStamp = "", Title = "Head IT", UserName = "mark" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e582", ConcurrencyStamp = "13744582-968c-46de-b535-bc824fcfb3b1", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ashton@project-tracking.com", EmailConfirmed = true, FirstName = "Ashton", LastName = "Kutcher", NormalizedEmail = "ASHTON@PROJECT-TRACKING.COM", NormalizedUserName = "ASHTON", PasswordHash = "AQAAAAEAACcQAAAAEMTcG5bptTPrRqOkFCsWl55W9DcT0XtKLzwFaZp14Jw/TB0i3sSW4JYfCczurEyRGA==", RoleCode = (short)1, SecurityStamp = "", Title = "Sr. Designer", UserName = "ashton" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e577", ConcurrencyStamp = "8b72c880-f16f-4a9a-95f2-e0085199dc1f", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ted@project-tracking.com", EmailConfirmed = true, FirstName = "Ted", LastName = "Mosby", NormalizedEmail = "TED@PROJECT-TRACKING.COM", NormalizedUserName = "TED", PasswordHash = "AQAAAAEAACcQAAAAEDxnFq6hdNi+Q/EdbuMfs/atr/7jkwRMoF/2gfZvHwewzQXJ22m3XKVsMcBqNTc7tw==", RoleCode = (short)0, SecurityStamp = "", Title = "Software Engineer", UserName = "ted" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e578", ConcurrencyStamp = "6813dac0-675d-4873-b547-343db473e3b2", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "marshall@project-tracking.com", EmailConfirmed = true, FirstName = "Marshall", LastName = "Eriksen", NormalizedEmail = "MARSHALL@PROJECT-TRACKING.COM", NormalizedUserName = "MARSHALL", PasswordHash = "AQAAAAEAACcQAAAAEB9FJrJPwdfg88yE2Kpdtns289tWncqz9+/iIhAsLUkUY6pEoN815F+g42Oeq66GlQ==", RoleCode = (short)0, SecurityStamp = "", Title = "Jr. Developer", UserName = "marshall" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e579", ConcurrencyStamp = "6cedbd3a-e7b7-420a-b071-01d93f4a9f7b", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "lilly@project-tracking.com", EmailConfirmed = true, FirstName = "Lilly", LastName = "Aldrin", NormalizedEmail = "LILLY@PROJECT-TRACKING.COM", NormalizedUserName = "LILLY", PasswordHash = "AQAAAAEAACcQAAAAEOiKXogpZgqQNMdBxTphEk7rEMJU/sYj5QtuJ4xpgD0lyGKG5duI6qwzCmHgmrxSyg==", RoleCode = (short)0, SecurityStamp = "", Title = "Dev Leader", UserName = "lilly" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e580", ConcurrencyStamp = "b3e691d0-e1c5-46d1-a186-1981e707d400", DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "robin@project-tracking.com", EmailConfirmed = true, FirstName = "Robin", LastName = "Scherbatsky", NormalizedEmail = "ROBIN@PROJECT-TRACKING.COM", NormalizedUserName = "ROBIN", PasswordHash = "AQAAAAEAACcQAAAAEIHXxgHeBBs6G3GGw8ONKZo4rhY3v8yYjKDvJPFN6xR7RWFpDD57oZfrQ92pC6rkRg==", RoleCode = (short)0, SecurityStamp = "", Title = "Graphic Designer", UserName = "robin" }
                    );
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Broadcast", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateSent");

                    b.Property<string>("FromUserId")
                        .IsRequired();

                    b.Property<string>("Message")
                        .HasMaxLength(255);

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
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15);

                    b.Property<string>("Title")
                        .HasMaxLength(50);

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

                    b.Property<string>("AddedByUserId")
                        .IsRequired();

                    b.Property<int>("CategoryId");

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

            modelBuilder.Entity("ProjectTracking.Data.DataSets.SupervisorLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedByUserId")
                        .IsRequired();

                    b.Property<DateTime>("DateAssigned");

                    b.Property<int>("TeamId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("AssignedByUserId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("SupervisorLog");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddedByUserId")
                        .IsRequired();

                    b.Property<string>("AssignedByUserId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("DateAssigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("SupervisorId");

                    b.HasKey("ID");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("AssignedByUserId");

                    b.HasIndex("SupervisorId");

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

                    b.Property<string>("UserId")
                        .IsRequired();

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
                        .HasColumnName("IpAdd")
                        .HasMaxLength(15);

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Message")
                        .HasMaxLength(150);

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
                        .HasColumnName("IpAdd")
                        .HasMaxLength(15);

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Message")
                        .HasMaxLength(150);

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
                        .HasColumnName("IpAdd")
                        .HasMaxLength(15);

                    b.Property<DateTime>("FromDate");

                    b.Property<short>("LogStatusCode");

                    b.Property<DateTime?>("ToDate");

                    b.Property<string>("UserId")
                        .IsRequired();

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

                    b.Property<string>("FromUserId")
                        .IsRequired();

                    b.Property<string>("Message")
                        .HasMaxLength(255);

                    b.Property<short>("NotificationTypeCode");

                    b.Property<string>("ToUserId")
                        .IsRequired();

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
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectTracking.Data.DataSets.Team", "ToTeam")
                        .WithMany()
                        .HasForeignKey("ToTeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Project", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AddedByUser")
                        .WithMany("AddedByUserProject")
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.Data.DataSets.Category", "Category")
                        .WithMany("Projects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
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

            modelBuilder.Entity("ProjectTracking.Data.DataSets.SupervisorLog", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AssignedByUser")
                        .WithMany("AssignedSupervisors")
                        .HasForeignKey("AssignedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.Data.DataSets.Team", "Team")
                        .WithMany("SupervisorLogs")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectTracking.ApplicationUser", "User")
                        .WithMany("Supervising")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.Team", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AddedByUser")
                        .WithMany("AddedByUserTeams")
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.ApplicationUser", "AssignedByUser")
                        .WithMany("AssignedSupervisorForTeams")
                        .HasForeignKey("AssignedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.ApplicationUser", "Supervisor")
                        .WithMany("SupervisedTeams")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Restrict);
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
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.ApplicationUser", "User")
                        .WithMany("TimeSheets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
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
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserNotification", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "FromUser")
                        .WithMany("FromUserNotifications")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.ApplicationUser", "ToUser")
                        .WithMany("ToUserNotifications")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
