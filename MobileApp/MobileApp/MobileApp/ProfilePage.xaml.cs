using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		private User User;
		public ProfilePage(User user)
		{
			InitializeComponent();

			User = user;
			LastNameLb.Text = user.LastName;
			FirstNameLb.Text = user.FirstName;
			AgeLb.Text = $"Возраст: {user.Age}";
			RoleLb.Text = $"Роль: {user.Role}";
			UserImage.Source = user.Photo;
		}

		private async void SkipButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new BarcodePage(User));
		}
	}
}