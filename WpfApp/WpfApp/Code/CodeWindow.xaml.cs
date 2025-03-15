using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Model;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Code
{
	/// <summary>
	/// Логика взаимодействия для CodeWindow.xaml
	/// </summary>
	public partial class CodeWindow : Window
	{
		private Пользователь currentUser;
		public CodeWindow()
		{
			InitializeComponent();
		}

		public static string GenerateCode()
		{
			Random random = new Random();
			return random.Next(100000, 999999).ToString();
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			string login = LoginTextBox.Text;
			string password = PasswordBox.Password;

			try
			{
				using (var db = new WarEntities())
				{
					var user = db.Пользователь.FirstOrDefault(s => s.Логин == login && s.ХэшПароль == password);
					if (user != null)
					{
						currentUser = user;
						string generatedCode = GenerateCode();

						CodeVerificationWindow codeVerificationWindow = new CodeVerificationWindow(currentUser, generatedCode);
						codeVerificationWindow.Show();
						this.Close();
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
		}
	}
}
