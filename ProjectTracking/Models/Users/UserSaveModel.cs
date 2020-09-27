using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Users
{
    public class UserSaveModel
    {
        public string id { get; set; }

        [Required(ErrorMessage = "email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "not an email")]
        [EmailAddress(ErrorMessage = "not an email")]
        public string email { get; set; }

        [Required(ErrorMessage = "first name is requierd")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "last name is requierd")]
        public string lastName { get; set; }

        public string middleName { get; set; }

        [Required(ErrorMessage = "title name is requierd")]
        public string title { get; set; }

        public float? monthlySalary { get; set; }
        public float? hourlyRate { get; set; }
        public float? hoursPerDay { get; set; }
        public short? employmentTypeCode { get; set; }
    }
}
