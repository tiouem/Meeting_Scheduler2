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
    class Login_VM
    {
        public User LoginUser { get; set; }

        public ICommand LoginClick
        {
            get { return new Command(_loginClick); }
        }

        public Login_VM()
        {
                LoginUser = new User(){Password = "", Username = ""};
        }

        private void _loginClick()
        {
            var usrHandler = new UserHandler();
            if (usrHandler.CheckUserExists(LoginUser, Storage.Instance.LoggedUser))
            {
                var md = new MessageDialog("Successfully logged in.");
                md.ShowAsyncQueue();
            }
            else
            {
                var md = new MessageDialog("Wrong username or password.");
                md.ShowAsyncQueue();
            }
        }
    }
}
