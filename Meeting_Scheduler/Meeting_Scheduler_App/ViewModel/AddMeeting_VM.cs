using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.ViewModel
{
    class AddMeeting_VM : ObservableObject
    {
        public AddMeeting_VM()
        {
            var stor = Storage.Instance;
            _rooms = new List<Room>(Database.GetRooms());

            if (stor.SelectedMeeting != null)
            {
                _newMeeting = stor.SelectedMeeting;
                StartTime = _newMeeting.Date.Value.TimeOfDay;
                EndTime = _newMeeting.Date.Value.TimeOfDay.Add(new TimeSpan(0, _newMeeting.Duration.Value, 0));
                _newMeeting.Room = _rooms.FirstOrDefault(x => x.Room_Id == stor.SelectedMeeting.Room_Id);
                _date = _newMeeting.Date.Value;
            }
        }

        private List<Room> _rooms;

        #region filter bindings

        public List<Room> FilteredRooms
        {
            get
            {
                return new List<Room>(_rooms.Where(
                    x =>
                        (Convert.ToBoolean(x.Camera) || (x.Camera == _checkCamera)) &&
                        (Convert.ToBoolean(x.FlipChart) || (x.FlipChart == _checkFlipchart)) &&
                        (Convert.ToBoolean(x.Phone) || (x.Phone == _checkPhone)) &&
                        (Convert.ToBoolean(x.Projector) || (x.Projector == _checkProjector)) &&
                        (x.Capacity >= Convert.ToInt32(MinCapacity))));
            }
        }

        private bool _checkFlipchart;

        public bool CheckFlipchart
        {
            get { return _checkFlipchart; }
            set
            {
                _checkFlipchart = value;
                RaisePropertyChangedEvent("FilteredRooms");
            }
        }

        private bool _checkCamera;

        public bool CheckCamera
        {
            get { return _checkCamera; }
            set
            {
                _checkCamera = value;
                RaisePropertyChangedEvent("FilteredRooms");
            }
        }

        private bool _checkPhone;
        
        public bool CheckPhone
        {
            get { return _checkPhone; }
            set
            {
                _checkPhone = value;
                RaisePropertyChangedEvent("FilteredRooms");
            }
        }

        private bool _checkProjector;

        public bool CheckProjector
        {
            get { return _checkProjector; }
            set
            {
                _checkProjector = value;
                RaisePropertyChangedEvent("FilteredRooms");
            }
        }

        private string _minCapacity;

        public string MinCapacity
        {
            get { return _minCapacity; }
            set
            {
                _minCapacity = value;
                RaisePropertyChangedEvent("FilteredRooms");
            }
        }

        #endregion

        #region new meeting bindings

        private Meeting _newMeeting = new Meeting();

        public Meeting NewMeeting
        {
            get { return _newMeeting; }
            set { _newMeeting = value; }
        }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTimeOffset Date {
            get {return  new DateTimeOffset(_date);}
            set {_date = value.DateTime; }
        }

        private DateTime _date;

        #endregion

        #region Commands

        public ICommand AddMeetingClick
        {
            get { return new Command(_addMeetingClick); }
        }

        public ICommand DeleteMeetingClick
        {
            get { return new Command(_deleteMeetingClick); }
        }

        private void _deleteMeetingClick()
        {

        }

        private void _addMeetingClick()
        {
            _newMeeting.Duration = (int)EndTime.Subtract(StartTime).TotalMinutes;
            _newMeeting.Date = _date;
            _newMeeting.Room_Id = _newMeeting.Room.Room_Id;

        }

        #endregion
    }
}
