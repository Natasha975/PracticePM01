using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp.Admin
{
	public class ПользовательData
	{
		public int Номер { get; set; }
		public string Фамилия { get; set; }
		public string Имя { get; set; }
		public string Отчество { get; set; }
		public Роль Роль { get; set; }
		public string Логин { get; set; }
		public string Пароль { get; set; }
	}
}
