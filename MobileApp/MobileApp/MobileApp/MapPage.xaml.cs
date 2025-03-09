using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage()
		{
			InitializeComponent();

			var mapCenter = new Position(58.731886, 50.183674);
			MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(mapCenter, Distance.FromKilometers(10)));

			AddPins();
		}

		private void AddPins()
		{
			var pins = new List<Pin>
			{
				new Pin
				{
					Label = "Склад 1",
					Address = "Кировская область, Киров, Ленина, 10",
					Position = new Position(58.614311, 49.680482),
                    Type = PinType.Place
				},
				new Pin
				{
					Label = "Склад 2",
					Address = "Кировская область, Киров, Баумана, 2",
					Position = new Position(58.642923, 49.718966),
                    Type = PinType.Place
				},
				new Pin
				{
					Label = "Склад 3",
					Address = "Томская область, Томск, Гагарина, 5",
					Position = new Position(56.483487, 84.951026),
                    Type = PinType.Place
				},
				new Pin
				{
					Label = "Склад 4",
					Address = "Пермский край, Пермь, Мира, 12",
					Position = new Position(57.983868, 56.204075),
					Type = PinType.Place
				},
				new Pin
				{
					Label = "Склад 5",
					Address = "Свердловская область, Екатеринбург, Малышева, 30",
					Position = new Position(56.833319, 60.594546),
					Type = PinType.Place
				}
			};

			foreach (var pin in pins)
			{
				MyMap.Pins.Add(pin);
			}
		}
	}
}