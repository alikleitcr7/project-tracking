using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using ProjectTracking.Data.DataSets;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(30), Required]
        public string FirstName { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string MiddleName { get; set; }

        [MaxLength(60)]
        public string Title { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string RoleAssignedByUserId { get; set; }
        public DateTime RoleAssignedDate { get; set; }

        public int? TeamId { get; set; }
        public float? MonthlySalary { get; set; }
        public float? HourlyRate { get; set; }
        public short? EmploymentTypeCode { get; set; }

        [Range(0, 2)]
        public short RoleCode { get; set; }

        public Team Team { get; set; }
        public bool NotificationFlag { get; set; }

        //public List<TimeSheetActivity> TimeSheetActivities { get; set; }
        public ApplicationUser RoleAssignedByUser { get; set; }
        public List<SupervisorLog> Supervising { get; set; }
        public List<SupervisorLog> AssignedSupervisors { get; set; }
        public List<Broadcast> Broadcasts { get; set; }
        public List<UserNotification> FromUserNotifications { get; set; }
        public List<UserNotification> ToUserNotifications { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }
        public List<TimeSheet> AddedByUserTimeSheets { get; set; }
        public List<Team> AddedByUserTeams { get; set; }
        public List<Team> SupervisedTeams { get; set; }
        public List<Team> AssignedSupervisorForTeams { get; set; }
        public List<Project> AddedByUserProject { get; set; }
        public List<ProjectTask> ProjectTaskStatusByUser { get; set; }
        public List<UserRoleLog> UserRoleLogs { get; set; }
        public List<UserRoleLog> AssignedUserRoleLogs { get; set; }
        public List<ProjectTaskStatusModification> ProjectTaskStatusModificationByUser { get; set; }
        //public ApplicationIdentityRole Role { get; set; }

        //public Role Role { get; set; }
        //public ICollection<IdentityUserRole<string>> Roles { get; set; }
        //public ICollection<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
        //public virtual List<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
    }

    public class ApplicationIdentityRole : IdentityRole<string>
    {
        public List<ApplicationUser> ApplicationUser { get; set; }
    }

}
