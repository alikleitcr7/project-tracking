﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.Data.DataSets;

namespace ProjectTracking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        //public DbSet<Permission> Permissions { get; set; }
        public DbSet<Superviser> Supervisers { get; set; }
        public DbSet<Project> Projects { get; set; }
        //public DbSet<RequestedPermission> RequestedPermissions { get; set; }
        //public DbSet<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetActivity> TimeSheetActivities { get; set; }
        public DbSet<TimeSheetStatus> TimeSheetStatuses { get; set; }
        public DbSet<TimeSheetProject> TimeSheetProjects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<IdentityRole> ApplicationRoles { get; set; }
        public DbSet<UserLog> UserLogging { get; set; }

        public DbSet<ProjectReference> ProjectReference { get; set; }
        //public DbSet<InventoryType> InventoryType { get; set; }
        //public DbSet<InventoryStatus> InventoryStatus { get; set; }
        //public DbSet<Country> Country { get; set; }
        //public DbSet<UpdateFrequency> UpdateFrequency { get; set; }
        //public DbSet<PublishingChannel> PublishingChannel { get; set; }
        //public DbSet<InventoryProject> InventoryProject { get; set; }

        public DbSet<TypeOfWork> TypeOfWork { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnit { get; set; }
        public DbSet<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        //public DbSet<Holiday> Holidays { get; set; }

        private readonly IConfiguration _config;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Team

            builder.Entity<Team>()
                   .HasMany(c => c.Employees)
                   .WithOne(c => c.Team)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Team>()
                  .HasMany(c => c.Projects)
                  .WithOne(c => c.Team)
                  .OnDelete(DeleteBehavior.SetNull);

            #endregion

            #region Category

            builder.Entity<Category>()
                .HasMany(c => c.Projects)
                .WithOne(c => c.Category)
                .OnDelete(DeleteBehavior.SetNull);

            #endregion

            #region Project
            builder.Entity<Project>()
.HasOne(c => c.Team).WithMany(c => c.Projects)
   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Project>()
           .HasOne(c => c.Category)
           .WithMany(c => c.Projects)
         .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region TimeSheetActivity

            builder.Entity<TimeSheetActivity>()
                  .HasMany(c => c.TimeSheetActivityLogs)
                  .WithOne(c => c.TimeSheetActivity)
                  .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Supervisor

            builder.Entity<Superviser>()
                   .HasKey(k => new { k.UserId, k.SupervisorId });

            builder.Entity<Superviser>()
                   .HasOne(x => x.Supervisor)
                   .WithMany(m => m.Supervising)
                   .HasForeignKey(x => x.SupervisorId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region IdentityUserRole

            builder.Entity<IdentityUserRole<string>>()
                   .HasKey(p => new { p.UserId, p.RoleId });

            #endregion

            #region Commented
            //builder.Entity<RequestedPermissionsStatus>().HasKey(k => new { k.SuperviserId, k.RequestedPermissionId });
            //builder.Entity<TimeSheetProject>().HasKey(k => new { k.TimeSheetId, k.ProjectId });
            //builder.Entity<InventoryProjectPublishingChannel>().HasKey(k => new { k.InventoryProjectId, k.PublishingChannelId });
            //builder.Entity<InventoryProjectSubProjects>().HasKey(k => new { k.InventoryProjectId, k.InventorySubProjectId });

            //builder.Entity<TimeSheetActivityLog>()
            //      .HasOne(c => c.TimeSheetActivity)
            //      .WithMany(k => k.TimeSheetActivityLogs)
            //      .OnDelete(DeleteBehavior.Cascade);


            //builder.Entity<Category>()
            //       .HasMany(c => c.Employees)
            //       ////.WithOne(c => c.Category)
            //       .OnDelete(DeleteBehavior.SetNull);

            //builder.Entity<RequestedPermission>()
            //       .HasMany(c => c.RequestedPermissionsStatuses)
            //       .WithOne(k => k.RequestedPermission)
            //       .HasForeignKey(k => k.RequestedPermissionId)
            //       .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<RequestedPermission>().HasKey(sc => new { sc.ApplicationUserId, sc.PermissionId });
            //builder.Entity<RequestedPermissionsStatus>().HasKey(sc => new { sc.SuperviserId, sc.RequestedPermissionId });

            //InventoryType,InventoryStatus,Country,UpdateFrequency

            // builder.Entity<InventoryType>()
            //   .HasMany(c => c.InventoryProjects)
            //   .WithOne(c => c.InventoryType)
            //   .OnDelete(DeleteBehavior.SetNull);

            // builder.Entity<InventoryStatus>()
            //.HasMany(c => c.InventoryProjects)
            //.WithOne(c => c.InventoryStatus)
            //.OnDelete(DeleteBehavior.SetNull);

            //            builder.Entity<Country>()
            //           .HasMany(c => c.InventoryProjects)
            //           .WithOne(c => c.Country)
            //           .OnDelete(DeleteBehavior.SetNull);


            //            builder.Entity<UpdateFrequency>()
            //           .HasMany(c => c.InventoryProjects)
            //           .WithOne(c => c.UpdateFrequency)
            //           .OnDelete(DeleteBehavior.SetNull);


            //            //PublishingChannel,InventorySubProject


            //            builder.Entity<PublishingChannel>()
            //           .HasMany(c => c.InventoryProjectPublishingChannels)
            //           .WithOne(c => c.PublishingChannel)
            //           .OnDelete(DeleteBehavior.Cascade);


            //            builder.Entity<InventorySubProject>()
            //           .HasMany(c => c.InventoryProjectSubProjects)
            //           .WithOne(c => c.InventorySubProject)
            //           .OnDelete(DeleteBehavior.Cascade);

            //            builder.Entity<RequestedPermissionsStatus>()
            //.HasOne(sc => sc.Superviser)
            //.WithMany(s => s.RequestedPermissionsStatuses)
            //.HasForeignKey(sc => sc.SuperviserId);


            //            builder.Entity<RequestedPermissionsStatus>()
            //                .HasOne(sc => sc.RequestedPermission)
            //                .WithMany(s => s.RequestedPermissionsStatuses)
            //                .HasForeignKey(sc => sc.RequestedPermissionId);


            //            //builder.Entity<RequestedPermissionsStatus>()
            //            //       .HasOne(c => c.RequestedPermission);
            //            //builder.Entity<RequestedPermissionsStatus>()
            //            //       .HasOne(c => c.Superviser);
            //            builder.Entity<RequestedPermission>()
            //    .HasOne(sc => sc.ApplicationUser)
            //    .WithMany(s => s.RequestedPermissions)
            //    .HasForeignKey(sc => sc.ApplicationUserId);


            //            builder.Entity<RequestedPermission>()
            //                .HasOne(sc => sc.Permission)
            //                .WithMany(s => s.RequestedPermissions)
            //                .HasForeignKey(sc => sc.PermissionId);

            #endregion

            base.OnModelCreating(builder);

            #region Initial Users

            string ADMIN_ID = _config.GetValue<string>("Tokens:SysUsers:Admin");
            string ADMIN_ROLE_ID = _config.GetValue<string>("Tokens:Roles:Admin");
            string USER_ROLE_ID = _config.GetValue<string>("Tokens:Roles:User");
            //const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Admin",
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@sys.com",
                NormalizedEmail = "admin@sys.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123123"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            #endregion
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Student>()
        //    //    .HasOptional<Standard>(s => s.Standard)
        //    //    .WithMany()
        //    //    .WillCascadeOnDelete(false);
        //}
    }
}
