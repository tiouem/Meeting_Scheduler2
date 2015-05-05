﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using Meeting_Scheduler.EF;
using Meeting_Scheduler.Implementations;
using Meeting_Scheduler.Interface;

namespace Meeting_Scheduler.Controllers
{
    public class RoomsController : ApiController
    {
        private EFModel db = new EFModel();
        IfControl asd = new EFImplementation();
        
        // GET: api/Rooms
        public IQueryable<Room> GetRooms()
        {
            return asd.GetRooms();
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
           Room room = asd.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Room_Id)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!asd.AddRoom(room))
            {
                return Conflict();
            }
            else
            {
                return Ok();              
            }
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = asd.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }
            asd.DeleteRoom(room);
            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.Room_Id == id) > 0;
        }
    }
}