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


namespace Meeting_Scheduler_App.ViewModel
{
    public class VM : ObservableObject
    {
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

        private Room _selectedRoom;

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                RaisePropertyChangedEvent("SelectedRoom");
            }
        }


        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private DateTimeOffset _date;

        public DateTimeOffset Date
        {
            get { return _date; }
            set { _date = value; }
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

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion

        #region commands

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

            var MDate = this.Date.Date.Add(ts2);

            var toAdd = new Meeting()
            {
                Description = this.Description,
                Date = MDate,
                Duration = (int) ts2.Subtract(ts1).TotalMinutes,
                Name = this.Name,
                Room = SelectedRoom,
                Room_Id = SelectedRoom.Room_Id
            };

        }

        #endregion


        private ObservableCollection<V_Tick> _v_Header;

        public ObservableCollection<V_Tick> V_Header
        {
            get { return _v_Header; }
            set { _v_Header = value; }
        }

        private ObservableCollection<V_Room> _v_Rooms;

        public ObservableCollection<V_Room> V_Rooms
        {
            get { return _v_Rooms; }
            set { _v_Rooms = value; }
        }

        private ObservableCollection<Room> _rooms;

        public List<Room> FilteredRooms
        {
            get
            {
                //new Windows.UI.Popups.MessageDialog("chci romy").ShowAsync();
                return new List<Room>(_rooms.Where(
                    x =>
                        (Convert.ToBoolean(x.Camera) || (x.Camera == _checkCamera)) &&
                        (Convert.ToBoolean(x.FlipChart) || (x.FlipChart == _checkFlipchart)) &&
                        (Convert.ToBoolean(x.Phone) || (x.Phone == _checkPhone)) &&
                        (Convert.ToBoolean(x.Projector) || (x.Projector == _checkProjector)) &&
                        (x.Capacity >= Convert.ToInt32(MinCapacity))));
            }
        }

        public VM()
        {
            _v_Rooms = new ObservableCollection<V_Room>();
            _v_Header = new ObservableCollection<V_Tick>();
            Random rnd = new Random();

            _rooms = new ObservableCollection<Room>();


            for (int i = 1; i < 13; i++)
            {
                _v_Header.Add(new V_Tick() {StrTick = Convert.ToString(i + 7) + ":00", TickWidth = 100});
                _v_Rooms.Add(new V_Room() {Name = "Room " + Convert.ToString(i)});
            }

            RoomTypeList = new List<string>();
            RoomTypeList.Add("Conferention room");
            RoomTypeList.Add("Normal room");
            RoomTypeList.Add("Classroom");

            newRoom = new Room();
            SelectedRoom2 = new Room()
            {
                Type = "Conferention room",
                Room_Id = 0,
                Capacity = 20,
                Camera = false,
                FlipChart = false,
                Phone = true,
                Projector = false,
                Image =
                    "C:\\Users\\T\\AppData\\Local\\Packages\\3d7ea83a-e5ae-4681-b00c-d8428d933b2e_1tkvwat7n69a8\\LocalState\\projector.png"

            };
            CreateRoomCommand = new RelayCommand(CreateRoom);
            PickFileCommand = new RelayCommand(PickFile);
        }

        public static Room SelectedRoom2 { get; set; }

        private Room newRoom;
        public List<string> RoomTypeList { get; set; }

        public Room NewRoom
        {
            get { return newRoom; }
            set { newRoom = value; }
        }

        public ICommand PickFileCommand { get; set; }

        public async void PickFile()
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
                newRoom.Image = file2.Path;
            }
            else
            {
                MessageDialog mg = new MessageDialog("File not choosed");
                mg.ShowAsync();
            }
        }

        public ICommand CreateRoomCommand { get; set; }

        public void CreateRoom()
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

        public IQueryable<Room> GetRooms()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Rooms").Result;

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IQueryable<Room>>().Result;
                return rooms;
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }
            return null;

        }

        public IQueryable<Meeting> GeMeetings()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Meetings").Result;

            if (response.IsSuccessStatusCode)
            {
                var meetings = response.Content.ReadAsAsync<IQueryable<Meeting>>().Result;
                return meetings;
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }
            return null;
        } 


    }
}
