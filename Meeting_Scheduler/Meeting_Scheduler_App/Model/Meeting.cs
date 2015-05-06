using System;
using Meeting_Scheduler.EF;

namespace Meeting_Scheduler_App.Model
{
    public partial class Meeting
    {
       
        public int Meeting_Id { get; set; }

       
        public string Name { get; set; }

       
        public string Description { get; set; }

        public int Room_Id { get; set; }

        public DateTime? Date { get; set; }

        public int? Duration { get; set; }

        public virtual Room Room { get; set; }
    }
}
