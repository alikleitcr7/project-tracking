using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
  public  class SuperVisorModel
    {
        public List<User> All { get; set; }
        public string[] SuperVise { get; set; }
    }
}
