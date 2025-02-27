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
using WebApp.Ent;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class СкладController : ApiController
    {
        private WarehouseEntities db = new WarehouseEntities();

        // GET: api/Склад
        [ResponseType(typeof(List<ResponseСклад>))]
        public IHttpActionResult GetСклад()
        {
            return Ok(db.Склад.ToList().ConvertAll(p => new ResponseСклад(p)));
        }


        // GET: api/Склад/5
        [ResponseType(typeof(Склад))]
        public IHttpActionResult GetСклад(int id)
        {
            Склад склад = db.Склад.Find(id);
            if (склад == null)
            {
                return NotFound();
            }

            return Ok(склад);
        }

        // PUT: api/Склад/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutСклад(int id, Склад склад)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != склад.Номер)
            {
                return BadRequest();
            }

            db.Entry(склад).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!СкладExists(id))
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

        // POST: api/Склад
        [ResponseType(typeof(Склад))]
        public IHttpActionResult PostСклад(Склад склад)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Склад.Add(склад);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = склад.Номер }, склад);
        }

        // DELETE: api/Склад/5
        [ResponseType(typeof(Склад))]
        public IHttpActionResult DeleteСклад(int id)
        {
            Склад склад = db.Склад.Find(id);
            if (склад == null)
            {
                return NotFound();
            }

            db.Склад.Remove(склад);
            db.SaveChanges();

            return Ok(склад);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool СкладExists(int id)
        {
            return db.Склад.Count(e => e.Номер == id) > 0;
        }
    }
}