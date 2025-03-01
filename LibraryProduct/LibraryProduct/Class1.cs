using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProduct
{
    public class ProductManager
	{
		public class Product
		{
			public string Name { get; set; }
			public string Category { get; set; }
			public int Quantity { get; set; }
			public decimal Price { get; set; }
			public string Warehouse { get; set; }
		}

		public int GetTotalQuantity(List<Product> products)
		{
			return products.Sum(p => p.Quantity);
		}

		public int GetTotalQuantity(List<Product> products, string warehouse)
		{
			return products.Where(p => p.Warehouse == warehouse).Sum(p => p.Quantity);
		}

		public decimal GetTotalCost(List<Product> products)
		{
			return products.Sum(p => p.Quantity * p.Price);
		}

		public decimal GetTotalCost(List<Product> products, string warehouse)
		{
			return products.Where(p => p.Warehouse == warehouse).Sum(p => p.Quantity * p.Price);
		}

		public Dictionary<string, int> GetQuantityByCategory(List<Product> products)
		{
			return products.GroupBy(p => p.Category)
						   .ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
		}

		public Dictionary<string, int> GetQuantityByCategory(List<Product> products, string warehouse)
		{
			return products.Where(p => p.Warehouse == warehouse)
							.GroupBy(p => p.Category)
							.ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
		}
	}
}
