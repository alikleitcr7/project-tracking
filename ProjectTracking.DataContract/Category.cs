using ProjectTracking.DataContract.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.DataContract
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<User> Employees { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}