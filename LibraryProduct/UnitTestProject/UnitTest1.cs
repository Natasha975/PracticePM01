using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibraryProduct;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
		 private List<Product> _testProducts;

        [TestInitialize]
        public void Initialize()
        {
            // Инициализация тестовых данных
            _testProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
                new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
                new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
                new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
            };
        }

        [TestMethod]
        public void GetTotalQuantity()
        {
			_testProducts = new List<Product>
			{
				new Product { Id = 1, Name = "Ноутбук", Quantity = 10, Price = 50000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 2, Name = "Монитор", Quantity = 5, Price = 20000, WarehouseId = 1, Category = "Электроника" },
				new Product { Id = 3, Name = "Стул", Quantity = 20, Price = 3000, WarehouseId = 2, Category = "Мебель" },
				new Product { Id = 4, Name = "Стол", Quantity = 15, Price = 7000, WarehouseId = 2, Category = "Мебель" }
			};

			var warehouseService = new WarehouseService(_testProducts);

            var totalQuantity = warehouseService.GetTotalQuantity();

            Assert.AreEqual(50, totalQuantity); // 10 + 5 + 20 + 15 = 50
        }

        [TestMethod]
        public void GetQuantityByWarehouse()
        {
            var warehouseService = new WarehouseService(_testProducts);

            var quantityWarehouse1 = warehouseService.GetQuantityByWarehouse(1); 
            var quantityWarehouse2 = warehouseService.GetQuantityByWarehouse(2); 

            Assert.AreEqual(15, quantityWarehouse1); // 10 + 5 = 15
            Assert.AreEqual(35, quantityWarehouse2); // 20 + 15 = 35
        }

        [TestMethod]
        public void GetTotalCost()
        {
            var warehouseService = new WarehouseService(_testProducts);

            var totalCost = warehouseService.GetTotalCost();

            Assert.AreEqual(865000m, totalCost); // (10 * 50000) + (5 * 20000) + (20 * 3000) + (15 * 7000) = 865000
        }

        [TestMethod]
        public void GetCostByWarehouse()
        {
            var warehouseService = new WarehouseService(_testProducts);

            var costWarehouse1 = warehouseService.GetCostByWarehouse(1);
            var costWarehouse2 = warehouseService.GetCostByWarehouse(2);

            Assert.AreEqual(600000m, costWarehouse1); // (10 * 50000) + (5 * 20000) = 600000
            Assert.AreEqual(265000m, costWarehouse2); // (20 * 3000) + (15 * 7000) = 265000
        }

        [TestMethod]
        public void GetQuantityByCategory()
        {
            var warehouseService = new WarehouseService(_testProducts);

            var quantityByCategory = warehouseService.GetQuantityByCategory();

            Assert.AreEqual(15, quantityByCategory["Электроника"]); // 10 + 5 = 15
            Assert.AreEqual(35, quantityByCategory["Мебель"]); // 20 + 15 = 35
        }

        [TestMethod]
        public void GetQuantityByCategoryAndWarehouse()
        {
            var warehouseService = new WarehouseService(_testProducts);

            var quantityByCategoryWarehouse1 = warehouseService.GetQuantityByCategoryAndWarehouse(1); // Склад 1
            var quantityByCategoryWarehouse2 = warehouseService.GetQuantityByCategoryAndWarehouse(2); // Склад 2

            Assert.AreEqual(15, quantityByCategoryWarehouse1["Электроника"]); // 10 + 5 = 15
            Assert.AreEqual(35, quantityByCategoryWarehouse2["Мебель"]); // 20 + 15 = 35
        }
	}
}
