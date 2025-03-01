using WebApp.Ent;

namespace WebApp.Models
{
	public class ResponseАвтор
	{
		public ResponseАвтор(Пользователь пользователь)
		{
			Номер = пользователь.Номер;
			Роль = пользователь.Роль;
			Логин = пользователь.Логин;
			Пароль = пользователь.Пароль;
		}
		public int Номер { get; set; }
		public int Роль { get; set; }
		public string Логин { get; set; }
		public string Пароль { get; set; }
	}
}