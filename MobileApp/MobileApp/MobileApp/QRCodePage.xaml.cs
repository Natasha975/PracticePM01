using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Net.Mobile.Forms;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QRCodePage : ContentPage
	{
		public QRCodePage()
		{
			InitializeComponent();
		}

		private async void ScanButton_Clicked(object sender, EventArgs e)
		{
			// Создание страницы сканирования
			var scanPage = new ZXingScannerPage();

			// Откройтие страницы сканирования
			await Navigation.PushAsync(scanPage);

			scanPage.OnScanResult += (result) =>
			{
				// Остановка сканирования
				scanPage.IsScanning = false;

				// Возврат на предыдущую страницу
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Navigation.PopAsync();
					ResultLabel.Text = $"Результат: {result.Text}";
				});
			};
		}
	}
}