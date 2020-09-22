using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class Team 
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public ICollection<User> Members { get; set; }
        public ICollection<TeamsProjects> TeamsProjects { get; set; }
    }
}