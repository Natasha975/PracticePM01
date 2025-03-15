using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp.Accountant
{
	/// <summary>
	/// Логика взаимодействия для InventoryReportPage.xaml
	/// </summary>
	public partial class InventoryReportPage : Page
	{
		private Товар selectedProduct;

		public InventoryReportPage()
		{
			InitializeComponent();
			LoadProducts();
			LoadInventoryResults();
		}

		// Загрузка списка товаров в ComboBox
		private void LoadProducts()
		{
			using (var db = new WarEntities())
			{
				var products = db.Товар.ToList();

				// Заполняем ComboBox
				ProductComboBox.ItemsSource = products;
				ProductComboBox.DisplayMemberPath = "Название";
			}
		}

		// Загрузка результатов инвентаризации в DataGrid
		private void LoadInventoryResults()
		{
			using (var db = new WarEntities())
			{
				var results = from резИнвентаризации in db.РезИнвентаризации
							  join товар in db.Товар on резИнвентаризации.Товар equals товар.Номер
							  join инвентаризация in db.Инвентаризация on резИнвентаризации.Номер equals инвентаризация.Результаты
							  select new
							  {
								  Дата = инвентаризация.ДатаПроведения,
								  Название = товар.Название,
								  ОжидаемыйРез = резИнвентаризации.ОжидаемыйРез,
								  ФактическийРез = резИнвентаризации.ФактическийРез,
								  Расхождение = резИнвентаризации.Расхождение
							  };

				InventoryDataGrid.ItemsSource = results.ToList();
			}
		}

		// Обработчик выбора товара в ComboBox
		private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			selectedProduct = ProductComboBox.SelectedItem as Товар;

			if (selectedProduct != null)
			{
				using (var db = new WarEntities())
				{
					var expectedQuantity = (from товарНаСкладе in db.ТоварНаСкладе
											join товар in db.Товар on товарНаСкладе.НомерТовара equals товар.Номер
											where товар.Название == selectedProduct.Название
											select товарНаСкладе.Количество).DefaultIfEmpty(0).Sum();

					ExpectedQuantityTextBox.Text = expectedQuantity.ToString();
				}
			}
			//_selectedProduct = ProductComboBox.SelectedItem as Товар;

			//if (_selectedProduct != null)
			//{
			//	// Загружаем ожидаемое количество товара на складах
			//	using (var db = new WarEntities())
			//	{
			//		//var results = from резИнвентаризации in db.РезИнвентаризации
			//		//			  join товар in db.Товар on резИнвентаризации.Товар equals товар.Номер
			//		//			  join инвентаризация in db.Инвентаризация on резИнвентаризации.Номер equals инвентаризация.Результаты
			//		//			  select new
			//		//			  {
			//		//				  Название = товар.Название,
			//		//				  ОжидаемыйРез = резИнвентаризации.ОжидаемыйРез,
			//		//				  ФактическийРез = резИнвентаризации.ФактическийРез,
			//		//				  Расхождение = резИнвентаризации.Расхождение
			//		//			  };

			//		//var expectedQuantity = from товарНаСкладе in db.ТоварНаСкладе
			//		//					   where товарНаСкладе.НомерТовара = _selectedProduct

			//		//						//.Where(t => t.Товар == _selectedProduct.Номер) // Исправлено: сравниваем Номер товара
			//		//						//.Sum(t => t.Количество);

			//		//ExpectedQuantityTextBox.Text = expectedQuantity.ToString();

			//		// Используем LINQ для получения ожидаемого количества товара на складе
			//		var expectedQuantity = (from товарНаСкладе in db.ТоварНаСкладе
			//								join товар in db.Товар on товарНаСкладе.Товар equals товар.Номер // Связываем таблицы
			//								where товар.Название == _selectedProduct.Название // Сравниваем по названию
			//								select товарНаСкладе.Количество).Sum();

			//		// Выводим результат в TextBox
			//		ExpectedQuantityTextBox.Text = expectedQuantity.ToString();
			//	}
			//}
		}

		// Обработчик ввода фактического количества
		private void ActualQuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (int.TryParse(ActualQuantityTextBox.Text, out int actualQuantity))
			{
				int expectedQuantity = int.Parse(ExpectedQuantityTextBox.Text);
				int discrepancy =  expectedQuantity-actualQuantity;

				DiscrepancyTextBox.Text = discrepancy.ToString();
			}
		}

		private void SaveResults_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (selectedProduct == null)
				{
					MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				if (!int.TryParse(ActualQuantityTextBox.Text, out int actualQuantity))
				{
					MessageBox.Show("Введите корректное фактическое количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				// Получение ожидаемого количества
				int expectedQuantity = int.Parse(ExpectedQuantityTextBox.Text);

				int discrepancy = expectedQuantity - actualQuantity;

				// Сохранение результатов в базу данных
				using (var db = new WarEntities())
				{
					var result = new РезИнвентаризации
					{
						Товар = selectedProduct.Номер,
						ОжидаемыйРез = expectedQuantity,
						ФактическийРез = actualQuantity,
						Расхождение = discrepancy
					};

					db.РезИнвентаризации.Add(result);
					db.SaveChanges();

					int результатИнвентаризации = result.Номер;

					var inventory = new Инвентаризация
					{
						ДатаПроведения = DateTime.Now,
						Ответственный = 1,
						Результаты = результатИнвентаризации
					};

					db.Инвентаризация.Add(inventory);
					db.SaveChanges();
									  
					
					//// Создаем запись в результатах инвентаризации
									  //var result = new РезИнвентаризации
									  //{
									  //	Товар = _selectedProduct.Номер,
									  //	ОжидаемыйРез = expectedQuantity,
									  //	ФактическийРез = actualQuantity,
									  //	Расхождение = discrepancy
									  //};

					//db.РезИнвентаризации.Add(result);
					//db.SaveChanges(); // Сохраняем все изменения


					//// Создаем запись о проведении инвентаризации
					//var inventory = new Инвентаризация
					//{
					//	ДатаПроведения = DateTime.Now,
					//	Ответственный = 1, // ID ответственного (можно заменить на реальный ID)
					//	Результаты = 1 // Временно, будет обновлено после расчета
					//};

					//db.Инвентаризация.Add(inventory);
					//db.SaveChanges(); // Сохраняем, чтобы получить ID инвентаризации
				}

				// Обновляем данные в DataGrid
				LoadInventoryResults();

				MessageBox.Show("Результаты сохранены успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


		//private void LoadData()
		//{
		//	using (var db = new WarEntities())
		//	{
		//		var q = from резИнвентаризации in db.РезИнвентаризации
		//				join товар in db.Товар on резИнвентаризации.Товар equals товар.Номер
		//				join инвентаризация in db.Инвентаризация on резИнвентаризации.Номер equals инвентаризация.Результаты
		//				select new
		//				{
		//					Дата = инвентаризация.ДатаПроведения,
		//					Название = товар.Название,
		//					ОжидаемыйРез = резИнвентаризации.ОжидаемыйРез,
		//					ФактическийРез = резИнвентаризации.ФактическийРез,
		//					Расхождение = резИнвентаризации.Расхождение
		//				};

		//		InventoryDataGrid.ItemsSource = q.ToList();
		//	}
		//}

		//private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		//{
		//	// Получаем текст из TextBox
		//	string searchText = SearchTextBox.Text.ToLower(); 

		//	using (var db = new WarEntities())
		//	{
		//		var q = from резИнвентаризации in db.РезИнвентаризации
		//				join товар in db.Товар on резИнвентаризации.Товар equals товар.Номер
		//				join инвентаризация in db.Инвентаризация on резИнвентаризации.Номер equals инвентаризация.Результаты
		//				where товар.Название.ToLower().Contains(searchText)
		//				select new
		//				{
		//					Дата = инвентаризация.ДатаПроведения,
		//					Название = товар.Название,
		//					ОжидаемыйРез = резИнвентаризации.ОжидаемыйРез,
		//					ФактическийРез = резИнвентаризации.ФактическийРез,
		//					Расхождение = резИнвентаризации.Расхождение
		//				};

		//		InventoryDataGrid.ItemsSource = q.ToList();
		//	}
		//}
	}
}