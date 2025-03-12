using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryProduct;

namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
		}

		private readonly string _apiUrl = "https://localhost:60319/api/products"; // URL реального API

		[TestMethod]
		public async Task GetProductsFromApiAsync_ShouldReturnProducts_WhenApiIsAvailable()
		{
			// Arrange
			var warehouseService = new WarehouseService(new List<Product>());

			// Act
			var products = await WarehouseService.GetProductsFromApiAsync(_apiUrl);

			// Assert
			Assert.IsNotNull(products);
			Assert.IsTrue(products.Count > 0);
		}
	}
}
