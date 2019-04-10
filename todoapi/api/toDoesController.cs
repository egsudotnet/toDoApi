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
    public class toDoesController : ApiController
    {
     
        private toDoListEntities db = new toDoListEntities();
     
        // GET: api/toDoes
        public IQueryable<toDo> GettoDoes()
        {
            return db.toDoes;
        }

        // GET: api/toDoes/5 
        [ResponseType(typeof(toDo))]
        public IHttpActionResult GettoDo(string id)
        {
            toDo toDo = db.toDoes.Find(id);
            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        // PUT: api/toDoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttoDo(string id, toDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.ID)
            {
                return BadRequest();
            }

            db.Entry(toDo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!toDoExists(id))
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

        // POST: api/toDoes
        [ResponseType(typeof(toDo))]
        public IHttpActionResult PosttoDo(toDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.toDoes.Add(toDo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (toDoExists(toDo.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = toDo.ID }, toDo);
        }

        // DELETE: api/toDoes/5 
        [ResponseType(typeof(toDo))]
        public IHttpActionResult DeletetoDo(string id)
        {
            toDo toDo = db.toDoes.Find(id);
            if (toDo == null)
            {
                return NotFound();
            }

            db.toDoes.Remove(toDo);
            db.SaveChanges();

            return Ok(toDo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool toDoExists(string id)
        {
            return db.toDoes.Count(e => e.ID == id) > 0;
        }
    }
}