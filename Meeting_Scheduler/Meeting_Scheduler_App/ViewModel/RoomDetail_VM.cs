using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.ViewModel
{
    internal class RoomDetail_VM
    {
        public Room SelectedRoom
        {
            get { return Storage.Instance.SelectedRoom; }
        }
    }
}