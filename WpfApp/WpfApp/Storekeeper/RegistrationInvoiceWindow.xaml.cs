using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Model;
using WpfApp.Storekeeper;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для RegistrationInvoiceWindow.xaml
	/// </summary>
	public partial class RegistrationInvoiceWindow : Window
	{
		public RegistrationInvoiceWindow()
		{
			InitializeComponent();
			LoadData();
			InvoiceTypeComboBox.SelectionChanged += InvoiceTypeComboBox_SelectionChanged;
		}

		private void LoadData()
		{
			UpdateSupplierClientComboBox("Приходная");
		}

		private void InvoiceTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedType = (InvoiceTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
			UpdateSupplierClientComboBox(selectedType);
		}

		private void UpdateSupplierClientComboBox(string invoiceType)
		{
			using (var db = new WarEntities())
			{
				if (invoiceType == "Приходная")
				{
					var поставщики = db.Поставщики.ToList();
					SupplierClientComboBox.ItemsSource = поставщики;
				}
				else if (invoiceType == "Расходная")
				{
					var клиенты = db.Клиент.ToList();
					SupplierClientComboBox.ItemsSource = клиенты;
				}
				else
				{
					SupplierClientComboBox.ItemsSource = null;
				}

				SupplierClientComboBox.SelectedIndex = -1;
			}
		}

		private void AddProductButton_Click(object sender, RoutedEventArgs e)
		{
			var selectedType = (InvoiceTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

			if (string.IsNullOrEmpty(selectedType))
			{
				MessageBox.Show("Выберите тип накладной!");
				return;
			}

			// Открытие окно для выбора товара
			var selectProductWindow = new SelectProductWindow(selectedType);
			if (selectProductWindow.ShowDialog() == true)
			{
				var выбранныйСклад = selectProductWindow.ВыбранныйСклад;

				if (selectedType == "Приходная")
				{
					var товар = selectProductWindow.ВыбранныйТовар as Товар;
					var количество = selectProductWindow.Количество;

					decimal сумма = товар.ЦенаЗаЕдиницу * количество;

					// Добавление товара в DataGrid
					ItemsDataGrid.Items.Add(new
					{
						НомерТовара = товар.Номер,
						Название = товар.Название,
						Количество = количество,
						ЦенаЗаЕдиницу = товар.ЦенаЗаЕдиницу,
						Сумма = сумма,
						НомерСклада = выбранныйСклад.Номер
					});
				}
				else if (selectedType == "Расходная")
				{
					var товарНаСкладе = selectProductWindow.ВыбранныйТовар as ТоварНаСкладе;
					var количество = selectProductWindow.Количество;

					decimal сумма = товарНаСкладе.Товар.ЦенаЗаЕдиницу * количество;

					ItemsDataGrid.Items.Add(new
					{
						НомерТовара = товарНаСкладе.Товар.Номер,
						Название = товарНаСкладе.Товар.Название,
						Количество = количество,
						ЦенаЗаЕдиницу = товарНаСкладе.Товар.ЦенаЗаЕдиницу,
						Сумма = сумма
					});
				}

				UpdateTotalAmount();
			}
		}

		decimal totalAmount = 0;
		private void UpdateTotalAmount()
		{
			foreach (var item in ItemsDataGrid.Items)
			{
				var сумма = (decimal)item.GetType().GetProperty("Сумма").GetValue(item);
				totalAmount += сумма;
			}
			TotalAmountTextBlock.Text = $"Общая сумма: {totalAmount}";
		}

		private void SaveInvoiceButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var selectedType = (InvoiceTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

				// Проверка, выбран ли тип накладной
				if (string.IsNullOrEmpty(selectedType))
				{
					MessageBox.Show("Выберите тип накладной!");
					return;
				}

				// Проверка, выбран ли поставщик/клиент
				var selectedSupplierClient = SupplierClientComboBox.SelectedItem as dynamic;
				if (selectedSupplierClient == null)
				{
					MessageBox.Show("Выберите поставщика или клиента!");
					return;
				}

				// Проверка, есть ли товары в DataGrid
				if (ItemsDataGrid.Items.Count == 0)
				{
					MessageBox.Show("Добавьте хотя бы один товар!");
					return;
				}

				using (var db = new WarEntities())
				{
					if (selectedType == "Приходная")
					{
						var первыйТовар = ItemsDataGrid.Items[0];
						var номерСклада = (int)первыйТовар.GetType().GetProperty("НомерСклада").GetValue(первыйТовар);

						// Создание записи в таблице ПриходнаяНакладная
						var приходнаяНакладная = new ПриходнаяНакладная
						{
							НомерНакладной = InvoiceNumberTextBox.Text,
							ДатаПоступления = InvoiceDatePicker.SelectedDate ?? DateTime.Now,
							Поставщик = selectedSupplierClient.Номер,
							ОбщаяСумма = totalAmount
						};

						//foreach (var item in ItemsDataGrid.Items)
						//{
						//	var номерТовара = (int)item.GetType().GetProperty("НомерТовара").GetValue(item);
						//	var количество = (int)item.GetType().GetProperty("Количество").GetValue(item);

						//	var товарВНакладной = new ТоварВНакладной
						//	{
						//		НомерТовара = номерТовара,
						//		Количество = количество,
						//		Цена = (decimal)item.GetType().GetProperty("ЦенаЗаЕдиницу").GetValue(item)
						//	};
						//	приходнаяНакладная.ТоварВНакладной.Add(товарВНакладной);

						//	// Обновление или добавление данных в таблицу ТоварНаСкладе
						//	var товарНаСкладе = db.ТоварНаСкладе
						//		.FirstOrDefault(t => t.НомерТовара == номерТовара && t.НомерСклада == номерСклада);

						//	if (товарНаСкладе != null)
						//	{
						//		// Если запись найдена, обновляем количество
						//		товарНаСкладе.Количество += количество;
						//	}
						//	else
						//	{
						//		// Если записи нет, создаем новую запись
						//		var новыйТоварНаСкладе = new ТоварНаСкладе
						//		{
						//			НомерТовара = номерТовара,
						//			НомерСклада = номерСклада,
						//			Количество = количество
						//		};
						//		db.ТоварНаСкладе.Add(новыйТоварНаСкладе);
						//	}
						//}

						// Добавление товаров в таблицу ТоварВНакладной
						foreach (var item in ItemsDataGrid.Items)
						{

							var товарВНакладной = new ТоварВНакладной
							{
								НомерТовара = (int)item.GetType().GetProperty("НомерТовара").GetValue(item),
								Количество = (int)item.GetType().GetProperty("Количество").GetValue(item),
								Цена = (decimal)item.GetType().GetProperty("ЦенаЗаЕдиницу").GetValue(item)
							};
							приходнаяНакладная.ТоварВНакладной = товарВНакладной;
						}

						db.ПриходнаяНакладная.Add(приходнаяНакладная);
					}
					else if (selectedType == "Расходная")
					{
						// Создание записи в таблице РасходнаяНакладная
						var расходнаяНакладная = new РасходнаяНакладная
						{
							НомерНакладной = InvoiceNumberTextBox.Text,
							ДатаОтгрузки = InvoiceDatePicker.SelectedDate ?? DateTime.Now,
							Клиент = selectedSupplierClient.Номер,
							ОбщаяСумма = totalAmount
						};

						// Добавление товаров в таблицу ТоварВНакладной
						foreach (var item in ItemsDataGrid.Items)
						{
							var товарВНакладной = new ТоварВНакладной
							{
								НомерТовара = (int)item.GetType().GetProperty("НомерТовара").GetValue(item),
								Количество = (int)item.GetType().GetProperty("Количество").GetValue(item),
								Цена = (decimal)item.GetType().GetProperty("ЦенаЗаЕдиницу").GetValue(item)
							};
							расходнаяНакладная.ТоварВНакладной = товарВНакладной;

							// Обновление количества товара на складе
							var товарНаСкладе = db.ТоварНаСкладе.FirstOrDefault(t => t.НомерТовара == товарВНакладной.НомерТовара);
							if (товарНаСкладе != null)
							{
								товарНаСкладе.Количество -= товарВНакладной.Количество;
							}
						}

						db.РасходнаяНакладная.Add(расходнаяНакладная);
					}

					db.SaveChanges();
				}
				MessageBox.Show("Накладная успешно сохранена!");

				ResetForm();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении накладной: {ex.Message}");
			}
		}

		// Метод для сброса всех полей формы
		private void ResetForm()
		{
			InvoiceNumberTextBox.Text = string.Empty;
			InvoiceDatePicker.SelectedDate = null;
			TotalAmountTextBlock.Text = "Общая сумма: 0";

			InvoiceTypeComboBox.SelectedIndex = -1;
			SupplierClientComboBox.SelectedIndex = -1;
			SupplierClientComboBox.ItemsSource = null;

			ItemsDataGrid.Items.Clear();

			totalAmount = 0;
		}
	}
}