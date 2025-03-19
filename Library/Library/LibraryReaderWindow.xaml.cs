using Library.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
	/// <summary>
	/// Логика взаимодействия для LibraryReaderWindow.xaml
	/// </summary>
	public partial class LibraryReaderWindow : Window
	{
		БиблиотекаEntities db = new БиблиотекаEntities();

		public LibraryReaderWindow()
		{
			InitializeComponent();
			LoadData();
		}

		void LoadData()
		{
			try
			{
				var quer = from читатель in db.Читатель
						   join история in db.ИсторияВыдачиКниг on читатель.Номер equals история.НомерЧитателя
						   join штраф in db.Штраф on читатель.Номер equals штраф.НомерЧитателя
						   group new { Читатель = читатель, Штраф = штраф } by new { читатель.ФИО, читатель.НомерЧитательскогоБилета } into g
						   select new
						   {
							   ФИО = g.Key.ФИО,
							   НомерЧит = g.Key.НомерЧитательскогоБилета,
							   ШтрафЧит = g.Sum(s => s.Штраф.Сумма),
						   };

				ReaderDG.ItemsSource = quer.ToList();
			}
			catch
			{
				MessageBox.Show($"Ошибка при загрузке данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void AddReadirBt_Click(object sender, RoutedEventArgs e)
		{
			var addEditWindow = new AddEditReaderWindow();
			if (addEditWindow.ShowDialog() == true)
			{
				LoadData();
			}
		}

		int libraryCardNumber;

		private void ReadersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedReader = ReaderDG.SelectedItem as dynamic;
			if (selectedReader == null)
			{
				MessageBox.Show("Выберите читателя для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			libraryCardNumber = selectedReader.НомерЧит;
			Select();
		}

		void Select()
		{

			var reader = db.Читатель.FirstOrDefault(r => r.НомерЧитательскогоБилета == libraryCardNumber);
			if (reader == null)
			{
				MessageBox.Show("Читатель не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var addEditWindow = new AddEditReaderWindow(reader);
			if (addEditWindow.ShowDialog() == true)
			{
				LoadData();
			}
		}

		private void HistoryBt_Click(object sender, RoutedEventArgs e)
		{
			//var selectedReader = ReaderDG.SelectedItem as dynamic;
			//if (selectedReader == null)
			//{
			//	MessageBox.Show("Выберите читателя для просмотра истории.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			//	return;
			//}

			//int readerId = selectedReader.НомерЧит;
			var historyWindow = new ReaderHistoryWindow();
			historyWindow.ShowDialog();
		}
	}
}