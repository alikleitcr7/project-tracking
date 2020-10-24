using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Teams
{
    public class TeamKeyValue
    {
        public TeamKeyValue(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
