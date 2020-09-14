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
    }
}
