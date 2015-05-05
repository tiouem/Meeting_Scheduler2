using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Meeting_Scheduler.EF;

namespace Meeting_Scheduler.Interface
{
    interface IfControl
    {
        IQueryable<Room> GetRooms();
        Room GetRoom(int id);
        void DeleteRoom(Room room);
        bool AddRoom(Room room);
        
        


        IQueryable<Meeting> GetMeetings();
        Meeting GetMeeting(int id);
        void DeleteMeeting(Meeting meeting);
        bool AddMeeting(Meeting meeting);

        bool ModifyMeeting(int id,Meeting meeting);

    }
}
