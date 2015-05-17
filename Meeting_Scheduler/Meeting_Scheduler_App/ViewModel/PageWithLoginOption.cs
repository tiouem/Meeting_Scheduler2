using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;
using Meeting_Scheduler_App.View;

namespace Meeting_Scheduler_App.ViewModel
{
    class PageWithLoginOption:ObservableObject
    {

        public User LoggedUser
        {
            get { return Storage.Instance.LoggedUser; }
        }

        public ICommand ShowLoginPageClick
        {
            get { return new Command(_showLoginPageClick); }
        }

        private void _showLoginPageClick()
        {
            var nav = new NavigationService();
            nav.Navigate(typeof(Login));
        }
        public ICommand LogoutClick
        {
            get { return new Command(_logoutClick); }
        }

        private void _logoutClick()
        {
            Storage.Instance.LoggedUser = null;
            RaisePropertyChangedEvent("LoggedUser");
        }
    }
}
