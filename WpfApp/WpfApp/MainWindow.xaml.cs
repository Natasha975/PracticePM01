using System;
using System.Linq;
using System.Windows;
using WpfApp.Model;
namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//private readonly ApiService apiService = new ApiService();

		private string generatedCode;
		private Пользователь currentUser;
		public MainWindow()
		{
			InitializeComponent();
			CodeTextBox.Visibility = Visibility.Collapsed;
			CodeLabel.Visibility = Visibility.Collapsed;
			SubmitCodeButton.Visibility = Visibility.Collapsed;
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			string login = LoginTextBox.Text;
			string password = PasswordBox.Text;

			try
			{
				using (var db = new WarEntities())
				{
					var user = db.Пользователь.FirstOrDefault(s => s.Логин == login && s.Пароль == password);
					if (user != null)
					{
						currentUser = user;
						generatedCode = GenerateCode();

						CodeTextBox.Visibility = Visibility.Visible;
						CodeLabel.Visibility = Visibility.Visible;
						SubmitCodeButton.Visibility = Visibility.Visible;
						CodeLabel.Content = $"Сгенерированный код: {generatedCode}";

						LoginTextBox.Visibility = Visibility.Collapsed;
						PasswordBox.Visibility = Visibility.Collapsed;
						LoginButton.Visibility = Visibility.Collapsed;
					}
					else
					{
						MessageBox.Show("Логин или пароль не верен");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}");
			}

			//try
			//{
			//	var authResponse = await apiService.AuthenticateAsync(login, password);

			//	if (authResponse != null)
			//	{
			//		// Проверка роли и открытие соответствующего окна
			//		switch (authResponse.Role)
			//		{
			//			case "1":
			//				new AdminWindow().Show();
			//				break;
			//			case "2":
			//				new StorekeeperWindow().Show();
			//				break;
			//			case "3":
			//				new SalesManagerWindow().Show();
			//				break;
			//			case "4":
			//				new AccountantWindow().Show();
			//				break;
			//			default:
			//				MessageBox.Show("Роль не определена.");
			//				break;
			//		}

			//		this.Close();
			//	}
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show($"Ошибка: {ex.Message}");
			//}
		}

		private void SubmitCodeButton_Click(object sender, RoutedEventArgs e)
		{
			string enteredCode = CodeTextBox.Text;

			if (enteredCode == generatedCode)
			{
				switch (currentUser.Роль)
				{
					case 1:
						AdminWindow adminWindow = new AdminWindow();
						adminWindow.Show();
						this.Close();
						break;
					case 2:
						StorekeeperWindow storekeeperWindow = new StorekeeperWindow();
						storekeeperWindow.Show();
						this.Close();
						break;
					case 3:
						SalesManagerWindow salesManagerWindow = new SalesManagerWindow();
						salesManagerWindow.Show();
						this.Close();
						break;
					case 4:
						AccountantWindow accountantWindow = new AccountantWindow();
						accountantWindow.Show();
						this.Close();
						break;
				}
			}
			else
			{
				MessageBox.Show("Неверный код");
			}
		}

		private string GenerateCode()
		{
			Random random = new Random();
			return random.Next(100000, 999999).ToString();
		}
	}
}