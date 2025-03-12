using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProduct
{
	public class Product
	{
		public int Id { get; set; } // Идентификатор товара
		public string Name { get; set; } // Название товара
		public string Category { get; set; } // Категория товара
		public decimal Price { get; set; } // Цена товара
		public int Quantity { get; set; } // Количество товара
		public int WarehouseId { get; set; } // Идентификатор склада
	}
}
