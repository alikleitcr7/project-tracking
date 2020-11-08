using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface INotificationMethods
    {
        List<UserNotification> GetFromUser(string fromUserId, int page, int countPerPage, out int totalCount);
        List<UserNotification> GetToUser(string toUserId, int page, int countPerPage, out int totalCount);
        Task<UserNotification> Send(string fromUserId, string toUserId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false, int? timesheetId = null, int? projectId = null);
        Task<Broadcast> SendToTeam(string fromUserId, int toTeamId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false);
    }
}