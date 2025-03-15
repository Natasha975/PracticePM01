using System;
using System.Linq;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp.Accountant
{
	/// <summary>
	/// Логика взаимодействия для TurnoverReportPage.xaml
	/// </summary>
	public partial class TurnoverReportPage : Page
	{
		public TurnoverReportPage()
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
		{
			using (var db = new WarEntities())
			{
				// Получение данных о приходе
				var приходы = db.ПриходнаяНакладная
					.Select(p => new
					{
						Товар = p.ТоварВНакладной.Товар,
						Склад = p.СписокТоваров, 
						Количество = p.ТоварВНакладной.Количество,
						ЦенаЗаЕдиницу = p.ТоварВНакладной.Товар.ЦенаЗаЕдиницу
					})
					.GroupBy(t => new { t.Товар, t.Склад }) // Группировка по товару и складу
					.Select(g => new
					{
						Товар = g.Key.Товар,
						Склад = g.Key.Склад,
						Приход = g.Sum(t => t.Количество), // Сумма количество прихода
						СуммаПрихода = g.Sum(t => t.Количество * t.ЦенаЗаЕдиницу) // Сумма прихода
					})
					.ToList();

				// Получение данных о расходе
				var расходы = db.РасходнаяНакладная
					.Select(r => new
					{
						Товар = r.ТоварВНакладной.Товар, 
						Склад = r.СписокТоваров, 
						Количество = r.ТоварВНакладной.Количество,
						ЦенаЗаЕдиницу = r.ТоварВНакладной.Товар.ЦенаЗаЕдиницу
					})
					.GroupBy(t => new { t.Товар, t.Склад }) 
					.Select(g => new
					{
						Товар = g.Key.Товар,
						Склад = g.Key.Склад,
						Расход = g.Sum(t => t.Количество),
						СуммаРасхода = g.Sum(t => t.Количество * t.ЦенаЗаЕдиницу)
					})
					.ToList();

				// Объединение данных о приходе и расходе
				var отчет = приходы
					.GroupJoin(расходы,
						приход => new { приход.Товар, приход.Склад },
						расход => new { расход.Товар, расход.Склад },
						(приход, расходГруппа) => new
						{
							Товар = приход.Товар,
							Приход = приход.Приход,
							Расход = расходГруппа.FirstOrDefault()?.Расход ?? 0,
							Сумма = приход.СуммаПрихода - (расходГруппа.FirstOrDefault()?.СуммаРасхода ?? 0) // Общая сумма
						})
					.ToList();

				TurnoverDataGrid.ItemsSource = отчет;
			}
		}
	}
}
