using Library.Model;
using System;
using System.Linq;
using System.Windows;

namespace Library
{
	/// <summary>
	/// Логика взаимодействия для ReaderHistoryWindow.xaml
	/// </summary>
	public partial class ReaderHistoryWindow : Window
	{
		private int readerID;

		public ReaderHistoryWindow()
		{
			InitializeComponent();
			LoadHistory();
		}

		private void LoadHistory()
		{
			try
			{
				using (var db = new БиблиотекаEntities())
				{
					var history = from выдача in db.ИсторияВыдачиКниг
								  join книга in db.Книга on выдача.НомерКниги equals книга.Номер
								  //where выдача.НомерЧитателя == readerID
								  select new
								  {
									  НаименованиеКниги = книга.Наименование,
									  ДатаВыдачи = выдача.ДатаВыдачи,
									  ДатаВозврата = выдача.ДатаВозврата
								  };

					HistoryDataGrid.ItemsSource = history.ToList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}

		public int CalculateFine(int readerId, int bookId, int daysOverdue, string bookType)
		{
			using (var db = new БиблиотекаEntities())
			{
				var readerExists = db.Читатель.Any(r => r.Номер == 1);
				var bookExists = db.Книга.Any(b => b.Номер == bookId);

				if (!readerExists || !bookExists)
				{
					return -1;
				}

				switch (bookType.ToLower())
				{
					case "учебная":
						return daysOverdue * 10;
					case "художественная":
						return daysOverdue * 5;
					case "научная":
						return daysOverdue * 15;
					default:
						return -1;
				}
			}
		}
	}
}