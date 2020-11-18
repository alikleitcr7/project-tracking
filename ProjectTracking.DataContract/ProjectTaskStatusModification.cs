using System;


namespace ProjectTracking.DataContract
{
    public class ProjectTaskStatusModification
    {
        public int ProjectTaskId { get; set; }
        public short StatusCode { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedByUserId { get; set; }

        public string ModifiedByUserName { get; set; }

        public string DateModifiedDisplay => DateModified.ToString("dd-MM-yyyy HH:mm");

        public string StatusDisplay
        {
            get
            {
                return ((ProjectTaskStatus)StatusCode).ToString();
            }
        }


        public ProjectTask ProjectTask { get; set; }
    }

}