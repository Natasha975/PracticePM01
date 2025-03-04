using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp.Model;
using WpfApp.Admin;
using static QRCoder.PayloadGenerator;
using static WpfApp.SalesManagerWindow;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		private WarEntities db;
		public ObservableCollection<ТипCклад> ТипыСкладов { get; set; }

		public AdminWindow()
		{
			InitializeComponent();
			db = new WarEntities();
			LoadData();
			ЗагрузитьТипыСкладов();
			this.DataContext = this;

			var склады = (from склад in db.Склад
						  join адрес in db.Адрес on склад.Адрес equals адрес.Номер
						  join типСклада in db.ТипCклад on склад.ТипСклада equals типСклада.Номер
						  join зоныХранения in db.ЗоныХранения on склад.ЗоныХранения equals зоныХранения.Номер into зоныХраненияGroup
						  from зоныХранения in зоныХраненияGroup.DefaultIfEmpty()
						  select new СкладData
						  {
							  Номер = склад.Номер,
							  НазваниеСклада = склад.НазваниеСклада,
							  Субъект = адрес.Субъект,
							  Улица = адрес.Улица,
							  Дом = адрес.Дом,
							  ТипСклада = типСклада, // Использование объекта ТипCклад
							  ЗоныХранения = зоныХранения != null ? зоныХранения.Наименование : "Не указано"
						  }).ToList();

			СкладыDataGrid.ItemsSource = склады;
		}

		public void ЗагрузитьТипыСкладов()
		{
			var типыСкладовИзБазы = db.ТипCклад.ToList();
			ТипыСкладов = new ObservableCollection<ТипCклад>(типыСкладовИзБазы);
		}

		private void LoadData()
		{
			//var склады = (from склад in db.Склад
			//			  join адрес in db.Адрес on склад.Адрес equals адрес.Номер
			//			  join типСклада in db.ТипCклад on склад.ТипСклада equals типСклада.Номер
			//			  join зоныХранения in db.ЗоныХранения on склад.ЗоныХранения equals зоныХранения.Номер into зоныХраненияGroup
			//			  from зоныХранения in зоныХраненияGroup.DefaultIfEmpty()
			//			  select new СкладData
			//			  {
			//				  Номер = склад.Номер,
			//				  НазваниеСклада = склад.НазваниеСклада,
			//				  Субъект = адрес.Субъект,
			//				  Улица = адрес.Улица,
			//				  Дом = адрес.Дом,
			//				  ТипСклада = типСклада.Наименование,
			//				  ЗоныХранения = зоныХранения != null ? зоныХранения.Наименование : "Не указано"
			//			  }).ToList();
			

			var товары = db.Товар.ToList();
			ТоварыDataGrid.ItemsSource = товары;

			var клиенты = (from клиент in db.Клиент
						  join адрес in db.Адрес on клиент.Адрес equals адрес.Номер
						  select new КлиентData
						  {
							  Номер = клиент.Номер,
							  Название = клиент.Название,
							  Tелефон = клиент.Tелефон,
							  Email = клиент.Email,
							  Субъект = адрес.Субъект,
							  Улица = адрес.Улица,
							  Дом = адрес.Дом,
						  }).ToList();

			КлиентыDataGrid.ItemsSource = клиенты;

			var поставщики = (from поставщик in db.Поставщики
						   join адрес in db.Адрес on поставщик.Адрес equals адрес.Номер
						   select new ПоставщикData
						   {
							   Номер = поставщик.Номер,
							   Название = поставщик.Название,
							   ИННКПП = поставщик.ИННКПП,
							   Tелефон = поставщик.Tелефон,
							   Email = поставщик.Email,
							   Субъект = адрес.Субъект,
							   Улица = адрес.Улица,
							   Дом = адрес.Дом,
						   }).ToList();

			ПоставщикиDataGrid.ItemsSource = поставщики;

			var пользователи = (from пользователь in db.Пользователь
							  join роль in db.Роль on пользователь.Роль equals роль.Номер
							  select new ПользовательData
							  {
								  Номер = пользователь.Номер,
								  Фамилия = пользователь.Фамилия,
								  Имя = пользователь.Имя,
								  Отчество = пользователь.Отчество,
								  Роль = роль.Наименование,
								  Логин = пользователь.Логин,
								  Пароль = пользователь.Пароль,
							  }).ToList();

			ПользователиDataGrid.ItemsSource = пользователи;
		}

		private void СохранитьИзмененияСклад()
		{
			//// Получение измененний данных из DataGrid
			//var измененныеДанные = СкладыDataGrid.ItemsSource as List<СкладData>;
			//if (измененныеДанные != null)
			//{
			//	foreach (var данные in измененныеДанные)
			//	{
			//		// Нахождение записи склада в БД
			//		var склад = db.Склад.FirstOrDefault(s => s.Номер == данные.Номер);
			//		if (склад != null)
			//		{
			//			// Обновление названия склада
			//			склад.НазваниеСклада = данные.НазваниеСклада;

			//			// Обновление адреса
			//			var адрес = db.Адрес.FirstOrDefault(a => a.Номер == склад.Адрес);
			//			if (адрес != null)
			//			{
			//				адрес.Субъект = данные.Субъект;
			//				адрес.Улица = данные.Улица;
			//				адрес.Дом = данные.Дом;
			//			}

			//			// Обновление типа склада
			//			var типСклада = db.ТипCклад.FirstOrDefault(t => t.Наименование == данные.ТипСклада);
			//			if (типСклада != null)
			//			{
			//				склад.ТипСклада = типСклада.Номер;
			//			}

			//			// Обновление зоны хранения
			//			var зоныХранения = db.ЗоныХранения.FirstOrDefault(z => z.Наименование == данные.ЗоныХранения);
			//			if (зоныХранения != null)
			//			{
			//				склад.ЗоныХранения = зоныХранения.Номер;
			//			}
			//			else if (данные.ЗоныХранения == "Не указано")
			//			{
			//				// Если зона хранения не указана, сбрасываем значение
			//				склад.ЗоныХранения = null;
			//			}
			//		}
			//	}
			//	db.SaveChanges();
			//}

			// Получение измененных данных из DataGrid
			var измененныеДанные = СкладыDataGrid.ItemsSource as List<СкладData>;
			if (измененныеДанные != null)
			{
				foreach (var данные in измененныеДанные)
				{
					// Нахождение записи склада в БД
					var склад = db.Склад.FirstOrDefault(s => s.Номер == данные.Номер);
					if (склад != null)
					{
						// Обновление названия склада
						склад.НазваниеСклада = данные.НазваниеСклада;

						// Обновление адреса
						var адрес = db.Адрес.FirstOrDefault(a => a.Номер == склад.Адрес);
						if (адрес != null)
						{
							адрес.Субъект = данные.Субъект;
							адрес.Улица = данные.Улица;
							адрес.Дом = данные.Дом;
						}

						// Обновление типа склада
						var типСклада = db.ТипCклад.FirstOrDefault(t => t.Номер == данные.ТипСклада.Номер);
						if (типСклада != null)
						{
							склад.ТипСклада = типСклада.Номер;
						}

						// Обновление зоны хранения
						var зоныХранения = db.ЗоныХранения.FirstOrDefault(z => z.Наименование == данные.ЗоныХранения);
						if (зоныХранения != null)
						{
							склад.ЗоныХранения = зоныХранения.Номер;
						}
						else if (данные.ЗоныХранения == "Не указано")
						{
							// Если зона хранения не указана, сбрасываем значение
							склад.ЗоныХранения = null;
						}
					}
					else
					{
						// Если склад новый (например, добавлен через CanUserAddRows)
						var новыйСклад = new WpfApp.Model.Склад
						{
							НазваниеСклада = данные.НазваниеСклада,
							Адрес = db.Адрес.FirstOrDefault(a => a.Субъект == данные.Субъект && a.Улица == данные.Улица && a.Дом == данные.Дом).Номер,
							ТипСклада = db.ТипCклад.FirstOrDefault(t => t.Номер == данные.ТипСклада.Номер).Номер,
							ЗоныХранения = db.ЗоныХранения.FirstOrDefault(z => z.Наименование == данные.ЗоныХранения)?.Номер
						};
						db.Склад.Add(новыйСклад);
					}
				}

				// Сохранение изменений в базе данных
				db.SaveChanges();
			}
		}

		private void СохранитьСклады_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				СохранитьИзмененияСклад();
				MessageBox.Show("Изменения сохранены.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
			}
		}

		private void УдалитьСклад_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = СкладыDataGrid.SelectedItem as dynamic;
			if (selectedItem != null)
			{
				try
				{
					int номерСклада = selectedItem.Номер;
					var склад = db.Склад.Find(номерСклада);
					if (склад != null)
					{
						db.Склад.Remove(склад);
						db.SaveChanges();
						LoadData();
						MessageBox.Show("Запись успешно удалена.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при удалении: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Выберите запись для удаления.");
			}
		}

		private void СохранитьИзмененияТовар()
		{
			try
			{
				var измененныеТовары = ТоварыDataGrid.ItemsSource as IEnumerable<Товар>;
				if (измененныеТовары != null)
				{
					foreach (var товар in измененныеТовары)
					{
						if (товар.Номер == 0)
						{
							db.Товар.Add(товар);
						}
						else
						{
							var существующийТовар = db.Товар.Find(товар.Номер);
							if (существующийТовар != null)
							{
								db.Entry(существующийТовар).CurrentValues.SetValues(товар);
							}
						}
					}
					db.SaveChanges();					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
			}			
		}	

		private void СохранитьТовары_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				СохранитьИзмененияТовар();
				MessageBox.Show("Изменения сохранены.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
			}
		}

		private void УдалитьТовар_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = ТоварыDataGrid.SelectedItem as dynamic;
			if (selectedItem != null)
			{
				try
				{
					int номерТовара = selectedItem.Номер;
					var товар = db.Товар.Find(номерТовара);
					if (товар != null)
					{
						db.Товар.Remove(товар);
						db.SaveChanges();
						LoadData();
						MessageBox.Show("Запись успешно удалена.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при удалении: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Выберите запись для удаления.");
			}
		}

		private void СохранитьИзмененияКлиент()
		{
			var измененныеКлиент = КлиентыDataGrid.ItemsSource as List<КлиентData>;
			if (измененныеКлиент != null)
			{
				foreach (var данные in измененныеКлиент)
				{
					var клиент = db.Клиент.FirstOrDefault(s => s.Номер == данные.Номер);
					if (клиент != null)
					{
						клиент.Название = данные.Название;
						клиент.Tелефон = данные.Tелефон;
						клиент.Email = данные.Email;

						var адрес = db.Адрес.FirstOrDefault(a => a.Номер == клиент.Адрес);
						if (адрес != null)
						{
							адрес.Субъект = данные.Субъект;
							адрес.Улица = данные.Улица;
							адрес.Дом = данные.Дом;
						}
					}
				}
				db.SaveChanges();
			}
		}

		private void СохранитьКлиенты_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				СохранитьИзмененияКлиент();
				MessageBox.Show("Изменения сохранены.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
			}
		}

		private void УдалитьКлиента_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = КлиентыDataGrid.SelectedItem as dynamic;
			if (selectedItem != null)
			{
				try
				{
					int номерКлиента = selectedItem.Номер;
					var клиент = db.Клиент.Find(номерКлиента);
					if (клиент != null)
					{
						db.Клиент.Remove(клиент);
						db.SaveChanges();
						LoadData();
						MessageBox.Show("Запись успешно удалена.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при удалении: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Выберите запись для удаления.");
			}
		}

		private void СохранитьИзмененияПоставщик()
		{
			var измененныеПоставщик = ПоставщикиDataGrid.ItemsSource as List<ПоставщикData>;
			if (измененныеПоставщик != null)
			{
				foreach (var данные in измененныеПоставщик)
				{
					var поставщик = db.Поставщики.FirstOrDefault(s => s.Номер == данные.Номер);
					if (поставщик != null)
					{
						поставщик.Название = данные.Название;
						поставщик.ИННКПП = данные.ИННКПП;
						поставщик.Tелефон = данные.Tелефон;
						поставщик.Email = данные.Email;

						var адрес = db.Адрес.FirstOrDefault(a => a.Номер == поставщик.Адрес);
						if (адрес != null)
						{
							адрес.Субъект = данные.Субъект;
							адрес.Улица = данные.Улица;
							адрес.Дом = данные.Дом;
						}
					}
				}
				db.SaveChanges();
			}
		}

		private void СохранитьПоставщики_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				СохранитьИзмененияПоставщик();
				MessageBox.Show("Изменения сохранены.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
			}
		}

		private void УдалитьПоставщика_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = ПоставщикиDataGrid.SelectedItem as dynamic;
			if (selectedItem != null)
			{
				try
				{
					int номерПоставщика = selectedItem.Номер;
					var поставщик = db.Поставщики.Find(номерПоставщика);
					if (поставщик != null)
					{
						db.Поставщики.Remove(поставщик);
						db.SaveChanges();
						LoadData();
						MessageBox.Show("Запись успешно удалена.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при удалении: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Выберите запись для удаления.");
			}
		}

		private void СохранитьПользователи_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				db.SaveChanges();
				MessageBox.Show("Изменения сохранены.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
			}
		}

		private void УдалитьПользователя_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = ПользователиDataGrid.SelectedItem as dynamic;
			if (selectedItem != null)
			{
				try
				{
					int номерПользователя = selectedItem.Номер;
					var пользователь = db.Пользователь.Find(номерПользователя);
					if (пользователь != null)
					{
						db.Пользователь.Remove(пользователь);
						db.SaveChanges();
						LoadData();
						MessageBox.Show("Запись успешно удалена.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при удалении: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Выберите запись для удаления.");
			}
		}	
	}
}