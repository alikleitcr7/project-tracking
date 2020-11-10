﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectTracking.Data;

namespace ProjectTracking.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("RoleAssignedByUserId");

                    b.Property<DateTime>("RoleAssignedDate");

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

                    b.HasIndex("RoleAssignedByUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575", ConcurrencyStamp = "730b3e18-0381-4b30-9be0-a120500686d8", DateOfBirth = new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "admin@sys.com", EmailConfirmed = true, FirstName = "Sys", LastName = "Admin", NormalizedEmail = "ADMIN@SYS.COM", NormalizedUserName = "ADMIN", PasswordHash = "AQAAAAEAACcQAAAAEOhyCW3XGoiIAFA3TZvIwlsPgt0D+PA8QY6s4ZHNTM195dVrqcQYLrUHjukt+EVK/w==", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 365, DateTimeKind.Local), RoleCode = (short)2, SecurityStamp = "", Title = "Admin", UserName = "admin" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e576", ConcurrencyStamp = "ce04ff31-99c6-4c43-9539-25ed60183ec6", DateOfBirth = new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "alikleitcr7@gmail.com", EmailConfirmed = true, FirstName = "Ali", LastName = "Kleit", NormalizedEmail = "ALIKLEITCR7@GMAIL.COM", NormalizedUserName = "ALIKLEIT", PasswordHash = "AQAAAAEAACcQAAAAEJSx+aohd+9JQOMwGriFa+2SskN2M5RJEM+8dPLNaxnf4UqN8ggBodTS6tqogbtU+w==", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 373, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Developer", UserName = "alikleit" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e581", ConcurrencyStamp = "89e7fefb-bd0e-487a-8a60-d31f2494d085", DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "mark@project-tracking.com", EmailConfirmed = true, FirstName = "Mark", LastName = "Goldman", NormalizedEmail = "MARK@PROJECT-TRACKING.COM", NormalizedUserName = "MARK", PasswordHash = "AQAAAAEAACcQAAAAEJuA6rKt4ZeDG0R5SrJf/UJQIIaUsQ6+fI2U1VWy5Y1H43k8TpbK2WalmjpdVSrSyg==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 385, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Head IT", UserName = "mark" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e582", ConcurrencyStamp = "9dd65704-67ee-47ed-8c17-feda933e232f", DateOfBirth = new DateTime(1996, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ashton@project-tracking.com", EmailConfirmed = true, FirstName = "Ashton", LastName = "Kutcher", NormalizedEmail = "ASHTON@PROJECT-TRACKING.COM", NormalizedUserName = "ASHTON", PasswordHash = "AQAAAAEAACcQAAAAEFiYDkDNO20JYSlhQFlsO+m/r9AJpgIroeo6A7kTqx6/D2q2zTYolgp2JjqeNBlmUw==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 392, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Sr. Designer", UserName = "ashton" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e577", ConcurrencyStamp = "46a59529-58f1-4462-96a1-677210e19aa7", DateOfBirth = new DateTime(1996, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ted@project-tracking.com", EmailConfirmed = true, FirstName = "Ted", LastName = "Mosby", NormalizedEmail = "TED@PROJECT-TRACKING.COM", NormalizedUserName = "TED", PasswordHash = "AQAAAAEAACcQAAAAEE6nzF6y/58bPo33uOeCLj0y3eDI7ulUWAQG5k1ZIhwKUGlX6G46be/E4Sl94pTJWA==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 399, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Software Engineer", UserName = "ted" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e578", ConcurrencyStamp = "3aeec919-0041-4ecf-811d-757c45ee3538", DateOfBirth = new DateTime(1996, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "marshall@project-tracking.com", EmailConfirmed = true, FirstName = "Marshall", LastName = "Eriksen", NormalizedEmail = "MARSHALL@PROJECT-TRACKING.COM", NormalizedUserName = "MARSHALL", PasswordHash = "AQAAAAEAACcQAAAAEC0n1L6Z7FgeoEZJl1LjRc2JbOaE83jMoH1u/Sm5Kh1dOzb5MwJ558BCK3YdFn4YSA==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 405, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Jr. Developer", UserName = "marshall" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e579", ConcurrencyStamp = "d6b88e31-b343-458e-a2c6-69ba60bf7d49", DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "lilly@project-tracking.com", EmailConfirmed = true, FirstName = "Lilly", LastName = "Aldrin", NormalizedEmail = "LILLY@PROJECT-TRACKING.COM", NormalizedUserName = "LILLY", PasswordHash = "AQAAAAEAACcQAAAAEP/xfraHq5zd+BopqiJr1r5xBzw3U0wWIPXgXCzUrATiqS0humcyLla3rwTjdDCt4w==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 412, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Dev Leader", UserName = "lilly" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e580", ConcurrencyStamp = "d7f3d125-8e40-479c-8163-3217417a8631", DateOfBirth = new DateTime(1996, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "robin@project-tracking.com", EmailConfirmed = true, FirstName = "Robin", LastName = "Scherbatsky", NormalizedEmail = "ROBIN@PROJECT-TRACKING.COM", NormalizedUserName = "ROBIN", PasswordHash = "AQAAAAEAACcQAAAAEJCZOq+od/d6hTwG90S3FMssEzRldwNR1y6WXSkVAL2fCbA+oDcrWcRPU/OWxEsYFg==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 11, 10, 21, 57, 38, 419, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Graphic Designer", UserName = "robin" }
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

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<DateTime?>("PlannedEnd");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("StatusByUserId");

                    b.Property<short>("StatusCode");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StatusByUserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectStatusModification", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("ModifiedByUserId");

                    b.Property<short>("StatusCode");

                    b.HasKey("ProjectId", "DateModified");

                    b.HasIndex("ModifiedByUserId");

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

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<DateTime?>("PlannedEnd");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("StatusByUserId");

                    b.Property<short>("StatusCode");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusByUserId");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectTaskStatusModification", b =>
                {
                    b.Property<int>("ProjectTaskId");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("ModifiedByUserId")
                        .IsRequired();

                    b.Property<short>("StatusCode");

                    b.HasKey("ProjectTaskId", "DateModified");

                    b.HasIndex("ModifiedByUserId");

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

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("ProjectTaskId");

                    b.Property<int?>("TimeSheetId");

                    b.Property<string>("ToUserId")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectTaskId");

                    b.HasIndex("TimeSheetId");

                    b.HasIndex("ToUserId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserRoleLog", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<DateTime>("DateAssigned");

                    b.Property<string>("AssignedByUserId")
                        .IsRequired();

                    b.Property<short>("RoleCode");

                    b.HasKey("UserId", "DateAssigned");

                    b.HasIndex("AssignedByUserId");

                    b.ToTable("UserRoleLogs");
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
                    b.HasOne("ProjectTracking.ApplicationUser", "RoleAssignedByUser")
                        .WithMany()
                        .HasForeignKey("RoleAssignedByUserId");

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

                    b.HasOne("ProjectTracking.ApplicationUser", "StatusByUser")
                        .WithMany()
                        .HasForeignKey("StatusByUserId");
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectStatusModification", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

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

                    b.HasOne("ProjectTracking.ApplicationUser", "StatusByUser")
                        .WithMany("ProjectTaskStatusByUser")
                        .HasForeignKey("StatusByUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.ProjectTaskStatusModification", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "ModifiedByUser")
                        .WithMany("ProjectTaskStatusModificationByUser")
                        .HasForeignKey("ModifiedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

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
                        .HasForeignKey("Address")
                        .OnDelete(DeleteBehavior.SetNull);

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

                    b.HasOne("ProjectTracking.Data.DataSets.Project", "Project")
                        .WithMany("UserNotifications")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ProjectTracking.Data.DataSets.ProjectTask", "ProjectTask")
                        .WithMany("UserNotifications")
                        .HasForeignKey("ProjectTaskId");

                    b.HasOne("ProjectTracking.Data.DataSets.TimeSheet", "TimeSheet")
                        .WithMany("UserNotifications")
                        .HasForeignKey("TimeSheetId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ProjectTracking.ApplicationUser", "ToUser")
                        .WithMany("ToUserNotifications")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ProjectTracking.Data.DataSets.UserRoleLog", b =>
                {
                    b.HasOne("ProjectTracking.ApplicationUser", "AssignedByUser")
                        .WithMany("AssignedUserRoleLogs")
                        .HasForeignKey("AssignedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectTracking.ApplicationUser", "User")
                        .WithMany("UserRoleLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
