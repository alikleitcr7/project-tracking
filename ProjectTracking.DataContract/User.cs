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
        public string Title { get; set; }
        //public string Role { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
        public DateTime DateOfBirth { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public string TeamDisplay
        {
            get
            {
                return Team == null ? "-" : Team.Name;
            }
        }
        public string DateOfBirthDisplay
        {
            get
            {
                return DateOfBirth.ToString("dd/MM/yyyy");
            }
        }
        //public Category Company { get; set; }
        //public string CompanyDisplay
        //{
        //    get
        //    {
        //        return Company == null ? "-" : Company.Name;
        //    }
        //}
        public List<TimeSheet> TimeSheets { get; set; }
        //public List<RequestedPermission> RequestedPermissions { get; set; }
        //public List<Supervising> Supervisors { get; set; }
        //public List<User> Supervising { get; set; }

        public float? MonthlySalary { get; set; }
        public float? HourlyRate { get; set; }
        public float? HoursPerDay { get; set; }
        public short? EmploymentTypeCode { get; set; }

        public EmploymentType? EmploymentType
        {
            get
            {
                return EmploymentTypeCode.HasValue ? (EmploymentType?)EmploymentTypeCode.Value : null;
            }
        }

        public string EmploymentTypeDisplay
        {
            get
            {
                return EmploymentType.HasValue ? EmploymentType.Value.ToString() : null;
            }
        }

        public string ProfileTwoLetters
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(FirstName))
                {
                    return null;
                }

                return FirstName[0] + " " + LastName[0];
            }
        }

        public int? SupervisingCount { get; set; }
    }

    public enum EmploymentType
    {
        FullTime, PartTime, Contractor
    }

}