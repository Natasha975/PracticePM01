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

namespace WpfApp.Storekeeper
{
	/// <summary>
	/// Логика взаимодействия для StorekeeperWindow.xaml
	/// </summary>
	public partial class StorekeeperWindow : Window
	{
		private Пользователь currentUser;

		public StorekeeperWindow(Пользователь user)
		{
			InitializeComponent();
			currentUser = user;
		}

		private void Ber_Click(object sender, RoutedEventArgs e)
		{
			CodeWindow main = new CodeWindow();
			main.Show();
			this.Close();
		}

		private void Invoice_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new RegistrationInvoicePage());
		}

		private void QRCode_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new ProductCodePage());
		}

		private void InventoryReport_Click(object sender, RoutedEventArgs e)
		{
			int номерИнвентаризации = 1;
			MainFrame.Navigate(new InventoryReportPage(номерИнвентаризации));
		}

		private void BtnProfile_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new UserProfilePage(currentUser));
		}
	}
}
