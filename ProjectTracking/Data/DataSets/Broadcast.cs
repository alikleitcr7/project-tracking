namespace ProjectTracking.Data.DataSets
{
    public class Broadcast : Notification
    {
        public string FromUserId { get; set; }
        public int ToTeamId { get; set; }

        public ApplicationUser FromUser { get; set; }
        public Team ToTeam { get; set; }
    }
}
