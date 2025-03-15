using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProduct
{
	public class ProductStock
	{
		public int НомерЗаписи { get; set; }
		public string НаименованиеТовара { get; set; }
		public string НазваниеСклада { get; set; }
		public decimal ЦенаЗаЕдиницу { get; set; }
		public int Количество { get; set; }
	}
}
