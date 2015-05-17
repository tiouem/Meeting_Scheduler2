using System.Collections.Generic;
using System.Windows.Input;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.View;

namespace Meeting_Scheduler_App.Model
{
    public class V_Room
    {
        private readonly Room _room;

        public V_Room(Room room)
        {
            _room = room;
        }

        public List<V_ScheduleBlock> Blocks { get; set; }
        public string Name { get; set; }

        public ICommand RoomClick
        {
            get { return new Command(_roomClick); }
        }

        private void _roomClick()
        {
            var nav = new NavigationService();
            var stor = Storage.Instance;
            stor.SelectedRoom = _room;
            nav.Navigate(typeof (RoomDetail));
        }
    }
}