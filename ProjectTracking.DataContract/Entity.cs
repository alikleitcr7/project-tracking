using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public interface IEntity
    {
        int ID { get; set; }
    }
    public class Entity : IEntity
    {
        public int ID { get; set; }
    }
}
