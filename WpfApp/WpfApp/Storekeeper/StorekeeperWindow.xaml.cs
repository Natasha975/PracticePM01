using System.Windows;
using WpfApp.Storekeeper;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для StorekeeperWindow.xaml
	/// </summary>
	public partial class StorekeeperWindow : Window
	{
		public StorekeeperWindow()
		{
			InitializeComponent();			
		}	

		private void Invoice_Click(object sender, RoutedEventArgs e)
		{
			RegistrationInvoiceWindow reg = new RegistrationInvoiceWindow();
			reg.Show();
			this.Close();
		}

		private void QRCode_Click(object sender, RoutedEventArgs e)
		{
			ProductQRCodeWindow product = new ProductQRCodeWindow();
			product.Show();
			this.Close();
		}
	}
}
