using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Meeting_Scheduler.EF;
using Meeting_Scheduler.Implementations;
using Meeting_Scheduler.Interface;

namespace Meeting_Scheduler.Controllers
{
    public class MeetingsController : ApiController
    {
        private EFModel db = new EFModel();
        IfControl asd = new EFImplementation();

        // GET: api/Meetings
        public IQueryable<Meeting> GetMeetings()
        {
            return asd.GetMeetings();
        }

        // GET: api/Meetings/5
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult GetMeeting(int id)
        {
            Meeting meeting = asd.GetMeeting(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }

        // PUT: api/Meetings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeeting(int id, Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meeting.Meeting_Id)
            {
                return BadRequest();
            }

            if (!asd.ModifyMeeting(id,meeting))
            {
                return Conflict();
            }
            else
            {
                return Ok(meeting);
            }

           

            
        }

        // POST: api/Meetings
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult PostMeeting(Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!asd.AddMeeting(meeting))
            {
                return Conflict();
            }
            else
            {
                return Ok();
            }
          
        }

        // DELETE: api/Meetings/5
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult DeleteMeeting(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            db.Meetings.Remove(meeting);
            db.SaveChanges();

            return Ok(meeting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}