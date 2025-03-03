using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Model;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для RegClientWindow.xaml
	/// </summary>
	public partial class RegClientWindow : Window
	{
		public RegClientWindow()
		{
			InitializeComponent();
			LoadData();
		}

		private List<string> GetRussianRegions()
		{
			return new List<string>
			{
				"Республика Адыгея",
				"Республика Башкортостан",
				"Республика Бурятия",
				"Республика Алтай",
				"Республика Дагестан",
				"Республика Ингушетия",
				"Кабардино-Балкарская Республика",
				"Республика Калмыкия",
				"Карачаево-Черкесская Республика",
				"Республика Карелия",
				"Республика Коми",
				"Республика Марий Эл",
				"Республика Мордовия",
				"Республика Саха (Якутия)",
				"Республика Северная Осетия - Алания",
				"Республика Татарстан (Татарстан)",
				"Республика Тыва",
				"Удмуртская Республика",
				"Республика Хакасия",
				"Чеченская Республика",
				"Чувашская Республика - Чувашия",
				"Алтайский край",
				"Краснодарский край",
				"Красноярский край",
				"Приморский край",
				"Ставропольский край",
				"Хабаровский край",
				"Амурская область",
				"Архангельская область",
				"Астраханская область",
				"Белгородская область",
				"Брянская область",
				"Владимирская область",
				"Волгоградская область",
				"Вологодская область",
				"Воронежская область",
				"Ивановская область",
				"Иркутская область",
				"Калининградская область",
				"Калужская область",
				"Камчатский край",
				"Кемеровская область - Кузбасс",
				"Кировская область",
				"Костромская область",
				"Курганская область",
				"Курская область",
				"Ленинградская область",
				"Липецкая область",
				"Магаданская область",
				"Московская область",
				"Мурманская область",
				"Нижегородская область",
				"Новгородская область",
				"Новосибирская область",
				"Омская область",
				"Оренбургская область",
				"Орловская область",
				"Пензенская область",
				"Пермский край",
				"Псковская область",
				"Ростовская область",
				"Рязанская область",
				"Самарская область",
				"Саратовская область",
				"Сахалинская область",
				"Свердловская область",
				"Смоленская область",
				"Тамбовская область",
				"Тверская область",
				"Томская область",
				"Тульская область",
				"Тюменская область",
				"Ульяновская область",
				"Челябинская область",
				"Забайкальский край",
				"Ярославская область",
				"г. Москва",
				"г. Санкт-Петербург",
				"Еврейская автономная область",
				"Ненецкий автономный округ",
				"Ханты-Мансийский автономный округ - Югра",
				"Чукотский автономный округ",
				"Ямало-Ненецкий автономный округ",
				"Республика Крым",
				"г. Севастополь",
				"иные территории, включая город и космодром Байконур",
			};
		}

		private void LoadData()
		{
			try
			{
				cmbRegion.ItemsSource = GetRussianRegions();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке адресов: {ex.Message}");
			}
		}

		private void RegClick_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var selectedType = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

				var адрес = new Адрес
				{
					Страна = "Россия",
					Субъект = cmbRegion.SelectedItem?.ToString(),
					Город = txtCity.Text,
					Улица = txtStreet.Text,
					Дом = int.TryParse(txtHouse.Text, out int house) ? house : 0
				};

				using (var db = new WarEntities())
				{
					db.Адрес.Add(адрес);
					db.SaveChanges();

					if (selectedType == "Поставщик")
					{
						var поставщик = new Поставщики
						{
							Название = NameTextBox.Text,
							ИННКПП = INNKPPTextBox.Text,
							Tелефон = PhoneTextBox.Text,
							Email = EmailTextBox.Text,
							Адрес = адрес.Номер
						};
						db.Поставщики.Add(поставщик);
					}
					else if (selectedType == "Клиент")
					{
						var клиент = new Клиент
						{
							Название = NameTextBox.Text,
							Tелефон = PhoneTextBox.Text,
							Email = EmailTextBox.Text,
							Адрес = адрес.Номер
						};
						db.Клиент.Add(клиент);
					}

					db.SaveChanges();
				}

				MessageBox.Show("Данные успешно сохранены!");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}");
			}
		}

		//string selectedType = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
		//var selectedAddress = AddressComboBox.SelectedItem as Адрес;

		//if (selectedAddress == null)
		//{
		//	MessageBox.Show("Выберите адрес");
		//	return;
		//}

		//if (selectedType == "Клиент")
		//{
		//	var newClient = new Клиент
		//	{
		//		Название = NameTextBox.Text,
		//		Tелефон = PhoneTextBox.Text,
		//		Email = EmailTextBox.Text,
		//		Адрес = selectedAddress.Номер,
		//	};

		//	db.Клиент.Add(newClient);
		//}
		//else if (selectedType == "Поставщик")
		//{
		//	var newSupplier = new Поставщики
		//	{
		//		Название = NameTextBox.Text,
		//		Tелефон = PhoneTextBox.Text,
		//		Email = EmailTextBox.Text,
		//		Адрес = selectedAddress.Номер
		//	};

		//	db.Поставщики.Add(newSupplier);
		//}
		//else
		//{
		//	MessageBox.Show("Выберите тип (Клиент или Поставщик)");
		//	return;
		//}

		//db.SaveChanges();
		//MessageBox.Show("Успешно зарегистрировано!");
	}
}