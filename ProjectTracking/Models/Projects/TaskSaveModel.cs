using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Models.Projects
{
    public class TaskSaveModel
    {
        // project is required for a task
        public int projectId { get; set; }

        public int? id { get; set; }

        [Required]
        public string title { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public short statusCode { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? plannedEnd { get; set; }
        public DateTime? actualEnd { get; set; }
        public List<int> teamsIds { get; set; }

        private string statusByUserId;

        public string GetStatusByUserId()
        {
            return statusByUserId;
        }

        public void SetStatusByUserId(string userId)
        {
            statusByUserId = userId;
        }
    }
}
