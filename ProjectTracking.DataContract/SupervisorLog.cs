using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class SupervisorLog : Entity
    {
        public string UserId { get; set; }
        public string AssignedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime DateAssigned { get; set; }
        public User User { get; set; }
        public User AssignedByUser { get; set; }
        public Team Team { get; set; }


        public string DateAssignedDisplay
        {
            get
            {
                return DateAssigned.ToString("dd-MM-yyyy");
            }
        }
    }
}
