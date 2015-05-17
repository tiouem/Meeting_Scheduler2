using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.ViewModel
{
    public class AddUser_VM:ObservableObject
    {
        public User NewUser { get; set; }
        public string PasswordAgain { get; set; }
        public List<string> Positions { get; set; }

        public AddUser_VM()
        {
            NewUser = new User();
            Positions = new List<string>(){"Administrator", "Administrator2?", "Administrator3?"};
        }

        public ICommand AddUserClick
        {
            get { return new Command(_addUserClick); }
        }

        private void _addUserClick()
        {
            if (NewUser.Password == PasswordAgain)
            {
                Database.AddUser(NewUser);
                NewUser = new User();
                PasswordAgain = "";
                RaisePropertyChangedEvent("NewUser");
                RaisePropertyChangedEvent("PasswordAgain");
            }
            else
            {
                var md = new MessageDialog("Passwords don't match.");
                md.ShowAsyncQueue();
            }
        }

    }
}
