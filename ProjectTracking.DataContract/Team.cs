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
        public string AddedByUserId { get; set; }
        public DateTime DateAdded { get; set; }

        public string SupervisorId { get; set; }
        public string AssignedByUserId { get; set; }
        public DateTime DateAssigned { get; set; }

        public int? MembersCount { get; set; }

        public List<User> Members { get; set; }
        public List<TeamsProjects> TeamsProjects { get; set; }
    }
}