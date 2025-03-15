using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryProduct
{
	public class WarehouseService
	{
        private List<Product> products;

        public WarehouseService(List<Product> products)
        {
            products = products ?? throw new ArgumentNullException(nameof(products));
        }

        // Подсчет общего количества товаров на всех складах
        public int GetTotalQuantity()
        {
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
				new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
			};

			return products.Sum(p => p.Quantity);
        }

        // Подсчет количества товаров на конкретном складе
        public int GetQuantityByWarehouse(int warehouseId)
        {
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
				new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
			};

			return products
                .Where(p => p.WarehouseId == warehouseId)
                .Sum(p => p.Quantity);
        }

        // Подсчет общей стоимости товаров на всех складах
        public decimal GetTotalCost()
        {
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
				new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
			};

			return products.Sum(p => p.Price * p.Quantity);
        }

        // Подсчет стоимости товаров на конкретном складе
        public decimal GetCostByWarehouse(int warehouseId)
        {
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
				new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
			};

			return products
                .Where(p => p.WarehouseId == warehouseId)
                .Sum(p => p.Price * p.Quantity);
        }

        // Подсчет количества товаров по категориям на всех складах
        public Dictionary<string, int> GetQuantityByCategory()
        {
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
				new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
			};

			return products
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
        }

        // Подсчет количества товаров по категориям на конкретном складе
        public Dictionary<string, int> GetQuantityByCategoryAndWarehouse(int warehouseId)
        {

			return products
                .Where(p => p.WarehouseId == warehouseId)
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
        }
    }
}
