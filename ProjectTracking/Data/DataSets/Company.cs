using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ProjectTracking.Data.DataSets
{
    public class Category
    {
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30), MinLength(2)]
        public string Name { get; set; }

        //public  ICollection<ApplicationUser> Employees { get; set; }
        public  ICollection<Project> Projects { get; set; }

    }
}
