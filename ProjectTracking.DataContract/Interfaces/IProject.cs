using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract.Interfaces
{
    public interface IProject
    {
        int ID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime DateAdded { get; set; }

    }

}