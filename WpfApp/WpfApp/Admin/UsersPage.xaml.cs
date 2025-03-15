using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp.Admin
{
	/// <summary>
	/// Логика взаимодействия для UsersPage.xaml
	/// </summary>
	public partial class UsersPage : Page
	{
		private WarEntities db;
		public ObservableCollection<Роль> Роли { get; set; }

		public UsersPage()
		{
			InitializeComponent();
			db = new WarEntities();
			this.DataContext = this;
			ЗагрузитьРоли();

			var пользователи = (from пользователь in db.Пользователь
								join роль in db.Роль on пользователь.Роль equals роль.Номер
								select new ПользовательData
								{
									Номер = пользователь.Номер,
									Фамилия = пользователь.Фамилия,
									Имя = пользователь.Имя,
									Отчество = пользователь.Отчество,
									Роль = роль,
									Логин = пользователь.Логин,
									Пароль = пользователь.ХэшПароль,
								}).ToList();

			ПользователиDataGrid.ItemsSource = пользователи;
		}

		public void ЗагрузитьРоли()
		{
			var РолиИзБазы = db.Роль.ToList();
			Роли = new ObservableCollection<Роль>(РолиИзБазы);
		}

		private void СохранитьИзмененияПользователи()
		{
			var измененныеПользователь = ПользователиDataGrid.ItemsSource as List<ПользовательData>;
			if (измененныеПользователь != null)
			{
				foreach (var данные in измененныеПользователь)
				{
					var пользователь = db.Пользователь.FirstOrDefault(p => p.Номер == данные.Номер);
					if (пользователь != null)
					{
						пользователь.Фамилия = данные.Фамилия;
						пользователь.Имя = данные.Имя;
						пользователь.Отчество = данные.Отчество;
						пользователь.Логин = данные.Логин;
						пользователь.ХэшПароль = данные.Пароль;

						var роль = db.Роль.FirstOrDefault(r => r.Номер == данные.Роль.Номер);
						if (роль != null)
						{
							пользователь.Роль = роль.Номер;
						}
					}
					else
					{
						var новыйПользователь = new Пользователь
						{
							Фамилия = данные.Фамилия,
							Имя = данные.Имя,
							Отчество = данные.Отчество,
							Логин = данные.Логин,
							ХэшПароль = данные.Пароль,
							Роль = db.Роль.FirstOrDefault(r => r.Номер == данные.Роль.Номер).Номер
						};
						db.Пользователь.Add(новыйПользователь);
					}
				}
				db.SaveChanges();

			}
		}

		private void СохранитьПользователи_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				СохранитьИзмененияПользователи();
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
