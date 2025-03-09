using System;
using Xamarin.Forms;

namespace MobileApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void LoginButton_Clicked(object sender, EventArgs e)
		{
			string login = LoginEntry.Text;
			string password = PasswordEntry.Text;

			var user = User.Authenticate(login, password);

			if (user == null)
			{
				await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
				return;
			}

			var random = new Random();
			string code = random.Next(1000, 9999).ToString();

			await Navigation.PushAsync(new CodeVerificationPage(code, user));
		}
	}
}