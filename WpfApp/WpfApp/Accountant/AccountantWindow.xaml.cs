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

namespace WpfApp.Accountant
{
	/// <summary>
	/// Логика взаимодействия для AccountantWindow.xaml
	/// </summary>
	public partial class AccountantWindow : Window
	{
		public AccountantWindow()
		{
			InitializeComponent();
		}

		private void OpenWarehouseReport_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new WarehouseReportPage());
		}

		private void OpenTurnoverReport_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new TurnoverReportPage());
		}

		private void OpenBalanceReport_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new BalanceReportPage());
		}

		private void OpenInventoryReport_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new InventoryReportPage());
		}
	}
}
