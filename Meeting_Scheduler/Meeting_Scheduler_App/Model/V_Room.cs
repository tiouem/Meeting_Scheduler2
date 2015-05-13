using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Meeting_Scheduler_App.Common;
using MSchedule.View;
using System.Windows.Input;
using Meeting_Scheduler_App.View;
using MSchedule.View;

namespace Meeting_Scheduler_App.Model
{
    public class V_Room
    {
        private List<V_ScheduleBlock> _blocks;

        public List<V_ScheduleBlock> Blocks
        {
            get { return _blocks; }
            set { _blocks = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Room _room;

        public ICommand RoomClick
        {
            get { return new Command(_roomClick); }
        }

        private void _roomClick()
        {
            var nav = new NavigationService();
            var stor = Storage.Instance;
            stor.SelectedRoom = _room;
            nav.Navigate(typeof(RoomDetail));
        }

        public V_Room(Room room)
        {
            _room = room;
        }
    }
}
