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
    public class ТоварController : ApiController
    {
        private WarehouseEntities db = new WarehouseEntities();

        // GET: api/Товар
        [ResponseType(typeof(List<ResponseТовар>))]
        public IHttpActionResult GetТовар()
        {
            return Ok(db.Товар.ToList().ConvertAll(p=> new ResponseТовар(p)));
        }

        //// POST: api/Товар/Add
        //public IHttpActionResult AddТовар([FromBody] Товар товар)
        //{
        //    if (товар == null)
        //    {
        //        return BadRequest("Отсутствуют данные о продукте.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        db.Товар.Add(товар);
        //        db.SaveChanges();

        //        return CreatedAtRoute("DefaultApi, new { id = товар.Номер }, товар);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine(ex);
        //        return InternalServerError(ex); 
        //    }
        //}

        // GET: api/Товар/5
        [ResponseType(typeof(Товар))]
        public IHttpActionResult GetТовар(int id)
        {
            Товар товар = db.Товар.Find(id);
            if (товар == null)
            {
                return NotFound();
            }

            return Ok(товар);
        }

        // PUT: api/Товар/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutТовар(int id, Товар товар)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != товар.Номер)
            {
                return BadRequest();
            }

            db.Entry(товар).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ТоварExists(id))
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

        // POST: api/Товар
        [ResponseType(typeof(Товар))]
        public IHttpActionResult PostТовар(Товар товар)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Товар.Add(товар);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = товар.Номер }, товар);
        }

        // DELETE: api/Товар/5
        [ResponseType(typeof(Товар))]
        public IHttpActionResult DeleteТовар(int id)
        {
            Товар товар = db.Товар.Find(id);
            if (товар == null)
            {
                return NotFound();
            }

            db.Товар.Remove(товар);
            db.SaveChanges();

            return Ok(товар);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ТоварExists(int id)
        {
            return db.Товар.Count(e => e.Номер == id) > 0;
        }
    }
}