using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CodeVerificationPage : ContentPage
	{
		private string Code;
		private User User;

		public CodeVerificationPage (string code, User user)
		{
			InitializeComponent ();
			Code = code;
			User = user;
			CodeLabel.Text = $"Введите код: {code}";
		}

		private async void SubmitButton_Clicked(object sender, EventArgs e)
		{
			if (CodeEntry.Text == Code)
			{
				await Navigation.PushAsync(new MainTabbedPage(User));
			}
			else
			{
				await DisplayAlert("Ошибка", "Неверный код", "OK");
			}
		}
	}
}