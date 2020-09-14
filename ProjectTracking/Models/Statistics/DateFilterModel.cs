using ProjectTracking.DataContract.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Models.Statistics
{
    public class DateFilterModel
    {
        public List<int> Years { get; set; } = new List<int>();
        public List<int> Months { get; set; } = new List<int>();
        public List<int> Days { get; set; } = new List<int>();
    }
}