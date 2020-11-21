using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.DataContract.Hubs
{
    public class ObservedUser
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }

        List<string> ConnectionIds = new List<string>();

        public void AddConnection(string id)
        {
            if (!HasConnection(id))
            {
                ConnectionIds.Add(id);
            }
        }

        public void RemoveConnection(string id)
        {
            if (HasConnection(id))
            {
                ConnectionIds.Remove(id);
            }
        }

        public bool HasConnection(string id)
        {
            return ConnectionIds.Contains(id);
        }

        public List<string> GetConnections()
        {
            return  ConnectionIds;
        }

        public bool IsActive => ConnectionIds.Count > 0;
    }
}
