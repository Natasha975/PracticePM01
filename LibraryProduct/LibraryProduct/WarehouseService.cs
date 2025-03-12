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

		private List<Product> _products;

		public WarehouseService(List<Product> products)
		{
			_products = products ?? throw new ArgumentNullException(nameof(products));
		}

		// Метод для получения данных от API
		public static async Task<List<Product>> GetProductsFromApiAsync(string apiUrl)
		{
			using (var httpClient = new HttpClient())
			{
				// Отправляем GET-запрос к API
				var response = await httpClient.GetAsync(apiUrl);

				// Проверяем, что запрос успешен
				response.EnsureSuccessStatusCode();

				// Читаем ответ как строку
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Десериализуем JSON в список объектов Product
				var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true // Игнорируем регистр свойств
				});

				return products;
			}
		}

		// Остальные методы библиотеки (GetTotalQuantity, GetQuantityByWarehouse и т.д.)
		public int GetTotalQuantity() => _products.Sum(p => p.Quantity);
		public int GetQuantityByWarehouse(int warehouseId) => _products.Where(p => p.WarehouseId == warehouseId).Sum(p => p.Quantity);
		public decimal GetTotalCost() => _products.Sum(p => p.Price * p.Quantity);
		public decimal GetCostByWarehouse(int warehouseId) => _products.Where(p => p.WarehouseId == warehouseId).Sum(p => p.Price * p.Quantity);
		public Dictionary<string, int> GetQuantityByCategory() => _products.GroupBy(p => p.Category).ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
		public Dictionary<string, int> GetQuantityByCategoryAndWarehouse(int warehouseId) => _products.Where(p => p.WarehouseId == warehouseId).GroupBy(p => p.Category).ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
		//// Список товаров (данные, полученные от API)
		//private List<Product> products;

		//public WarehouseService(List<Product> products)
		//{
		//	products = products ?? throw new ArgumentNullException(nameof(products));
		//}

		//// 1. Подсчет общего количества товаров на всех складах
		//public int GetTotalQuantity()
		//{
		//	return products.Sum(p => p.Quantity);
		//}

		//// 2. Подсчет количества товаров на конкретном складе
		//public int GetQuantityByWarehouse(int warehouseId)
		//{
		//	return products
		//		.Where(p => p.WarehouseId == warehouseId)
		//		.Sum(p => p.Quantity);
		//}

		//// 3. Подсчет общей стоимости товаров на всех складах
		//public decimal GetTotalCost()
		//{
		//	return products.Sum(p => p.Price * p.Quantity);
		//}

		//// 4. Подсчет стоимости товаров на конкретном складе
		//public decimal GetCostByWarehouse(int warehouseId)
		//{
		//	return products
		//		.Where(p => p.WarehouseId == warehouseId)
		//		.Sum(p => p.Price * p.Quantity);
		//}

		//// 5. Подсчет количества товаров по категориям на всех складах
		//public Dictionary<string, int> GetQuantityByCategory()
		//{
		//	return products
		//		.GroupBy(p => p.Category)
		//		.ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
		//}

		//// 6. Подсчет количества товаров по категориям на конкретном складе
		//public Dictionary<string, int> GetQuantityByCategoryAndWarehouse(int warehouseId)
		//{
		//	return products
		//		.Where(p => p.WarehouseId == warehouseId)
		//		.GroupBy(p => p.Category)
		//		.ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
		//}
	}
}
