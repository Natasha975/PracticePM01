using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryProduct
{
    public class WarehouseServiceAPI
    {

		private readonly HttpClient _httpClient;
		private const string ApiUrl = "http://localhost:60319/api/products";

		public WarehouseServiceAPI(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		// Метод для получения данных от API
		public async Task<List<ProductStock>> GetProductsFromApiAsync()
		{
			var response = await _httpClient.GetAsync(ApiUrl);

			// Проверяем, что запрос успешен
			response.EnsureSuccessStatusCode(); 

			var json = await response.Content.ReadAsStringAsync();

			// Десериализация JSON
			var products = JsonSerializer.Deserialize<List<ProductStock>>(json); 
			return products;
		}

		// Метод для подсчета общего количества товаров на всех складах
		public int GetTotalQuantity(List<ProductStock> products)
		{
			return products.Sum(p => p.Количество);
		}

		// Метод для подсчета количества товаров на конкретном складе
		public int GetQuantityByWarehouse(List<ProductStock> products, string warehouseName)
		{
			return products
				.Where(p => p.НазваниеСклада == warehouseName)
				.Sum(p => p.Количество);
		}

		// Метод для подсчета общей стоимости товаров на всех складах
		public decimal GetTotalCost(List<ProductStock> products)
		{
			return products.Sum(p => p.Количество * p.ЦенаЗаЕдиницу);
		}

		// Метод для подсчета стоимости товаров на конкретном складе
		public decimal GetCostByWarehouse(List<ProductStock> products, string warehouseName)
		{
			return products
				.Where(p => p.НазваниеСклада == warehouseName)
				.Sum(p => p.Количество * p.ЦенаЗаЕдиницу);
		}
	}
}
