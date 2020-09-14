using ProjectTracking.DataContract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Models.Admin
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the role name")]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public List<ApplicationUser> Users { get; set; }

    }
}