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
    [Migration("20201201100116_removed salaries")]
    partial class removedsalaries
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

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<bool>("NotificationFlag");

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
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575", ConcurrencyStamp = "51a69990-ad6e-45fd-b936-cb35da056d56", DateOfBirth = new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "admin@sys.com", EmailConfirmed = true, FirstName = "Sys", LastName = "Admin", NormalizedEmail = "ADMIN@SYS.COM", NormalizedUserName = "ADMIN", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEPvu4+PM3grE0Z3Hl/Ftxpzu93dnddh+iiwdSOYE4wLDJG3Jd3is8aTsadRKun8z8A==", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 816, DateTimeKind.Local), RoleCode = (short)2, SecurityStamp = "", Title = "Admin", UserName = "admin" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e576", ConcurrencyStamp = "24a1a706-8eb8-41d3-aa52-6ca16068e5b8", DateOfBirth = new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "alikleitcr7@gmail.com", EmailConfirmed = true, FirstName = "Ali", LastName = "Kleit", NormalizedEmail = "ALIKLEITCR7@GMAIL.COM", NormalizedUserName = "ALIKLEIT", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEJn7/NTvSRLMZztuxD/26oDoRZG2p4akGUg5bO8pMwWlUxT97HCLFcesLj1ReoTzmA==", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 825, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Developer", UserName = "alikleit" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e581", ConcurrencyStamp = "54c51775-f176-4a6e-8e24-19fe30f779ed", DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "mark@project-tracking.com", EmailConfirmed = true, FirstName = "Mark", LastName = "Goldman", NormalizedEmail = "MARK@PROJECT-TRACKING.COM", NormalizedUserName = "MARK", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAENq471LTkRuXfTTxHyAEDYriesYAOICy/4MGyaMxlK8tbXB7BJUr/aAwFQDlQvc7Hg==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 834, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Head IT", UserName = "mark" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e582", ConcurrencyStamp = "1891b68e-655f-4008-89c8-a234e0bfea2e", DateOfBirth = new DateTime(1996, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ashton@project-tracking.com", EmailConfirmed = true, FirstName = "Ashton", LastName = "Kutcher", NormalizedEmail = "ASHTON@PROJECT-TRACKING.COM", NormalizedUserName = "ASHTON", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEBTYVtX30t2owiw7/BAxCR1wNE+pP6oKS7Zk4JQXdllgw2d0QdKh7JhBBRdiTWke4Q==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 844, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Sr. Designer", UserName = "ashton" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e577", ConcurrencyStamp = "7314171b-1643-4e36-b57a-36bd8069db8e", DateOfBirth = new DateTime(1996, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ted@project-tracking.com", EmailConfirmed = true, FirstName = "Ted", LastName = "Mosby", NormalizedEmail = "TED@PROJECT-TRACKING.COM", NormalizedUserName = "TED", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEHaaS8H6lU5uCni4ZUx8jaU6CcMcxl7lI5DHF2G9cz4WTNXEh0mHhVxNlR6OfxUpqg==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 854, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Software Engineer", UserName = "ted" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e578", ConcurrencyStamp = "fe93161c-8ff6-4181-be46-fdfec02638e8", DateOfBirth = new DateTime(1996, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "marshall@project-tracking.com", EmailConfirmed = true, FirstName = "Marshall", LastName = "Eriksen", NormalizedEmail = "MARSHALL@PROJECT-TRACKING.COM", NormalizedUserName = "MARSHALL", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAECorA2uu4dCjEkJpFaAMA8VEJgEFnVjxPCp+THElGby9SGvawhOdjlAjyAGpdGaGuQ==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 863, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Jr. Developer", UserName = "marshall" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e579", ConcurrencyStamp = "08a88386-9600-4390-89e1-ab7ae4a99c59", DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "lilly@project-tracking.com", EmailConfirmed = true, FirstName = "Lilly", LastName = "Aldrin", NormalizedEmail = "LILLY@PROJECT-TRACKING.COM", NormalizedUserName = "LILLY", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEPSCXyKr1Qr+zvnqNgOLY8CiLJe1uy0bRZ4F1IXHZaj/6jI3tOJuNLEHd/JHcJLvww==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 870, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Dev Leader", UserName = "lilly" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e580", ConcurrencyStamp = "fb47fe18-c462-4b3c-8877-6f345b30de2c", DateOfBirth = new DateTime(1996, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "robin@project-tracking.com", EmailConfirmed = true, FirstName = "Robin", LastName = "Scherbatsky", NormalizedEmail = "ROBIN@PROJECT-TRACKING.COM", NormalizedUserName = "ROBIN", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAENrFS/vIr1bR1k7NjaYTrqeeRuHC54y8NT9zsIqLkuotBokSRji1fgOCir0uL/PGaQ==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 12, 1, 15, 877, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Graphic Designer", UserName = "robin" }
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

                    b.Property<bool>("IsRead");

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

                    b.Property<bool>("IsRead");

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

                    b.Property<string>("AssignedByUserId");

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
