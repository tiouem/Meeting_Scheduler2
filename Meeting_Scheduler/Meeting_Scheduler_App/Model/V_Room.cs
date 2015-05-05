using System;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Meeting_Scheduler_App.Model
{
    public class V_Room
    {
        private ObservableCollection<V_ScheduleBlock> _blocks;

        public ObservableCollection<V_ScheduleBlock> Blocks
        {
            get { return _blocks; }
            set { _blocks = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public V_Room()
        {
            _blocks = new ObservableCollection<V_ScheduleBlock>();
            Random rnd = new Random();
            for (int i = 1; i < 13; i++)
            {
                if (rnd.Next(1,3) == 1)
                {
                    _blocks.Add(new V_ScheduleBlock(){Booked = true, Color = new SolidColorBrush(Windows.UI.Colors.Crimson), BlockWidth = 100});  
                }
                else
                {
                    _blocks.Add(new V_ScheduleBlock() { Booked = true, Color = new SolidColorBrush(new Color() { A = 0, R = 0, G = 0, B = 0 }), BlockWidth = 100 });  
                }
            }               
        }
    }
}
