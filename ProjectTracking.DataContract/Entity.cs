using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectTracking.DataContract
{
    public interface IEntity
    {
        int ID { get; set; }
    }
    public class Entity : IEntity
    {
        [Column("ID", Order = 0)]
        public int ID { get; set; }
    }
}
