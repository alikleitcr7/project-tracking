using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.Data.DataSets;

namespace ProjectTracking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationIdentityRole, string>
    {
        public DbSet<Superviser> Supervisers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TeamsProjects> TeamsProjects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetActivity> TimeSheetActivities { get; set; }
        public DbSet<TimeSheetTask> TimeSheetTasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Team> Teams { get; set; }
        //public DbSet<ApplicationIdentityRole> ApplicationRoles { get; set; }
        public DbSet<UserLog> UserLogging { get; set; }
        public DbSet<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<ProjectStatusModification> ProjectStatusModifications { get; set; }
        public DbSet<ProjectTaskStatusModification> ProjectTaskStatusModifications { get; set; }

        private readonly IConfiguration _config;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationIdentityRole>().ToTable("Roles").HasKey(k=>k.Id);
            builder.Entity<ApplicationUser>().ToTable("Users").HasKey(k => k.Id);

            #region Team

            builder.Entity<Team>()
                   .HasMany(c => c.Members)
                   .WithOne(c => c.Team)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Team>()
                  .HasMany(c => c.TeamsProjects)
                  .WithOne(c => c.Team)
                  .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Category

            builder.Entity<Category>()
                .HasMany(c => c.Projects)
                .WithOne(c => c.Category)
                .OnDelete(DeleteBehavior.SetNull);

            #endregion

            #region Project

            builder.Entity<Project>()
                .HasMany(c => c.TeamsProjects)
                .WithOne(c => c.Project)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Project>()
                   .HasOne(c => c.Category)
                   .WithMany(c => c.Projects)
                   .OnDelete(DeleteBehavior.SetNull);

            #endregion


            #region TimeSheet
            builder.Entity<TimeSheet>()
              .HasMany(c => c.TimeSheetTasks)
              .WithOne(c => c.TimeSheet)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TimeSheet>()
                  .HasOne(c => c.User)
                  .WithMany(c => c.TimeSheets)
                  .HasForeignKey(k => k.UserId);

            builder.Entity<TimeSheet>()
                  .HasOne(c => c.AddedByUser)
                  .WithMany(c => c.AddedByUserTimeSheets)
                  .HasForeignKey(k => k.AddedByUserId);

            #endregion

            #region TimeSheetActivity

            builder.Entity<TimeSheetTask>()
                  .HasMany(c => c.Activities)
                  .WithOne(c => c.TimeSheetTask)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TimeSheetActivity>()
                  .HasMany(c => c.TimeSheetActivityLogs)
                  .WithOne(c => c.TimeSheetActivity)
                  .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<TimeSheetActivity>()
            //      .HasMany(c => c.TimeSheetActivityLogs)
            //      .WithOne(c => c.TimeSheetActivity)
            //      .OnDelete(DeleteBehavior.Cascade);

            #endregion

            builder.Entity<TeamsProjects>()
                   .HasKey(k => new { k.ProjectId, k.TeamId });


            builder.Entity<TeamsProjects>()
                   .HasOne(k => k.Project)
                   .WithMany(k => k.TeamsProjects)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TeamsProjects>()
                   .HasOne(k => k.Team)
                   .WithMany(k => k.TeamsProjects)
                   .OnDelete(DeleteBehavior.Cascade);

            #region Broadcast

            builder.Entity<Broadcast>()
                   .HasOne(k => k.FromUser)
                   .WithMany(k => k.Broadcasts)
                   .HasForeignKey(k => k.FromUserId);

            #endregion


            builder.Entity<ProjectStatusModification>()
                   .HasKey(k => new { k.ProjectId, k.DateModified });

            builder.Entity<ProjectStatusModification>()
                   .HasOne(k => k.Project)
                   .WithMany(k => k.ProjectStatusModifications)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectTaskStatusModification>()
                   .HasKey(k => new { k.ProjectTaskId, k.DateModified });

            builder.Entity<ProjectTaskStatusModification>()
                   .HasOne(k => k.ProjectTask)
                   .WithMany(k => k.ProjectTaskStatusModifications)
                   .OnDelete(DeleteBehavior.Cascade);

            #region Supervisor

            builder.Entity<Superviser>()
                   .HasKey(k => new { k.TeamId, k.UserId });

            builder.Entity<Superviser>()
                   .HasOne(x => x.User)
                   .WithMany(m => m.Supervising)
                   .HasForeignKey(x => x.UserId);

            builder.Entity<Superviser>()
                   .HasOne(x => x.AssignedByUser)
                   .WithMany(m => m.AssignedSupervisors)
                   .HasForeignKey(x => x.AssignedByUserId);

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

            #region IPAddress

            builder.Entity<IpAddress>().HasKey(k => k.Address);

            builder.Entity<IpAddress>()
                   .HasMany(k => k.TimeSheetActivities)
                   .WithOne(k => k.IpAddress)
                   .HasForeignKey(k => k.Address)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<IpAddress>()
                   .HasMany(k => k.TimeSheetActivityLogs)
                   .WithOne(k => k.IpAddress)
                   .HasForeignKey(k => k.Address)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<IpAddress>()
                   .HasMany(k => k.UserLogs)
                   .WithOne(k => k.IpAddress)
                   .HasForeignKey(k => k.Address)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            #endregion

            base.OnModelCreating(builder);

            #region Initial Users

            string ADMIN_ID = _config.GetValue<string>("Tokens:SysUsers:Admin");
            string DEV_ID = _config.GetValue<string>("Tokens:SysUsers:Dev");

            string ADMIN_ROLE_ID = _config.GetValue<string>("Tokens:Roles:Admin");
            string USER_ROLE_ID = _config.GetValue<string>("Tokens:Roles:User");
            //const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one

            builder.Entity<ApplicationIdentityRole>().HasData(new ApplicationIdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<ApplicationIdentityRole>().HasData(new ApplicationIdentityRole
            {
                Id = USER_ROLE_ID,
                Name = "User",
                NormalizedName = "USER",
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                FirstName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@sys.com",
                NormalizedEmail = "ADMIN@SYS.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123123"),
                SecurityStamp = string.Empty,
                Title = "Admin",
                RoleId = ADMIN_ROLE_ID
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = DEV_ID,
                UserName = "alikleit",
                FirstName = "Ali",
                LastName = "Kleit",
                NormalizedUserName = "ALIKLEIT",
                Email = "alikleitcr7@gmail.com",
                NormalizedEmail = "ALIKLEITCR7@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123123"),
                SecurityStamp = string.Empty,
                Title = "Developer",
                RoleId = USER_ROLE_ID
            });

            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    UserId = ADMIN_ID,
            //    RoleId = ADMIN_ROLE_ID,
            //});


            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    UserId = DEV_ID,
            //    RoleId = USER_ROLE_ID,
            //});

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
