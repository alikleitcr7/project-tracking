using System;
using System.Threading.Tasks;

namespace ProjectTracking.Hubs
{
    public interface INotificationsHub
    {
        Task SendNotificationToClient(string message);
    }
}