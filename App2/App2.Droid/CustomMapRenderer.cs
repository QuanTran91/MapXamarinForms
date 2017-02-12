using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using App2;
using App2.Droid;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace App2.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        GoogleMap map;
        List<Position> routeCoordinates;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                routeCoordinates = formsMap.RouteCoordinates;
                formsMap.LineDrawed += FormsMap_LineDrawed;

                ((MapView)Control).GetMapAsync(this);
            }
        }

        private void FormsMap_LineDrawed(object sender, System.EventArgs e)
        {
            if (map != null)
            {
                var polylineOptions = new PolylineOptions();
                polylineOptions.InvokeColor(0x66FF0000);

                foreach (var position in routeCoordinates)
                {
                    polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                }

                map.AddPolyline(polylineOptions);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
           
        }
    }
}