using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Model;

namespace WpfApp.Admin
{
	/// <summary>
	/// Логика взаимодействия для ProductsPage.xaml
	/// </summary>
	public partial class ProductsPage : Page
	{
		private WarEntities db;

		public ProductsPage()
		{
			InitializeComponent();
			db = new WarEntities();
			LoadData();

		}
		private void LoadData()
		{
			var товары = db.Товар.ToList();
			ТоварыDataGrid.ItemsSource = товары;
		}

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!char.IsDigit(e.Text, e.Text.Length - 1))
			{
				e.Handled = true;
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
	}
}
