namespace ProjectTracking.DataContract
{
    public class Broadcast : Notification
    {
        public int ToTeamId { get; set; }
        public Team ToTeam { get; set; }

        public string ToTeamDisplay
        {
            get
            {
                return ToTeam == null ? "-" : ToTeam.Name;
            }
        }
    }
}