using ProjectTracking.DataContract.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.DataContract
{
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "name is required"), MaxLength(30), MinLength(2)]
        public string Name { get; set; }

        //public virtual List<User> Employees { get; set; }
        public virtual List<Project> Projects { get; set; }

        public int ProjectsCount { get; set; }
    }
}