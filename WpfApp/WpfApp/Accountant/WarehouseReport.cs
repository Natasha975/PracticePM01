using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Accountant
{
	public class WarehouseReport
	{
		public string НазваниеСклада { get; set; }
		public string ТипСклада { get; set; }
		public decimal ОбщаяСумма { get; set; }
		public int ОбщееКоличество { get; set; }
	}
}
