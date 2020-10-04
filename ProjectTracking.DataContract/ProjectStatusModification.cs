using System;


namespace ProjectTracking.DataContract
{
    public class ProjectStatusModification
    {
        public int ProjectId { get; set; }
        public short StatusCode { get; set; }
        public DateTime DateModified { get; set; }

        public string DateModifiedDisplay => DateModified.ToString("dd-MM-yyyy HH:mm");
        public string StatusDisplay
        {
            get
            {
                return ((ProjectStatus)StatusCode).ToString();
            }
        }

        public Project Project { get; set; }
    }

}