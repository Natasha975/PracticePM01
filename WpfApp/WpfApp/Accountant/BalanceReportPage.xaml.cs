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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Model;

namespace WpfApp.Accountant
{
	/// <summary>
	/// Логика взаимодействия для BalanceReportPage.xaml
	/// </summary>
	public partial class BalanceReportPage : Page
	{
		public BalanceReportPage()
		{
			InitializeComponent();
			LoadData();
		}
		private void LoadData()
		{
			using (var db = new WarEntities())
			{
				// Загрузка данных из таблицы "ТоварНаСкладе" и установка источника данных для BalanceDataGrid
				var отчет = db.ТоварНаСкладе
					.Select(t => new
					{
						t.Склад,
						t.Товар,
						t.Количество
					}).ToList();

				BalanceDataGrid.ItemsSource = отчет;

				// Вычисление общего остатка товаров на всех складах
				int общийОстаток = отчет.Sum(t => t.Количество);
				TotalAmountTextBlock.Content = $"Общий остаток: {общийОстаток}";

				// Группировка данных по складам
				var остаткиПоСкладам = отчет
					.GroupBy(t => t.Склад.НазваниеСклада)
					.Select(g => new
					{
						Склад = g.Key,
						Остаток = g.Sum(t => t.Количество)
					}).ToList();

				// Очистка панели перед добавлением новых Label
				StockLabelsPanel.Children.Clear();

				// Создание Label для каждого склада
				foreach (var склад in остаткиПоСкладам)
				{
					var label = new Label
					{
						Content = $"Склад: {склад.Склад}, Остаток: {склад.Остаток}",
						FontSize = 14,
						Margin = new Thickness(0, 5, 0, 0)
					};
					StockLabelsPanel.Children.Add(label);
				}
			}
		}

	}
}
