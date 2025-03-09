using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabbedPage : TabbedPage
	{
		public MainTabbedPage(User user)
		{
			InitializeComponent();

			Children.Add(new NavigationPage(new ProfilePage(user))
			{
				Title = "Профиль"			
			});			
		}
	}
}