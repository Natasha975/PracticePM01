using Library.Model;
using System;
using System.Windows;

namespace Library
{
	/// <summary>
	/// Логика взаимодействия для AddEditReaderWindow.xaml
	/// </summary>
	public partial class AddEditReaderWindow : Window
	{
		//БиблиотекаEntities db = new БиблиотекаEntities();
		private readonly Читатель reader;

		private readonly БиблиотекаEntities db = new БиблиотекаEntities();

		public AddEditReaderWindow(Читатель readers = null)
		{
			InitializeComponent();
			reader = readers;

			if (reader != null)
			{
				FullNameTB.Text = reader.ФИО;
				CardNumberTB.Text = reader.НомерЧитательскогоБилета.ToString();
				AddressTB.Text = reader.Адрес;
				PhoneTB.Text = reader.Телефон;
				EmailTB.Text = reader.Email;
			}
		}

		private void SaveBt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(FullNameTB.Text) || string.IsNullOrWhiteSpace(CardNumberTB.Text))
				{
					MessageBox.Show("Заполните обязательные поля: ФИО и Номер читательского билета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				if (reader == null)
				{
					var newReader = new Читатель
					{
						ФИО = FullNameTB.Text,
						НомерЧитательскогоБилета = Convert.ToInt32(CardNumberTB.Text),
						Адрес = AddressTB.Text,
						Телефон = PhoneTB.Text,
						Email = EmailTB.Text
					};
					db.Читатель.Add(newReader);
				}
				else
				{
					reader.ФИО = FullNameTB.Text;
					reader.НомерЧитательскогоБилета = Convert.ToInt32(CardNumberTB.Text);
					reader.Адрес = AddressTB.Text;
					reader.Телефон = PhoneTB.Text;
					reader.Email = EmailTB.Text;
				}
				
				db.SaveChanges();
				DialogResult = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void CancelBt_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
