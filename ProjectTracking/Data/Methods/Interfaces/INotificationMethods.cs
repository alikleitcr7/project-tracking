using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface INotificationMethods
    {
        List<Notification> GetFromUser(string fromUserId, int page, int countPerPage, out int totalCount);
        List<Notification> GetToUser(string toUserId, int page, int countPerPage, out int totalCount);
        Task<Notification> Send(string fromUserId, string toUserId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false);
    }
}