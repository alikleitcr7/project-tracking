using System;
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

        [Required(ErrorMessage = "first name is requierd"), MaxLength(30,ErrorMessage ="first name should not have more than 30 characters")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "last name is requierd"), MaxLength(30, ErrorMessage = "last name should not have more than 30 characters")]
        public string lastName { get; set; }

        [MaxLength(60)]
        public string title { get; set; }

        public short? employmentTypeCode { get; set; }

        [RegularExpression(@"^(?=.{3,30}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Username should be between 3 to 30 characters and can contain letters from a-z and numbers 0-9 and can contain (.-_) but not in a row and not in the beginning or end")]
        public string userName { get; set; }

        public DateTime dateOfBirth { get; set; }
    }
}
