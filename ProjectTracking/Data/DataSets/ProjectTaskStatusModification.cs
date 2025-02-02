﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTracking.Data.DataSets
{
    public class ProjectTaskStatusModification
    {
        [Column(Order = 0)]
        public int ProjectTaskId { get; set; }
        [Column(Order = 1)]
        public DateTime DateModified { get; set; }
        [Column(Order = 2)]
        public short StatusCode { get; set; }
        [Required]
        public string ModifiedByUserId { get; set; }

        public ApplicationUser ModifiedByUser { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }

}