using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileApp
{
	public class User
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string Role { get; set; }
		public string Photo { get; set; }

		public static List<User> Users = new List<User>
		{
			new User
			{
				Login = "user1",
				Password = "password1",
				FirstName = "Иван",
				LastName = "Иванов",
				Age = 30,
				Role = "Администратор",
				Photo = "Photo.jpg"
			},
			new User
			{
				Login = "user2",
				Password = "password2",
				FirstName = "Петр",
				LastName = "Петров",
				Age = 25,
				Role = "Менеждер",
				Photo = "Photo2.jpeg"
			},
			new User
			{
				Login = "user3",
				Password = "password3",
				FirstName = "Николай",
				LastName = "Сидоров",
				Age = 45,
				Role = "Кладовщик",
				Photo = "Photo3.jpg"
			},
			new User
			{
				Login = "user4",
				Password = "password4",
				FirstName = "Анна",
				LastName = "Сорока",
				Age = 25,
				Role = "Бухгалтер",
				Photo = "Photo1.jpg"
			}
		};

		public static User Authenticate(string login, string password)
		{
			return Users.FirstOrDefault(u => u.Login == login && u.Password == password);
		}
	}
}