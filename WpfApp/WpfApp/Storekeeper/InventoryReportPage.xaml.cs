using System;
using System.Linq;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp.Storekeeper
{
	/// <summary>
	/// Логика взаимодействия для InventoryReportPage.xaml
	/// </summary>
	public partial class InventoryReportPage : Page
	{
		public InventoryReportPage(int номерИнвентаризации)
		{
			InitializeComponent();
			ЗагрузитьОтчет(номерИнвентаризации);
		}

		private void ЗагрузитьОтчет(int номерИнвентаризации)
		{
			using (var db = new WarEntities())
			{
				var результаты = db.РезИнвентаризации
					.Where(r => r.Инвентаризация.Any(i => i.Номер == номерИнвентаризации))
					.Select(r => new ОтчетИнвентаризации
					{
						НазваниеТовара = r.Товар1.Название,
						ОжидаемыйРез = r.ОжидаемыйРез ?? 0,
						ФактическийРез = r.ФактическийРез ?? 0,
						Расхождение = r.Расхождение ?? 0
					}).ToList();

				ОтчетDataGrid.ItemsSource = результаты;
			}
		}
	}
}
