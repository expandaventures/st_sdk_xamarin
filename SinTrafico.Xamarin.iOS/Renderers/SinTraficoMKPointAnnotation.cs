using System;
using CoreLocation;
using MapKit;
using SinTrafico.Xamarin.Forms;

namespace SinTrafico.Xamarin.iOS
{
    public class SinTraficoMKPointAnnotation : MKPointAnnotation
    {
        public SinTraficoPin Pin { get; }

        public SinTraficoMKPointAnnotation(SinTraficoPin pin)
        {
            Pin = pin;
            Title = Pin.Label;
            Subtitle = Pin.Address ?? "";
            Coordinate = new CLLocationCoordinate2D(Pin.Position.Latitude, Pin.Position.Longitude);
        }
    }
}
