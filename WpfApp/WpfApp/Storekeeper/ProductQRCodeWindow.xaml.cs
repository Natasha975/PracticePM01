using QRCoder;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApp.Model;

namespace WpfApp.Storekeeper
{
	/// <summary>
	/// Логика взаимодействия для ProductQRCodeWindow.xaml
	/// </summary>
	public partial class ProductQRCodeWindow : Window
	{
		private List<Товар> товары;
		public ProductQRCodeWindow()
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

		private void ProductComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
			var selectedProduct = ProductComboBox.SelectedItem as Товар;
			if (selectedProduct == null)
			{
				MessageBox.Show("Выберите товар!");
				return;
			}

			// Использование штрихкода для генерации QR-кода
			string qrData = selectedProduct.Штрихкод;

			// Генерация QR-кода
			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
			QRCode qrCode = new QRCode(qrCodeData);

			// Преобразование QR-кода в изображение
			var qrCodeImage = qrCode.GetGraphic(20);
			QRCodeImage.Source = ToBitmapImage(qrCodeImage);
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
		
		//private void ExportToWordButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var selectedProduct = ProductComboBox.SelectedItem as Товар;
		//	if (selectedProduct == null)
		//	{
		//		MessageBox.Show("Выберите товар!");
		//		return;
		//	}

		//	if (QRCodeImage.Source == null)
		//	{
		//		MessageBox.Show("Сначала сгенерируйте QR-код!");
		//		return;
		//	}

		//	var wordApp = new Microsoft.Office.Interop.Word.Application();
		//	var wordDoc = wordApp.Documents.Add();
		//	wordApp.Visible = true;

		//	var productInfo = $"Название: {selectedProduct.Название}\n" +
		//					 $"Артикул: {selectedProduct.Артикул}\n" +
		//					 $"Штрихкод: {selectedProduct.Штрихкод}";

		//	wordDoc.Content.Text = productInfo;

		//	var qrCodeBitmap = (QRCodeImage.Source as BitmapImage).ToBitmap();
		//	var tempFilePath = Path.GetTempFileName() + ".png";
		//	qrCodeBitmap.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Png);

		//	var imageRange = wordDoc.Content;
		//	imageRange.InsertAfter("\n\nQR-код:\n");
		//	imageRange.InsertAfter("\n");
		//	imageRange = wordDoc.Content;
		//	imageRange.InsertAfter("\n");

		//	wordDoc.InlineShapes.AddPicture(tempFilePath, Range: imageRange);

		//	File.Delete(tempFilePath);

		//	var saveFileDialog = new Microsoft.Win32.SaveFileDialog
		//	{
		//		Filter = "Word Documents|*.docx",
		//		FileName = $"Товар_{selectedProduct.Название}.docx"
		//	};

		//	if (saveFileDialog.ShowDialog() == true)
		//	{
		//		wordDoc.SaveAs2(saveFileDialog.FileName);
		//		MessageBox.Show("Документ успешно сохранен!");
		//	}

		//	// Закрытие Word
		//	// wordDoc.Close();
		//	// wordApp.Quit();
		//}
		//public static class BitmapImageExtensions
		//{
		//	public static System.Drawing.Bitmap ToBitmap(this BitmapImage bitmapImage)
		//	{
		//		using (var outStream = new MemoryStream())
		//		{
		//			BitmapEncoder enc = new PngBitmapEncoder();
		//			enc.Frames.Add(BitmapFrame.Create(bitmapImage));
		//			enc.Save(outStream);
		//			return new System.Drawing.Bitmap(outStream);
		//		}
		//	}
		//}
	}
}