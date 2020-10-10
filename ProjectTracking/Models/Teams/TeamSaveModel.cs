using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Teams
{
    public class TeamSaveModel
    {
        public int? id { get; set; }
        public string name { get; set; }

        private string addedByUserId { get; set; }
        private string supervisorId { get; set; }
        private string assignedByUserId { get; set; }


        public string GetAddedByUserId()
        {
            return addedByUserId;
        }

        public string GetSupervisorId()
        {
            return supervisorId;
        }

        public string GetAssignedByUserId()
        {
            return assignedByUserId;
        }


        public void SetAddedByUserId(string userId)
        {
            addedByUserId = userId;
        }

        public void SetSupervisorId(string userId)
        {
            supervisorId = userId;
        }

        public void SetAssignedByUserId(string userId)
        {
            assignedByUserId = userId;
        }


        public List<string> userIds { get; set; }
    }
}
