using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MobileApp.WarehouseDetailsPage;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WarehouseListPage : ContentPage
	{
		public WarehouseListPage()
		{
			InitializeComponent();
			LoadWarehouses();			
		}

		public class Warehouse
		{
			public string Name { get; set; }
			public string Address { get; set; }
			public string WarehouseType { get; set; }
			public string StorageZone { get; set; }
			public string Image { get; set; }
			public List<Product> Products { get; set; }
		}

		public class Product
		{
			public string Name { get; set; }
			public int Quantity { get; set; }
			public string Category { get; set; }
			public decimal Price { get; set; }
		}
		private void LoadWarehouses()
		{
			var warehouses = new List<Warehouse>
			{
				new Warehouse
				{
					Name = "Склад 1",
					Address = "Кировская область, Киров, Ленина, 10",
					WarehouseType = "Основной",
					StorageZone = "Стеллаж 1",
					Image = "warehouse1.jpg",
					Products = new List<Product>
					{
						new Product { Name = "Монитор Samsung 27\"", Quantity = 96, Category = "Электроника", Price = 25000.00m },
						new Product { Name = "Клавиатура Logitech K120", Quantity = 50, Category = "Компьютерные аксессуары", Price = 1200.00m },
						new Product {Name = "Мышь проводная USB Defender", Quantity = 50, Category = "Компьютерные аксессуары", Price = 500.00m}
					}
				},
				new Warehouse
				{
					Name = "Склад Номер 2",
					Address = "Кировская область, Киров, Баумана, 2",
					WarehouseType = "Временный",
					StorageZone = "Полка 3",
					Image = "warehouse2.jpg",
					Products = new List<Product>
					{
						new Product { Name = "Монитор Samsung 27\"", Quantity = 200, Category = "Электроника", Price = 25000.00m },
						new Product {Name = "Мышь проводная USB Defender", Quantity = 200, Category = "Компьютерные аксессуары", Price = 500.00m},
						new Product {Name = "Оперативная память DDR4 8GB Kingston", Quantity = 50, Category = "Компьютерные комплектующие", Price = 3200.00m}
					}
				},
				new Warehouse
				{
					Name = "Склад 3",
					Address = "Томская область, Томск, Гагарина, 5",
					WarehouseType = "Розничный",
					StorageZone = "Стеллаж 2",
					Image = "warehouse3.jpg",
					Products = new List<Product>
					{
						new Product { Name = "Монитор Samsung 27\"", Quantity = 500, Category = "Электроника", Price = 25000.00m },
						new Product { Name = "Клавиатура Logitech K120", Quantity = 100, Category = "Компьютерные аксессуары", Price = 1200.00m }
					}
				},
				new Warehouse
				{
					Name = "Склад 4",
					Address = "Пермский край, Пермь, Мира, 12",
					WarehouseType = "Основной",
					StorageZone = "Стеллаж 2",
					Image = "warehouse3.jpg",
					Products = new List<Product>
					{
						new Product { Name = "Клавиатура Logitech K120", Quantity = 130, Category = "Компьютерные аксессуары", Price = 1200.00m }
					}
				},
				new Warehouse
				{
					Name = "Склад 5",
					Address = "Свердловская область, Екатеринбург, Малышева, 30",
					WarehouseType = "Временный",
					StorageZone = "Стеллаж 2",
					Image = "warehouse4.jpg",
					Products = new List<Product>
					{
						new Product { Name = "Клавиатура Logitech K120", Quantity = 60, Category = "Компьютерные аксессуары", Price = 1200.00m }
					}
				}
			};
			WarehouseListView.ItemsSource = warehouses;
		}

		private async void WarehouseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var selectedWarehouse = e.SelectedItem as Warehouse;

			await Navigation.PushAsync(new WarehouseDetailsPage(selectedWarehouse));

			WarehouseListView.SelectedItem = null;
		}
	}
}