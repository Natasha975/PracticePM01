using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Ent;

namespace WebApp.Models
{
	public class ResponseАвтор
	{
		public ResponseАвтор(Пользователь пользователь)
		{
			Номер = пользователь.Номер;
			Фамилия = пользователь.Фамилия;
			Имя = пользователь.Имя;
			Отчество = пользователь.Отчество;
			Роль = пользователь.Роль;
			Login = пользователь.Логин;
			Password = пользователь.Пароль;
		}
		public int Номер { get; set; }
		public string Фамилия { get; set; }
		public string Имя { get; set; }
		public string Отчество { get; set; }
		public int Роль { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}
}