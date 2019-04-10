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
using todoapi.Models;

namespace todoapi.api
{
    public class toDo2Controller : ApiController
    {
        private toDoListEntities db = new toDoListEntities();

        // GET: api/toDo2
        public IQueryable<toDo2> GettoDo2()
        {
            return db.toDo2;
        }

        // GET: api/toDo2/5
        [ResponseType(typeof(toDo2))]
        public IHttpActionResult GettoDo2(int id)
        {
            toDo2 toDo2 = db.toDo2.Find(id);
            if (toDo2 == null)
            {
                return NotFound();
            }

            return Ok(toDo2);
        }

        // PUT: api/toDo2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttoDo2(int id, toDo2 toDo2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo2.ID)
            {
                return BadRequest();
            }

            db.Entry(toDo2).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!toDo2Exists(id))
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

        // POST: api/toDo2
        [ResponseType(typeof(toDo2))]
        public IHttpActionResult PosttoDo2(toDo2 toDo2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.toDo2.Add(toDo2);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toDo2.ID }, toDo2);
        }

        // DELETE: api/toDo2/5
        [HttpDelete]
        [Route("api/toDo2/deleteData/{id}")]
        public IHttpActionResult deleteData(int id)
        {
            toDo2 toDo2 = db.toDo2.Find(id);
            if (toDo2 == null)
            {
                return NotFound();
            }

            db.toDo2.Remove(toDo2);
            db.SaveChanges();

            return Ok(toDo2);
        }

        [ResponseType(typeof(toDo2))]
        public IHttpActionResult DeletetoDo2(int id)
        {
            toDo2 toDo2 = db.toDo2.Find(id);
            if (toDo2 == null)
            {
                return NotFound();
            }

            db.toDo2.Remove(toDo2);
            db.SaveChanges();

            return Ok(toDo2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool toDo2Exists(int id)
        {
            return db.toDo2.Count(e => e.ID == id) > 0;
        }
    }
}