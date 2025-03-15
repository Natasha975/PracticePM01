using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Storekeeper
{
	public class ОтчетИнвентаризации
	{
		public string НазваниеТовара { get; set; }
		public int ОжидаемыйРез { get; set; }
		public int ФактическийРез { get; set; }
		public int Расхождение { get; set; }
	}
}
