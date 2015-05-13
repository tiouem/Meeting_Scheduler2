using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using Meeting_Scheduler.EF;
using Meeting_Scheduler.Interface;

namespace Meeting_Scheduler.Implementations
{
    public class EFImplementation: IfControl
    {
        EFModel db = new EFModel();

        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }
        public Room GetRoom(int id)
        {
            return  db.Rooms.Find(id);          
        }
        public bool AddRoom(Room room)
        {
            db.Rooms.Add(room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoomExists(room.Room_Id))
                {
                    return false;
                }               
            }
            return true;
        }
        public void DeleteRoom(Room room)
        {
            db.Rooms.Remove(room);
            db.SaveChanges();
        }
/// <summary>
/// //////////////////////////////////////////////////////////////
/// </summary>

        public IQueryable<Meeting> GetMeetings()
        {
            return db.Meetings;
        }

        public Meeting GetMeeting(int id)
        {
            return db.Meetings.Find(id);
        }

        public void DeleteMeeting(Meeting meeting)
        {
            db.Meetings.Remove(meeting);
            db.SaveChanges();
        }

        public bool AddMeeting(Meeting meeting)
        {
            db.Meetings.Add(meeting);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoomExists(meeting.Meeting_Id))
                {
                    return false;
                }              
            }
            return true;
        }

        public bool ModifyMeeting( int id,Meeting meeting)
        {
            Meeting m = GetMeeting(meeting.Meeting_Id);
            m = meeting;
            using (EFModel db2 = new EFModel())
            {
                db2.Entry(m).State = EntityState.Modified;
             
                try
                {
                    db2.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(id))
                    {
                        return false;
                    }

                }
                return true;
            }
           
        }

        public IQueryable<User> GetUsers()
        {
         return db.Users;
        }


        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.Room_Id == id) > 0;
        }
        private bool MeetingExists(int id)
        {
            return db.Meetings.Count(e => e.Meeting_Id == id) > 0;
        }
    }
}