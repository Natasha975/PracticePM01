using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MobileApp.WarehouseListPage;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WarehouseDetailsPage : ContentPage
	{
		public WarehouseDetailsPage(Warehouse selectedWarehouse)
		{
			InitializeComponent();

			Title = selectedWarehouse.Name;
			allProducts = selectedWarehouse.Products;
			ProductsListView.ItemsSource = allProducts;
		}

		private List<Product> allProducts;

		private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			var searchText = e.NewTextValue.ToLower();
			var filteredProducts = allProducts.Where(p => p.Name.ToLower().Contains(searchText)).ToList();
			ProductsListView.ItemsSource = filteredProducts;
		}
	}
}