namespace ProjectTracking.DataContract
{
    public class UserNotification : Notification
    {
        public string ToUserId { get; set; }
        public User ToUser { get; set; }


        public string ToUserDisplay
        {
            get
            {

                return ToUser == null ? "-" : ToUser.FullName;
            }
        }
    }
}