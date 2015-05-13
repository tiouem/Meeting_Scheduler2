using System.Windows.Input;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.View;
using MSchedule.View;

namespace Meeting_Scheduler_App.Model
{
    public class V_ScheduleBlock
    {
        public Windows.UI.Xaml.Media.Brush Color { get; set; }

        public bool Booked { get; set; }

        public int BlockWidth { get; set; }

        public string ButtonText
        {
            get
            {
                if (BlockWidth > 40 && Booked)
                {
                    return _meeting.Date.Value.ToString("H:mm - ") +
                           _meeting.Date.Value.AddMinutes((double)_meeting.Duration.Value).ToString("H:mm");
                }
                return "";
            }
        }

        public double ButtonFontSize {
            get {
                if ((BlockWidth > 70) && (BlockWidth < 110))
                {
                    return 10;
                }
                else
                {
                    return 15;
                }
            }
        }

        public V_ScheduleBlock(Meeting meeting)
        {
            _meeting = meeting;
        }
        
        private readonly Meeting _meeting;

        public ICommand MeetingClick
        {
            get { return new Command(_meetingClick); }
        }

        private void _meetingClick()
        {
            var nav = new NavigationService();
            var stor = Storage.Instance;
            stor.SelectedMeeting = _meeting;
         
            if (Booked)
            {
                nav.Navigate(typeof(MeetingDetail));                
            }
            else
            {
                nav.Navigate(typeof(AddMeeting));
            }
        }
    }
}
