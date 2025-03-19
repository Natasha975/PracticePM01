using System;
using System.Collections.Generic;
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

namespace Library
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private decimal CalculatePenalty(int daysOverdue, string bookType)
		{
			decimal penaltyRate = 0;

			switch (bookType)
			{
				case "Учебная":
					penaltyRate = 10;
					break;
				case "Художественная":
					penaltyRate = 5;
					break;
				case "Научная":
					penaltyRate = 15;
					break;
				default:
					throw new ArgumentException("Выберите тип книги.");
			}

			return daysOverdue * penaltyRate;
		}

		private void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int daysOverdue = int.Parse(DaysOverdueTextBox.Text);
				string bookType = (BookTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

				decimal penalty = CalculatePenalty(daysOverdue, bookType);

				ResultTextBlock.Text = $"Штраф: {penalty} руб.";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
