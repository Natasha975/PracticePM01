using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp.Model;
using ZXing;

namespace WpfApp.Storekeeper
{
	/// <summary>
	/// Логика взаимодействия для ProductCodePage.xaml
	/// </summary>
	public partial class ProductCodePage : Page
	{
		private List<Товар> товары;
		public ProductCodePage()
		{
			InitializeComponent();
			LoadProducts();
		}

		private void LoadProducts()
		{
			using (var db = new WarEntities())
			{
				товары = db.Товар.ToList();
				ProductComboBox.ItemsSource = товары;
			}
		}

		private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedProduct = ProductComboBox.SelectedItem as Товар;
			if (selectedProduct != null)
			{
				NameTextBox.Text = selectedProduct.Название;
				ArticleTextBox.Text = selectedProduct.Артикул;
				BarcodeTextBox.Text = selectedProduct.Штрихкод;
			}
		}

		private void GenerateQRButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Drawing.Image img = null;
				BitmapImage bimg = new BitmapImage();
				using (var ms = new MemoryStream())
				{
					BarcodeWriter writer;
					writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.CODE_93 };
					writer.Options.Height = 80;
					writer.Options.Width = 280;
					writer.Options.PureBarcode = true;
					img = writer.Write(BarcodeTextBox.Text);
					img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
					ms.Position = 0;
					bimg.BeginInit();
					bimg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
					bimg.CacheOption = BitmapCacheOption.OnLoad;
					bimg.UriSource = null;
					bimg.StreamSource = ms;
					bimg.EndInit();
					imgbarcode.Source = bimg;// return File(ms.ToArray(), "image/jpeg");  
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private BitmapImage ToBitmapImage(System.Drawing.Bitmap bitmap)
		{
			using (var memory = new System.IO.MemoryStream())
			{
				bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();

				return bitmapImage;
			}
		}

		private void ExportToWordButton_Click(object sender, RoutedEventArgs e)
		{
			if (imgbarcode.Source == null)
			{
				MessageBox.Show("Сначала сгенерируйте штрихкод.");
				return;
			}

			PrintDialog printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{
				// Создание визуального элемента для печати
				var visual = new DrawingVisual();
				using (var context = visual.RenderOpen())
				{
					var image = new BitmapImage(new Uri(imgbarcode.Source.ToString()));
					context.DrawImage(image, new Rect(0, 0, image.PixelWidth, image.PixelHeight));
				}

				// Печать визуального элемента
				printDialog.PrintVisual(visual, "Печать штрихкода");
			}
		}
	}
}
