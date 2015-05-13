using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;
using MSchedule.View;

namespace Meeting_Scheduler_App.ViewModel
{
    class MeetingDetail_VM
    {
        public Meeting Meeting { get; set; }

        public string Date
        {
            get { return Meeting.Date.Value.ToString("dddd dd. MMMM yyyy H:mm - ") + Meeting.Date.Value.AddMinutes((double)Meeting.Duration.Value).ToString("H:mm"); }
        }

        public MeetingDetail_VM()
        {
            Meeting = Storage.Instance.SelectedMeeting;
            Meeting.Room = Database.GetRooms().FirstOrDefault(x=>x.Room_Id == Meeting.Room_Id);
        }

        public ICommand Edit
        {
            get { return new Command(_edit); }
        }

        private void _edit()
        {
            var nav = new NavigationService();
            var stor = Storage.Instance;
            stor.SelectedMeeting = Meeting;
            nav.Navigate(typeof(AddMeeting));
        }


        public ICommand GoBackClick
        {
            get { return new Command(_goBackClick); }
        }

        private void _goBackClick()
        {
            var nav = new NavigationService();
            nav.Navigate(typeof(Schedule));
        }
    }
}
