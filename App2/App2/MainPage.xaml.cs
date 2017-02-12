using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();



            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            if (position != null && position.Latitude != 0 && position.Longitude != 0)
            {
                var position1 = new Position(position.Latitude, position.Longitude); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position1,
                    Label = "Peoplelink Viet Nam",
                    Address = "Số 7 đường 46 phường 5 quận 4"
                };

                myMap.Pins.Clear();

                myMap.Pins.Add(pin);
                myMap.RouteCoordinates.Add(position1);

                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(position1, Distance.FromMiles(0.1)));

            }

            locator.PositionChanged += (s, e) =>
            {

                try
                {
                    var newPosition = e.Position;
                    Position newLogPosition = new Position(newPosition.Latitude, newPosition.Longitude);

                    myMap.MoveToRegion(MapSpan.FromCenterAndRadius(newLogPosition, Distance.FromMiles(0.1)));

                    myMap.RouteCoordinates.Add(newLogPosition);
                    myMap.ReDrawLine();
                }
                catch (Exception ex)
                {

                }
            };

            if (!locator.IsListening)
            {
                await locator.StartListeningAsync(5000, 0);
            }

        }
    }
}
