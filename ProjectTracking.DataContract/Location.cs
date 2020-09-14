using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}