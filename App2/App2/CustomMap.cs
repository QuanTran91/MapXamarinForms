using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace App2
{
    public class CustomMap : Map
    {
        public List<Position> RouteCoordinates { get; set; }
        public event EventHandler LineDrawed;

        public void ReDrawLine()
        {
            LineDrawed?.Invoke(this, EventArgs.Empty);
        }
        public CustomMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}
