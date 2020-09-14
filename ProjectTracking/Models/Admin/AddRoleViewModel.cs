using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Models.Admin
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}