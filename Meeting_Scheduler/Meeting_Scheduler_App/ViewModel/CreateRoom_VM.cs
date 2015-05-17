using System;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.ViewModel
{
    internal class CreateRoom_VM : ObservableObject
    {
        public Room NewRoom { get; set; }
        public CreateRoom_VM()
        {
            NewRoom = new Room();
        }

        public ICommand CreateRoomClick
        {
            get { return new Command(_createRoomClick); }
        }

        private void _createRoomClick()
        {
            Database.CreateRoom(NewRoom);
            NewRoom = new Room();
            RaisePropertyChangedEvent("NewRoom");
        }

        public ICommand PickFileClick
        {
            get { return new Command(_pickFileClick); }
        }

        private void _pickFileClick()
        {
            throw new NotImplementedException();
        }

    }
}