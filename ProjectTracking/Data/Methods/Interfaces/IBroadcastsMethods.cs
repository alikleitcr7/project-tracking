using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IBroadcastsMethods
    {
        Broadcast Get(int id);
        List<Broadcast> GetFromUser(string fromUserId, int page, int countPerPage, out int totalCount);
        List<Broadcast> GetToTeam(int toTeamId, int page, int countPerPage, out int totalCount);
        Task<Broadcast> Send(string fromUserId, int toTeamId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false);
    }
}