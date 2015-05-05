namespace Meeting_Scheduler_App.Model
{
    public class V_ScheduleBlock
    {
        private Windows.UI.Xaml.Media.Brush _color;

        public Windows.UI.Xaml.Media.Brush Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private bool _booked;

        public bool Booked
        {
            get { return _booked; }
            set { _booked = value; }
        }

        private int _blockWidth;

        public int BlockWidth
        {
            get { return _blockWidth; }
            set { _blockWidth = value; }
        }

        
        
    }
}
