using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.ViewModel
{
    class RoomDetail_VM
    {
        public Room SelectedRoom {
            get { return Storage.Instance.SelectedRoom; }
        }
    }
}
