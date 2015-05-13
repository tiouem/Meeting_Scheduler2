using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Meeting_Scheduler_App.View;


namespace Meeting_Scheduler_App.ViewModel
{
    public class VM : ObservableObject
    {

        #region Add Meeting

        #region filter bindings
        
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

        private string _startTime;

        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private string _endTime;

        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

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
            TimeSpan ts1 = TimeSpan.Parse(StartTime);
            TimeSpan ts2 = TimeSpan.Parse(EndTime);

            //var MDate = this.Date.Date.Add(ts2);

          //  _newMeeting.Date = this.Date.Date.Add(ts2);
            _newMeeting.Duration = (int)ts2.Subtract(ts1).TotalMinutes;
            _newMeeting.Room_Id = _newMeeting.Room.Room_Id;

        }

        #endregion

        #endregion

        #region Schedule

        #endregion

        #region Add Room
        public static Room SelectedRoom2 { get; set; }

        private Room _newRoom = new Room();

        public Room NewRoom
        {
            get { return _newRoom; }
            set { _newRoom = value; }
        }

        public ICommand PickFileCommand
        {
            get { return new Command(_pickFile); }
        }

        public ICommand CreateRoomCommand
        {
            get { return new Command(_createRoom); }
        }

        private async void _pickFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                await file.CopyAsync(ApplicationData.Current.LocalFolder);
                StorageFile file2 = await ApplicationData.Current.LocalFolder.GetFileAsync(file.Name.ToString());
                _newRoom.Image = file2.Path;
            }
            else
            {
                MessageDialog mg = new MessageDialog("File not choosed");
                mg.ShowAsync();
            }
        }

        private void _createRoom()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("api/Rooms", NewRoom).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageDialog md = new MessageDialog("Room added");
                md.ShowAsync();

            }
            else
            {
                MessageDialog md = new MessageDialog("Error");
                md.ShowAsync();
            }
        }
        #endregion



        private ObservableCollection<Room> _rooms;

        public List<string> RoomTypeList { get; set; }

        public VM()
        {

            Random rnd = new Random();

            _rooms = new ObservableCollection<Room>(GetRooms());


            RoomTypeList = new List<string>();
            RoomTypeList.Add("Conferention room");
            RoomTypeList.Add("Normal room");
            RoomTypeList.Add("Classroom");


            SelectedRoom2 = new Room()
            {
                Type = "Conferention room",
                Room_Id = 0,
                Capacity = 20,
                Camera = false,
                FlipChart = false,
                Phone = false,
                Projector = false,
                Image =
                    "C:\\Users\\T\\AppData\\Local\\Packages\\3d7ea83a-e5ae-4681-b00c-d8428d933b2e_1tkvwat7n69a8\\LocalState\\projector.png"
            };
        }

        #region Database

        public IEnumerable<Room> GetRooms()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Rooms").Result;

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms;
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }
            return null;
        }

        public IEnumerable<Meeting> GetMeetings()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Meetings").Result;

            if (response.IsSuccessStatusCode)
            {
                var meetings = response.Content.ReadAsAsync<IEnumerable<Meeting>>().Result;
                return meetings;
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }
            return null;
        }

        public Room GetRoom()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            string path = "api/Rooms/" + SelectedRoom2.Room_Id;
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var room = response.Content.ReadAsAsync<Room>().Result;
                return room;
            }
            else
            {
                MessageDialog md = new MessageDialog("Room not found");
                md.ShowAsync();
            }
            return null;
        }
        #endregion


    }
}
