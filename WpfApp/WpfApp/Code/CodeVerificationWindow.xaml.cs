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
using WpfApp.Model;
using WpfApp.SalesManager;
using WpfApp.Storekeeper;
using WpfApp.Accountant;
using WpfApp.Admin;

namespace WpfApp.Code
{
	/// <summary>
	/// Логика взаимодействия для CodeVerificationWindow.xaml
	/// </summary>
	public partial class CodeVerificationWindow : Window
	{
		private Пользователь currentUser;
		private string generatedCode;

		public CodeVerificationWindow(Пользователь user, string code)
		{
			InitializeComponent();
			currentUser = user;
			generatedCode = code;
			CodeLabel.Content = $"Код: {generatedCode}";
		}

		private void SubmitCodeButton_Click(object sender, RoutedEventArgs e)
		{
			string enteredCode = CodeTextBox.Text;

			if (enteredCode == generatedCode)
			{
				switch (currentUser.Роль)
				{
					case 1:
						AdminWindow adminWindow = new AdminWindow(currentUser);
						adminWindow.Show();
						this.Close();
						break;
					case 2:
						StorekeeperWindow storekeeperWindow = new StorekeeperWindow(currentUser);
						storekeeperWindow.Show();
						this.Close();
						break;
					case 3:
						SalesManagerWindow salesManagerWindow = new SalesManagerWindow(currentUser);
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
	}
}
