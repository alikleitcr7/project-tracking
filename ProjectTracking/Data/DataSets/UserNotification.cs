namespace ProjectTracking.Data.DataSets
{
    public class UserNotification : Notification
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        public ApplicationUser FromUser { get; set; }
        public ApplicationUser ToUser { get; set; }
    }
}
