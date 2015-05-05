namespace Meeting_Scheduler_App.Model
{
    public partial class Room
    {
        

        public int Room_Id { get; set; }     
        public string Type { get; set; }

        public int Capacity { get; set; }

        public bool Projector { get; set; }

        public bool FlipChart { get; set; }

        public bool Phone { get; set; }

        public bool Camera { get; set; }
        public string Image { get; set; }
    
    }
}
