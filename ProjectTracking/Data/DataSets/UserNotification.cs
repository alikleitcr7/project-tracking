using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Data.DataSets
{
    public class UserNotification : Notification
    {
        [Required]
        public string FromUserId { get; set; }
        [Required]
        public string ToUserId { get; set; }

        public int? TimeSheetId { get; set; }

        public TimeSheet TimeSheet { get; set; }

        public ApplicationUser FromUser { get; set; }
        public ApplicationUser ToUser { get; set; }
    }
}
