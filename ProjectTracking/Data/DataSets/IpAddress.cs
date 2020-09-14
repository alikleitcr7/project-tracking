using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class IpAddress : Entity
    {
        public string Address { get; set; }
        public string Title { get; set; }
    }
}
