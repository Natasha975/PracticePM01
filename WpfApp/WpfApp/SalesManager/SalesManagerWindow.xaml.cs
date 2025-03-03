using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp.Model;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для SalesManagerWindow.xaml
	/// </summary>
	public partial class SalesManagerWindow : Window
	{
		public SalesManagerWindow()
		{
			InitializeComponent();
			LoadData();
		}

		public void LoadData()
		{
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

		private void RegClient_Click(object sender, RoutedEventArgs e)
		{
			RegClientWindow regClientWindow = new RegClientWindow();
			regClientWindow.Show();
			this.Close();
		}

		private void InvoiceViewing_Click(object sender, RoutedEventArgs e)
		{
			InvoiceViewingWindow invoice = new InvoiceViewingWindow();
			invoice.Show();
			this.Close();
		}
    }
}