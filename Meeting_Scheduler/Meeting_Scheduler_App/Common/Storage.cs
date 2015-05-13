using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.Common
{
    public class Storage
    {
        private static Storage _instance;

        private Storage()
        {
            Date = DateTimeOffset.Now.Date;
        }

        public static Storage Instance
        {
            get { return _instance ?? (_instance = new Storage()); }
        }

        public Meeting SelectedMeeting { get; set; }
        public Room SelectedRoom { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
