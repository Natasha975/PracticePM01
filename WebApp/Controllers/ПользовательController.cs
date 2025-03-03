﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Ent;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ПользовательController : ApiController
    {
        private WarehouseEntities db = new WarehouseEntities();

		//[ResponseType(typeof(void))]
		//public IHttpActionResult Авторизация(ResponseАвтор login)
		//{
		//	if (login == null || string.IsNullOrEmpty(login.Логин) || string.IsNullOrEmpty(login.Пароль))
		//	{
		//		return BadRequest("Неверные данные для входа.");
		//	}

		//	var пользователь = db.Пользователь.FirstOrDefault(u => u.Логин == login.Логин && u.Пароль == login.Пароль);
		//	if (пользователь == null)
		//	{
		//		return Unauthorized();
		//	}

		//	return Ok(new { UserId = пользователь.Номер });
		//}


		//public IQueryable<Пользователь> GetПользователь()
		//      {
		//          return db.Пользователь;
		//      }

		// POST: api/Пользователь
		[ResponseType(typeof(List<ResponseАвтор>))]
		public IHttpActionResult GetПользователь(ResponseАвтор login)
		{
			//return Ok(db.Пользователь.ToList().ConvertAll(p => new ResponseАвтор(p)));
			if (login == null || string.IsNullOrEmpty(login.Логин) || string.IsNullOrEmpty(login.Пароль))
			{
				return BadRequest("Неверные данные для входа.");
			}

			var пользователь = db.Пользователь.FirstOrDefault(u => u.Логин == login.Логин && u.Пароль == login.Пароль);
			if (пользователь == null)
			{
				return Unauthorized();
			}

			return Ok(new AuthResponse
			{
				UserId = пользователь.Номер,
				Role = пользователь.Роль,
				RequiresTwoFactor = true
			});

			//return Ok(new { UserId = пользователь.Номер });
		}

		// GET: api/Пользователь/5
		//[ResponseType(typeof(Пользователь))]
		//      public IHttpActionResult GetПользователь(int id)
		//      {
		//          Пользователь пользователь = db.Пользователь.Find(id);
		//          if (пользователь == null)
		//          {
		//              return NotFound();
		//          }

		//          return Ok(пользователь);
		//      }

		//      // PUT: api/Пользователь/5
		//      [ResponseType(typeof(void))]
		//      public IHttpActionResult PutПользователь(int id, Пользователь пользователь)
		//      {
		//          if (!ModelState.IsValid)
		//          {
		//              return BadRequest(ModelState);
		//          }

		//          if (id != пользователь.Номер)
		//          {
		//              return BadRequest();
		//          }

		//          db.Entry(пользователь).State = EntityState.Modified;

		//          try
		//          {
		//              db.SaveChanges();
		//          }
		//          catch (DbUpdateConcurrencyException)
		//          {
		//              if (!ПользовательExists(id))
		//              {
		//                  return NotFound();
		//              }
		//              else
		//              {
		//                  throw;
		//              }
		//          }

		//          return StatusCode(HttpStatusCode.NoContent);
		//      }

		//      // POST: api/Пользователь
		//      [ResponseType(typeof(Пользователь))]
		//      public IHttpActionResult PostПользователь(Пользователь пользователь)
		//      {
		//          if (!ModelState.IsValid)
		//          {
		//              return BadRequest(ModelState);
		//          }

		//          db.Пользователь.Add(пользователь);
		//          db.SaveChanges();

		//          return CreatedAtRoute("DefaultApi", new { id = пользователь.Номер }, пользователь);
		//      }

		//      // DELETE: api/Пользователь/5
		//      [ResponseType(typeof(Пользователь))]
		//      public IHttpActionResult DeleteПользователь(int id)
		//      {
		//          Пользователь пользователь = db.Пользователь.Find(id);
		//          if (пользователь == null)
		//          {
		//              return NotFound();
		//          }

		//          db.Пользователь.Remove(пользователь);
		//          db.SaveChanges();

		//          return Ok(пользователь);
		//      }

		//      protected override void Dispose(bool disposing)
		//      {
		//          if (disposing)
		//          {
		//              db.Dispose();
		//          }
		//          base.Dispose(disposing);
		//      }

		//      private bool ПользовательExists(int id)
		//      {
		//          return db.Пользователь.Count(e => e.Номер == id) > 0;
		//      }
	}
}