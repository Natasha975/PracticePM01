using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для InvoiceViewingWindow.xaml
	/// </summary>
	public partial class InvoiceViewingWindow : Window
	{
		public InvoiceViewingWindow()
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
					var приходные = db.ПриходнаяНакладная
						.Select(s => new НакладнаяДляОтображения
						{
							ТипНакладной = "Приходная",
							НомерНакладной = s.НомерНакладной,
							Дата = s.ДатаПоступления,
							Контрагент = s.Поставщики.Название,
							Товары = db.ТоварВНакладной
								.Where(t => t.Номер == s.СписокТоваров)
								.Select(t => new ТоварДляОтображения
								{
									НаименованиеТовара = t.Товар.Название,
									Количество = t.Количество,
									ЦенаЗаЕдиницу = t.Цена
								})
								.ToList(),
							ОбщаяСумма = s.ОбщаяСумма
						})
						.ToList();

					var расходные = db.РасходнаяНакладная
						.Join(db.Клиент,
							расходная => расходная.Клиент,
							клиент => клиент.Номер,
							(расходная, клиент) => new НакладнаяДляОтображения
							{
								ТипНакладной = "Расходная",
								НомерНакладной = расходная.НомерНакладной,
								Дата = расходная.ДатаОтгрузки,
								Контрагент = клиент.Название,
								Товары = db.ТоварВНакладной
									.Where(t => t.Номер == расходная.СписокТоваров)
									.Select(t => new ТоварДляОтображения
									{
										НаименованиеТовара = t.Товар.Название,
										Количество = t.Количество,
										ЦенаЗаЕдиницу = t.Цена
									})
									.ToList(),
								ОбщаяСумма = расходная.ОбщаяСумма
							})
						.ToList();

					foreach (var накладная in приходные)
					{
						var item = new TreeViewItem
						{
							Header = $"{накладная.ТипНакладной} №{накладная.НомерНакладной}",
							Tag = накладная
						};
						приходныеНакладные.Items.Add(item);
					}

					foreach (var накладная in расходные)
					{
						var item = new TreeViewItem
						{
							Header = $"{накладная.ТипНакладной} №{накладная.НомерНакладной}",
							Tag = накладная
						};
						расходныеНакладные.Items.Add(item);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
			}
		}

		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue is TreeViewItem selectedItem && selectedItem.Tag != null)
			{
				if (selectedItem.Tag is НакладнаяДляОтображения накладная)
				{
					detailsText.Text = $"Тип: {накладная.ТипНакладной}\nНомер: {накладная.НомерНакладной}\nДата: {накладная.Дата.ToShortDateString()}\n" +
						$"Контрагент: {накладная.Контрагент}\nОбщая сумма: {накладная.ОбщаяСумма.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("ru-RU"))}";
					detailsGrid.ItemsSource = накладная.Товары;
				}
			}
		}

		public class НакладнаяДляОтображения
		{
			public string ТипНакладной { get; set; }
			public string НомерНакладной { get; set; }
			public DateTime Дата { get; set; }
			public string Контрагент { get; set; }
			public List<ТоварДляОтображения> Товары { get; set; }
			public decimal ОбщаяСумма { get; set; }
		}

		public class ТоварДляОтображения
		{
			public string НаименованиеТовара { get; set; }
			public int Количество { get; set; }
			public decimal ЦенаЗаЕдиницу { get; set; }
			public decimal Сумма => Количество * ЦенаЗаЕдиницу;
		}
	}
}