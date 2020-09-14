using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.DataContract
{
    public class Holiday : Entity
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public string DateDisplay
        {
            get
            {
                return Date.ToString("MM/dd/yyyy");
            }
        }
    }
}