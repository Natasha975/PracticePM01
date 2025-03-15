using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp.Code;
using WpfApp.Model;

namespace WpfApp.SalesManager
{
	/// <summary>
	/// Логика взаимодействия для SalesManagerWindow.xaml
	/// </summary>
	public partial class SalesManagerWindow : Window
	{
		private Пользователь currentUser;

		public SalesManagerWindow(Пользователь user)
		{
			InitializeComponent();
			currentUser = user;

		}

		public void LoadData()
		{
			StackWar.Visibility = Visibility.Visible;
			try
			{
				using (var db = new WarEntities())
				{
					var склады = db.Склад
						.Select(s => new Склад
						{
							НазваниеСклада = s.НазваниеСклада,
							Товары = db.ТоварНаСкладе
								.Where(t => t.НомерСклада == s.Номер)
								.Join(db.Товар,
									t => t.НомерТовара,
									товар => товар.Номер,
									(t, товар) => new ТоварНаСкладе
									{
										НаименованиеТовара = товар.Название,
										Количество = t.Количество
									})
								.ToList()
						})
						.ToList();

					treeView.ItemsSource = склады;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
			}
		}

		public class Склад
		{
			public string НазваниеСклада { get; set; }
			public List<ТоварНаСкладе> Товары { get; set; } = new List<ТоварНаСкладе>();
		}

		public class ТоварНаСкладе
		{
			public string НаименованиеТовара { get; set; }
			public int Количество { get; set; }
		}

		private void Ber_Click(object sender, RoutedEventArgs e)
		{
			CodeWindow main = new CodeWindow();
			main.Show();
			this.Close();
		}

		private void WerSee_Click(object sender, RoutedEventArgs e)
		{
			LoadData();
		}

		private void RegClient_Click(object sender, RoutedEventArgs e)
		{
			StackWar.Visibility = Visibility.Collapsed;

			MainFrame.Navigate(new RegClientPage());
		}

		private void InvoiceViewing_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new InvoiceViewingPage());
		}

		private void BtnProfile_Click(object sender, RoutedEventArgs e)
		{
			StackWar.Visibility = Visibility.Collapsed;

			MainFrame.Navigate(new UserProfilePage(currentUser));
		}

		private void Order_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new OrderSupplierPage());
		}
	}
}
