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
    public class ТоварНаСкладеController : ApiController
    {
        private WarehouseEntities db = new WarehouseEntities();

        // GET: api/ТоварНаСкладе
        [ResponseType(typeof(List<ResponseТоварНаСкладе>))]
        public IHttpActionResult GetоварНаСкладе()
        {
            var result = db.ТоварНаСкладе
                .Select(tns => new ResponseТоварНаСкладе
                {
                    НазваниеСклада = tns.Склад.НазваниеСклада,
                    НазваниеТовара = tns.Товар.Название,
                    Количество = tns.Количество
                })
                .ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/ТоварНаСкладе/Номер склада 
        [ResponseType(typeof(List<ResponseТоварНаСкладе>))]
        public IHttpActionResult GetТоварыНаСкладеПоСкладу(int складId)
        {
            var result = db.ТоварНаСкладе
                .Where(tns => tns.НомерСклада == складId)
                .Select(tns => new ResponseТоварНаСкладе
                {
                    НазваниеСклада = tns.Склад.НазваниеСклада,
                    НазваниеТовара = tns.Товар.Название,
                    Количество = tns.Количество
                })
                .ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        //[ResponseType(typeof(List<ResponseТоварНаСкладе>))]
        //public IHttpActionResult GetоварНаСкладе()
        //{
        //    return Ok(db.ТоварНаСкладе.ToList().ConvertAll(p => new ResponseТоварНаСкладе(p)));
        //}


        // GET: api/ТоварНаСкладе/5
        [ResponseType(typeof(ТоварНаСкладе))]
        public IHttpActionResult GetТоварНаСкладе(int id)
        {
            ТоварНаСкладе товарНаСкладе = db.ТоварНаСкладе.Find(id);
            if (товарНаСкладе == null)
            {
                return NotFound();
            }

            return Ok(товарНаСкладе);
        }

        // PUT: api/ТоварНаСкладе/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutТоварНаСкладе(int id, ТоварНаСкладе товарНаСкладе)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != товарНаСкладе.НомерЗаписи)
            {
                return BadRequest();
            }

            db.Entry(товарНаСкладе).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ТоварНаСкладеExists(id))
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

        // POST: api/ТоварНаСкладе
        [ResponseType(typeof(ТоварНаСкладе))]
        public IHttpActionResult PostТоварНаСкладе(ТоварНаСкладе товарНаСкладе)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ТоварНаСкладе.Add(товарНаСкладе);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = товарНаСкладе.НомерЗаписи }, товарНаСкладе);
        }

        // DELETE: api/ТоварНаСкладе/5
        [ResponseType(typeof(ТоварНаСкладе))]
        public IHttpActionResult DeleteТоварНаСкладе(int id)
        {
            ТоварНаСкладе товарНаСкладе = db.ТоварНаСкладе.Find(id);
            if (товарНаСкладе == null)
            {
                return NotFound();
            }

            db.ТоварНаСкладе.Remove(товарНаСкладе);
            db.SaveChanges();

            return Ok(товарНаСкладе);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ТоварНаСкладеExists(int id)
        {
            return db.ТоварНаСкладе.Count(e => e.НомерЗаписи == id) > 0;
        }
    }
}