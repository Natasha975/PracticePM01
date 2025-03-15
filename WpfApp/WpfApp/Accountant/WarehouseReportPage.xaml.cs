using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp.Accountant
{
	/// <summary>
	/// Логика взаимодействия для WarehouseReportPage.xaml
	/// </summary>
	public partial class WarehouseReportPage : Page
	{
		public WarehouseReportPage()
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
		{
			using (var db = new WarEntities())
			{
				var отчет = db.Склад
				   .Select(s => new WarehouseReport
				   {
					   НазваниеСклада = s.НазваниеСклада,
					   ТипСклада = s.ТипCклад != null ? s.ТипCклад.Наименование : "Не указано",
					   ОбщаяСумма = s.ТоварНаСкладе
						   .Where(t => t.Товар != null && t.Товар.ЦенаЗаЕдиницу != null && t.Количество != null)
						   .Sum(t => (decimal?)t.Товар.ЦенаЗаЕдиницу * t.Количество) ?? 0,
					   ОбщееКоличество = s.ТоварНаСкладе
						   .Select(t => t.Количество)
						   .DefaultIfEmpty(0)
						   .Sum()
				   }).ToList();

				WarehouseDataGrid.ItemsSource = отчет;
			}
		}
	}
}
