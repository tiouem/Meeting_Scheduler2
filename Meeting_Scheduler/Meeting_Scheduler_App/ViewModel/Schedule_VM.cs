using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;
using Meeting_Scheduler_App.View;

namespace Meeting_Scheduler_App.ViewModel
{
    internal class Schedule_VM : PageWithLoginOption
    {
        public Schedule_VM()
        {
            _drawCalendar();
        }

        public List<V_Tick> VHeader { get; set; }
        public List<V_Room> VRooms { get; set; }

        public DateTimeOffset Date
        {
            get { return Storage.Instance.Date; }
        }

        public string StringDate
        {
            get { return Date.Date.ToString("dd. MM. yyyy"); }
        }

        private void _drawCalendar()
        {
            VRooms = new List<V_Room>();
            var meetings =
                Database.GetMeetings().Where(x => x.Date != null && x.Date.Value.Date == Storage.Instance.Date);
            var rooms = Database.GetRooms();

            var firstMeeting = TimeSpan.Parse("7:00");
            var lastMeeting = TimeSpan.Parse("19:00");

            if (meetings.Any())
            {
                firstMeeting = meetings.Min(x => x.Date != null ? x.Date.Value.TimeOfDay : new TimeSpan());
                lastMeeting = meetings.Max(x => x.Date != null ? x.Date.Value.TimeOfDay : new TimeSpan());

                firstMeeting = new TimeSpan(firstMeeting.Hours, 0, 0);
                lastMeeting = new TimeSpan(lastMeeting.Hours + 1, 0, 0);
                if (firstMeeting > TimeSpan.Parse("7:00"))
                {
                    firstMeeting = TimeSpan.Parse("7:00");
                }

                if (lastMeeting < TimeSpan.Parse("19:00"))
                {
                    lastMeeting = TimeSpan.Parse("19:00");
                }
            }

            var containerWidth = Window.Current.Bounds.Width - 120 - 120;

            VHeader = new List<V_Tick>();
            for (var i = firstMeeting.TotalHours; i < lastMeeting.TotalHours; i++)
            {
                VHeader.Add(new V_Tick
                {
                    StrTick = Convert.ToString(i) + ":00",
                    TickWidth = (int) (containerWidth/(lastMeeting.TotalHours - firstMeeting.TotalHours))
                });
            }

            var odd = false;

            var total = (int) (lastMeeting.TotalMinutes - firstMeeting.TotalMinutes);

            foreach (var room in rooms)
            {
                var schdBlocks = new List<V_ScheduleBlock>();
                var meetingsInThisRoom =
                    meetings.Where(x => x.Room_Id == room.Room_Id)
                        .OrderBy(x => x.Date != null ? x.Date.Value.TimeOfDay.TotalMinutes : 0);
                var lastBlockEnd = (int) firstMeeting.TotalMinutes;
                Meeting potentionalMeeting;
                foreach (var meeting in meetingsInThisRoom)
                {
                    if (meeting.Date != null && (meeting.Date.Value.TimeOfDay.TotalMinutes - lastBlockEnd) > 0)
                    {
                        potentionalMeeting = new Meeting
                        {
                            Date = Storage.Instance.Date.Date.AddMinutes(lastBlockEnd),
                            Duration = (int) meeting.Date.Value.TimeOfDay.TotalMinutes - lastBlockEnd,
                            Room_Id = room.Room_Id
                        };
                        schdBlocks.Add(new V_ScheduleBlock(potentionalMeeting)
                        {
                            BlockWidth =
                                (int)
                                    (((meeting.Date.Value.TimeOfDay.TotalMinutes - lastBlockEnd)/total)*containerWidth),
                            Booked = false,
                            Color = new SolidColorBrush(odd ? Colors.White : Colors.GhostWhite)
                        });
                    }

                    if (meeting.Duration != null)
                    {
                        schdBlocks.Add(new V_ScheduleBlock(meeting)
                        {
                            BlockWidth = (int) (((double) meeting.Duration.Value/total)*containerWidth),
                            Color = new SolidColorBrush(Colors.Crimson),
                            Booked = true
                        });
                        lastBlockEnd = (int) meeting.Date.Value.TimeOfDay.TotalMinutes + meeting.Duration.Value;
                    }
                }
                potentionalMeeting = new Meeting
                {
                    Date = Storage.Instance.Date.Date.AddMinutes(lastBlockEnd),
                    Duration = 60,
                    Room_Id = room.Room_Id
                };
                schdBlocks.Add(new V_ScheduleBlock(potentionalMeeting)
                {
                    BlockWidth = (int) containerWidth,
                    Color = new SolidColorBrush(odd ? Colors.White : Colors.GhostWhite),
                    Booked = false
                });
                VRooms.Add(new V_Room(room) {Name = room.Type, Blocks = schdBlocks});
                odd = !odd;
            }
            RaisePropertyChangedEvent("VRooms");
            RaisePropertyChangedEvent("VHeader");
        }

        #region Commands

        public ICommand NextDayClick
        {
            get { return new Command(_nextDayClick); }
        }

        private void _nextDayClick()
        {
            Storage.Instance.Date = Storage.Instance.Date.AddDays(1);
            _drawCalendar();
            RaisePropertyChangedEvent("StringDate");
        }

        public ICommand PrevDayClick
        {
            get { return new Command(_prevDayClick); }
        }

        private void _prevDayClick()
        {
            Storage.Instance.Date = Storage.Instance.Date.AddDays(-1);
            _drawCalendar();
            RaisePropertyChangedEvent("StringDate");
        }

        public ICommand AddRoomClick
        {
            get { return new Command(_addRoomClick); }
        }

        private void _addRoomClick()
        {
            var nav = new NavigationService();
            nav.Navigate(typeof(CreateRoom));
        }

        public ICommand AddUserClick
        {
            get { return new Command(_addUserClick); }
        }

        private void _addUserClick()
        {
            var nav = new NavigationService();
            nav.Navigate(typeof(AddUser));
        }


        #endregion
    }
}