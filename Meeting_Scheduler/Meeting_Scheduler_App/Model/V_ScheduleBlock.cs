using System;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.View;
using MSchedule.View;

namespace Meeting_Scheduler_App.Model
{
    public class V_ScheduleBlock
    {
        private readonly Meeting _meeting;

        public V_ScheduleBlock(Meeting meeting)
        {
            _meeting = meeting;
        }

        public Brush Color { get; set; }
        public bool Booked { get; set; }
        public int BlockWidth { get; set; }

        public string ButtonText
        {
            get
            {
                if (BlockWidth > 40 && Booked)
                {
                    return _meeting.Date.Value.ToString("H:mm - ") +
                           _meeting.Date.Value.AddMinutes(_meeting.Duration.Value).ToString("H:mm");
                }
                return "";
            }
        }

        public double ButtonFontSize
        {
            get
            {
                if ((BlockWidth > 70) && (BlockWidth < 110))
                {
                    return 10;
                }
                return 15;
            }
        }

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
                nav.Navigate(typeof (MeetingDetail));
            }
            else
            {
                if (!(Storage.Instance.Date.DateTime < DateTime.Today))
                {
                    nav.Navigate(typeof (AddMeeting));
                }
                else
                {
                    var md =
                        new MessageDialog(
                            "You can't plan meetings in past. If you want to create new meeting navigate to current or future date with buttons in top corners of screen.");
                    md.ShowAsyncQueue();
                }
            }
        }
    }
}