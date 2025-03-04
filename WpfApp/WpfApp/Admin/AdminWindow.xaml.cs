using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp.Model;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		private WarEntities db;

		public AdminWindow()
		{
			InitializeComponent();
			db = new WarEntities();
			LoadData();
		}

		private void LoadData()
		{
			//var прих = from ds in db.Склад
			//		   join seb in db.ТипCклад on ds.ТипСклада equals seb.Номер
			//		   join sebyt in db.ЗоныХранения on ds.ЗоныХранения equals sebyt.Номер
			//		   select new
			//		   {
			//			   НомерСклад = ds.Номер, // Исправлено: НомерСклад вместо НомерСклада
			//			   НазваниеСклад = ds.НазваниеСклада,
			//			   ТипСклада = seb.Наименование, // Исправлено: ТипСклада вместо ТипCклад
			//			   Наим = sebyt.Наименование // Исправлено: sebyt.Наименование вместо seb.Наименование
			//		   };
			//СкладыDataGrid.ItemsSource = прих.ToList();
			// Загрузка данных для вкладки "Склады"
			var склады = (from склад in db.Склад
						  join адрес in db.Адрес on склад.Адрес equals адрес.Номер
						  join типСклада in db.ТипCклад on склад.ТипСклада equals типСклада.Номер
						  join зоныХранения in db.ЗоныХранения on склад.ЗоныХранения equals зоныХранения.Номер into зоныХраненияGroup
						  from зоныХранения in зоныХраненияGroup.DefaultIfEmpty() // LEFT JOIN для ЗоныХранения
						  select new
						  {
							  Номер = склад.Номер,
							  НазваниеСклада = склад.НазваниеСклада,
							  Адрес = адрес, // Загружаем весь объект адреса
							  ТипСклада = типСклада.Наименование,
							  ЗоныХранения = зоныХранения != null ? зоныХранения.Наименование : "Не указано"
						  })
				 .AsEnumerable() // Преобразуем в объекты .NET
				 .Select(x => new
				 {
					 x.Номер,
					 x.НазваниеСклада,
					 Адрес = $"{x.Адрес.Страна}, {x.Адрес.Субъект}, {x.Адрес.Город}, {x.Адрес.Улица}, {x.Адрес.Дом}",
					 x.ТипСклада,
					 x.ЗоныХранения
				 });

			СкладыDataGrid.ItemsSource = склады.ToList();

			//СкладыDataGrid.ItemsSource = db.Склад.ToList();
			ТоварыDataGrid.ItemsSource = db.Товар.ToList();
			КлиентыDataGrid.ItemsSource = db.Клиент.ToList();
			ПоставщикиDataGrid.ItemsSource = db.Поставщики.ToList();
			ПользователиDataGrid.ItemsSource = db.Пользователь.ToList();
		}



		private void СохранитьСклады_Click(object sender, RoutedEventArgs e)
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
						LoadData(); // Перезагружаем данные после удаления
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

		private void СохранитьТовары_Click(object sender, RoutedEventArgs e)
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

		private void СохранитьКлиенты_Click(object sender, RoutedEventArgs e)
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

		private void СохранитьПоставщики_Click(object sender, RoutedEventArgs e)
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
