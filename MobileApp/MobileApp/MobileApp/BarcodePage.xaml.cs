using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarcodePage : ContentPage
	{
		public BarcodePage(User user)
		{
			InitializeComponent();

			UserLastNameLb.Text = user.LastName;
			UserFirstNameLb.Text = user.FirstName;

			string barcodeValue = GenerateRandomBarcode();
			BarcodeLb.Text = barcodeValue;

			BarcodeImage.BarcodeValue = barcodeValue;
		}

		private string GenerateRandomBarcode()
		{
			var random = new Random();
			return random.Next(10000000, 99999999).ToString();
		}
	}
}