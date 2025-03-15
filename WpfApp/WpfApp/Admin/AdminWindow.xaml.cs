using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Code;
using WpfApp.Model;

namespace WpfApp.Admin
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		private Пользователь currentUser;
		public AdminWindow(Пользователь user)
		{
			InitializeComponent();
			currentUser = user;
		}
		private void BtnWarehouse_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new WarehousePage());
		}

		private void BtnProducts_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new ProductsPage());
		}

		private void BtnClients_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new ClientsPage());
		}

		private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new SuppliersPage());
		}

		private void BtnUsers_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new UsersPage());
		}

		private void Ber_Click(object sender, RoutedEventArgs e)
		{
			CodeWindow main = new CodeWindow();
			main.Show();
			this.Close();
		}

		private void BtnProfile_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new UserProfilePage(currentUser));
		}
	}
}
