using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class UserRoleLog
    {
        public string UserId { get; set; }
        public DateTime DateAssigned { get; set; }

        public string AssignedByUserId { get; set; }
        public short RoleCode { get; set; }

        public User User { get; set; }
        public User AssignedByUser { get; set; }

        public ApplicationUserRole Role => (ApplicationUserRole)RoleCode;
        public string RoleDisplay => Role.ToString();

        public string DateAssignedDisplay
        {
            get
            {
                return DateAssigned.ToString("dd-MM-yyyy");
            }
        }
    }
}
