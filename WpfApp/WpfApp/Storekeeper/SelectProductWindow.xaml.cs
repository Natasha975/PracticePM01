using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp.Storekeeper
{
	/// <summary>
	/// Логика взаимодействия для SelectProductWindow.xaml
	/// </summary>
	public partial class SelectProductWindow : Window
	{
		public object ВыбранныйТовар { get; private set; } // Может быть Товар или ТоварНаСкладе
		public int Количество { get; private set; }
		private string ТипНакладной { get; set; }
		public Склад ВыбранныйСклад { get; private set; }

		public SelectProductWindow(string типНакладной)
		{
			InitializeComponent();
			ТипНакладной = типНакладной;
			LoadWarehouses();
			UpdateProductComboBoxVisibility();
		}

		private void LoadWarehouses()
		{
			using (var db = new WarEntities())
			{
				WarehouseComboBox.ItemsSource = db.Склад.ToList();
			}
		}

		private void UpdateProductComboBoxVisibility()
		{
			if (ТипНакладной == "Приходная")
			{
				ProductComboBoxIncome.Visibility = Visibility.Visible;
				LabelIncome.Visibility = Visibility.Visible;
			}
			else if (ТипНакладной == "Расходная")
			{
				LabelOutcome.Visibility = Visibility.Visible;
				ProductComboBoxOutcome.Visibility = Visibility.Visible;
			}
		}

		private void WarehouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (WarehouseComboBox.SelectedItem is Склад выбранныйСклад)
			{
				using (var db = new WarEntities())
				{
					if (ТипНакладной == "Приходная")
					{
						// Загрузка всех товаров из таблицы "Товар"
						ProductComboBoxIncome.ItemsSource = db.Товар.ToList();
					}
					else if (ТипНакладной == "Расходная")
					{
						// Загрузка товаров на выбранном складе из таблицы "ТоварНаСкладе"
						ProductComboBoxOutcome.ItemsSource = db.ТоварНаСкладе
							.Include("Товар")
							.Where(t => t.НомерСклада == выбранныйСклад.Номер)
							.ToList();
					}
				}
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if (ТипНакладной == "Приходная" && ProductComboBoxIncome.SelectedItem == null)
			{
				MessageBox.Show("Выберите товар!");
				return;
			}

			if (ТипНакладной == "Расходная" && ProductComboBoxOutcome.SelectedItem == null)
			{
				MessageBox.Show("Выберите товар!");
				return;
			}

			if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
			{
				MessageBox.Show("Введите корректное количество!");
				return;
			}

			ВыбранныйСклад = WarehouseComboBox.SelectedItem as Склад;

			if (ТипНакладной == "Приходная")
			{
				ВыбранныйТовар = ProductComboBoxIncome.SelectedItem as Товар;
			}
			else if (ТипНакладной == "Расходная")
			{
				var товарНаСкладе = ProductComboBoxOutcome.SelectedItem as ТоварНаСкладе;
				if (quantity > товарНаСкладе.Количество)
				{
					MessageBox.Show("Количество не может превышать доступное на складе!");
					return;
				}
				ВыбранныйТовар = товарНаСкладе;
			}

			Количество = quantity;
			DialogResult = true;
			Close();
		}
	}
}
