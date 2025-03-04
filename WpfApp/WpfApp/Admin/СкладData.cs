using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp.Admin
{
	public class СкладData
	{
		public int Номер { get; set; }
		public string НазваниеСклада { get; set; }
		public string Субъект { get; set; }
		public string Улица { get; set; }
		public int Дом { get; set; }
		public ТипCклад ТипСклада { get; set; }
		public string ЗоныХранения { get; set; }
	}
}
