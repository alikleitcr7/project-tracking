using System;
using System.ComponentModel.DataAnnotations;
using ProjectTracking.DataContract;

namespace ProjectTracking.Models.Admin
{
    public class AddUserViewModel
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@".{6,}", ErrorMessage = "Passwords must be at least 6 characters ")]
        public string Password { get; set; }
        [DataType(DataType.Password)]

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int DepartmentID { get; set; }
        [Required]

        public string Role { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please insert a proper Name")]

        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please insert a proper Name")]

        public bool IsTracked { get; set; }

        public string LastName { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        public string MiddleName { get; set; }
        public float? MonthlySalary { get; set; }
        public float? HourlyRate { get; set; }
        
        public float? HoursPerDay { get; set; }
        public EmploymentType? AgreementType { get; set; }
    }
}