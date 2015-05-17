using System;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;
using MSchedule.View;

namespace Meeting_Scheduler_App.ViewModel
{
    public class MeetingDetail_VM
    {
        public MeetingDetail_VM()
        {
            Meeting = Storage.Instance.SelectedMeeting;
            Meeting.Room = Database.GetRooms().FirstOrDefault(x => x.Room_Id == Meeting.Room_Id);
        }

        public Meeting Meeting { get; set; }

        public string Date
        {
            get
            {
                return Meeting.Date.Value.ToString("dddd dd. MMMM yyyy H:mm - ") +
                       Meeting.Date.Value.AddMinutes(Meeting.Duration.Value).ToString("H:mm");
            }
        }

        public ICommand Edit
        {
            get { return new Command(_edit); }
        }

        public ICommand GoBackClick
        {
            get { return new Command(_goBackClick); }
        }

        private void _edit()
        {
            if (Meeting.Date >= DateTime.Now)
            {
                var nav = new NavigationService();
                nav.Navigate(typeof (AddMeeting));
            }
            else
            {
                var md = new MessageDialog("You can't edit meetings that already happened.");
                md.ShowAsyncQueue();
            }
        }

        private void _goBackClick()
        {
            var nav = new NavigationService();
            nav.Navigate(typeof (Schedule));
        }
    }
}