using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class Holiday : Entity
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
