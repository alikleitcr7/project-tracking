using System;
using System.Threading.Tasks;

namespace ProjectTracking.Hubs
{
    public interface IBroadcastsHub
    {
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception exception);
    }
}