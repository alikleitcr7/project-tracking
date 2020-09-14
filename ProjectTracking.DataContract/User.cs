using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectTracking.DataContract
{

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsTracked { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
        public DateTime DateOfBirth { get; set; }
        public Team Department { get; set; }
        public string DepartmentDisplay
        {
            get
            {
                return Department == null ? "-" : Department.Name;
            }
        }
        public Category Company { get; set; }
        public string CompanyDisplay
        {
            get
            {
                return Company == null ? "-" : Company.Name;
            }
        }
        public List<TimeSheet> TimeSheets { get; set; }
        //public List<RequestedPermission> RequestedPermissions { get; set; }
        public List<User> Supervisors { get; set; }
        public List<User> Supervising { get; set; }
        public int MonthlySalary { get; set; }
        public int HourlyRate { get; set; }
        public int HoursPerDay { get; set; }
        public EmploymentType? AgreementType { get; set; }
    }

    public enum EmploymentType
    {
        FullTime, PartTime, Contractor
    }

}