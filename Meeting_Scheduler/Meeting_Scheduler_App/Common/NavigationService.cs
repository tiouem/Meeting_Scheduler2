﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler_App.Common
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;


    public class NavigationService
    {
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }
    }
}
