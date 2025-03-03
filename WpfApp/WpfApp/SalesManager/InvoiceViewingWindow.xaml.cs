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
using System.Windows.Shapes;
using WpfApp.Model;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для InvoiceViewingWindow.xaml
	/// </summary>
	public partial class InvoiceViewingWindow : Window
	{
		public InvoiceViewingWindow()
		{
			InitializeComponent();
			LoadData();
		}

		public void LoadData()
		{
			try
			{
			//	using (var db = new WarEntities())
			//	{
			//		var приход = db.ПриходнаяНакладная
			//			.Select(s => new ПриходнаяНакладная
			//			{
			//				НазваниеСклада = s.НазваниеСклада,
			//				Товары = db.ТоварНаСкладе
			//					.Where(t => t.НомерСклада == s.Номер)
			//					.Join(db.Товар,
			//						t => t.НомерТовара,
			//						товар => товар.Номер,
			//						(t, товар) => new ТоварНаСкладе
			//						{
			//							НаименованиеТовара = товар.Название,
			//							Количество = t.Количество
			//						})
			//					.ToList()
			//			})
			//			.ToList();

			//		treeView.ItemsSource = склады;
			//	}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
			}
		}
	}
}
