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
    [Migration("20201201175226_username limit2")]
    partial class usernamelimit2
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
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575", ConcurrencyStamp = "5eb287da-e9ed-4898-bbc9-c6895a2bcf65", DateOfBirth = new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "admin@sys.com", EmailConfirmed = true, FirstName = "Sys", LastName = "Admin", NormalizedEmail = "ADMIN@SYS.COM", NormalizedUserName = "ADMIN", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAENuJSkN073VylMa1eIlMe0HmONspslDLSJZOcY5St3C4mTqQSNKVbUpdKV9Z+VIJ4g==", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 671, DateTimeKind.Local), RoleCode = (short)2, SecurityStamp = "", Title = "Admin", UserName = "admin" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e576", ConcurrencyStamp = "817b15ac-240e-4be6-95a3-5faf1d5ecf81", DateOfBirth = new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "alikleitcr7@gmail.com", EmailConfirmed = true, FirstName = "Ali", LastName = "Kleit", NormalizedEmail = "ALIKLEITCR7@GMAIL.COM", NormalizedUserName = "ALIKLEIT", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEEG9q1fTqb03iAxThdu21B26GlI5iVQXPDO2Sc1/bCezK/WYJQtADFz+SQuMf/PXIw==", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 680, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Developer", UserName = "alikleit" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e581", ConcurrencyStamp = "a8f79eb6-4066-411f-a43c-0a7b8d22350f", DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "mark@project-tracking.com", EmailConfirmed = true, FirstName = "Mark", LastName = "Goldman", NormalizedEmail = "MARK@PROJECT-TRACKING.COM", NormalizedUserName = "MARK", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEBDaugCq11rFSmmdwKWE/xs7l2DT5b/mbDT7ygznLdJpIYhCxbAQ0jQkn3ltD97WIA==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 688, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Head IT", UserName = "mark" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e582", ConcurrencyStamp = "e1cb79ec-74c1-4047-a3b6-2eac36971e41", DateOfBirth = new DateTime(1996, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ashton@project-tracking.com", EmailConfirmed = true, FirstName = "Ashton", LastName = "Kutcher", NormalizedEmail = "ASHTON@PROJECT-TRACKING.COM", NormalizedUserName = "ASHTON", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEA8j2vYdK0g5sfGw8HunuWoc/wXqweWNC7Ct0P72s2l9tZAO2/bGiZae9Kd9lxWYZw==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 695, DateTimeKind.Local), RoleCode = (short)1, SecurityStamp = "", Title = "Sr. Designer", UserName = "ashton" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e577", ConcurrencyStamp = "50f9a24a-8649-485c-8390-75004f6f08a2", DateOfBirth = new DateTime(1996, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "ted@project-tracking.com", EmailConfirmed = true, FirstName = "Ted", LastName = "Mosby", NormalizedEmail = "TED@PROJECT-TRACKING.COM", NormalizedUserName = "TED", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEI5ziA3zOQbpMsPcMFy8RgIRGwhKzpDwBdc+xt8u8goxPP7hw4WxSNqyqQKoB3erNw==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 703, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Software Engineer", UserName = "ted" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e578", ConcurrencyStamp = "acdccc32-16a4-4b41-a5f4-875bf15bad62", DateOfBirth = new DateTime(1996, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "marshall@project-tracking.com", EmailConfirmed = true, FirstName = "Marshall", LastName = "Eriksen", NormalizedEmail = "MARSHALL@PROJECT-TRACKING.COM", NormalizedUserName = "MARSHALL", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEKyQQkVyUStiA7KGIRycrc9P2mnBU4V0aupQoddumbbtuAS5i4T2cpFGhdwZ2MeG0Q==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 711, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Jr. Developer", UserName = "marshall" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e579", ConcurrencyStamp = "fb6d52df-efef-4ae1-9700-c1ee18ec4bed", DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "lilly@project-tracking.com", EmailConfirmed = true, FirstName = "Lilly", LastName = "Aldrin", NormalizedEmail = "LILLY@PROJECT-TRACKING.COM", NormalizedUserName = "LILLY", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEAk/EeEZxuCiAxr8eOsEtSuDji7bFfan8mEsk9uB/GHUYwQBmNqM7Lm1eLxgNi5fhg==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 719, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Dev Leader", UserName = "lilly" },
                        new { Id = "a18be9c0-aa65-4af8-bd17-00bd9344e580", ConcurrencyStamp = "bae2b5fa-00a0-40f6-9db8-16de22a0e71f", DateOfBirth = new DateTime(1996, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "robin@project-tracking.com", EmailConfirmed = true, FirstName = "Robin", LastName = "Scherbatsky", NormalizedEmail = "ROBIN@PROJECT-TRACKING.COM", NormalizedUserName = "ROBIN", NotificationFlag = false, PasswordHash = "AQAAAAEAACcQAAAAEEgcyqeSeZMnGx1Z8YuSsDEP8BPvmq8SKyv7PkAG6WFGJMZrk1uIBnDSRZ48XuzAuQ==", RoleAssignedByUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575", RoleAssignedDate = new DateTime(2020, 12, 1, 19, 52, 25, 727, DateTimeKind.Local), RoleCode = (short)0, SecurityStamp = "", Title = "Graphic Designer", UserName = "robin" }
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
