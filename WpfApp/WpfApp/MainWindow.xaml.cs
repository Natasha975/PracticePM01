using System;
using System.Windows;
namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ApiService apiService = new ApiService();
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			string login = LoginTextBox.Text;
			string password = PasswordBox.Text;

			try
			{
				var authResponse = await apiService.AuthenticateAsync(login, password);

				if (authResponse != null)
				{
					// Проверка роли и открытие соответствующего окна
					switch (authResponse.Role)
					{
						case "Администратор":
							new AdminWindow().Show();
							break;
						case "Кладовщик":
							new StorekeeperWindow().Show();
							break;
						case "Менеджер":
							new SalesManagerWindow().Show();
							break;
						case "Бухгалтер":
							new AccountantWindow().Show();
							break;
						default:
							MessageBox.Show("Роль не определена.");
							break;
					}

					this.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}");
			}
		}
	}
}
