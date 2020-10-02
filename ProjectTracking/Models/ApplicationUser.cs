using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using ProjectTracking.Data.DataSets;

namespace ProjectTracking
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
        //public int? CompanyID { get; set; }
        public bool? IsTracked { get; set; }
        public float? MonthlySalary { get; set; }
        public float? HourlyRate { get; set; }
        //public float? HoursPerDay { get; set; } // to be removed
        public short? EmploymentTypeCode { get; set; }
        //public bool IsDeleted { get; set; }

        //public virtual List<Superviser> Supervisors { get; set; }
        public List<Superviser> Supervising { get; set; }
        public List<Broadcast> Broadcasts { get; set; }

        //public ICollection<Superviser> Supervisors { get; set; }
        //public ICollection<RequestedPermission> RequestedPermissions { get; set; }
        public ICollection<TimeSheet> TimeSheets { get; set; }
        public ICollection<TimeSheet> AddedByUserTimeSheets { get; set; }
        public List<Project> Projects { get; set; }
        //public Role Role { get; set; }
        //public ICollection<IdentityUserRole<string>> Roles { get; set; }
        //public ICollection<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
        //public virtual List<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
    }
}
