using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SinTrafico.Xamarin.Forms
{
    public class SinTraficoMap : Map
    {
        //
        // Fields
        //
        private readonly ObservableCollection<Polyline> _polyLines = new ObservableCollection<Polyline>();
        private readonly ObservableCollection<SinTraficoPin> _extendedPins = new ObservableCollection<SinTraficoPin>();

        //
        // Properties
        //
        public IList<Polyline> PolyLines
        {
            get
            {
                return _polyLines;
            }
        }

        public IList<SinTraficoPin> ExtendedPins
        {
            get
            {
                return _extendedPins;
            }
        }

        public async Task LoadRouteAsync(RouteRequest request, Color lineColor, double lineWidth = 1)
        {
            Pins.Add(new SinTraficoPin() { Label = "" });
            var service = new RoutesServiceClient();
            var response = await service.GetRoutes(request);
            if(response.Result != null)
            {
                foreach (var route in response.Result.Routes)
                {
                    var polyline = new Polyline
                    {
                        LineColor = lineColor,
                        LineWidth = lineWidth
                    };
                    foreach (var leg in route.Legs)
                    {
                        foreach (var step in leg.Steps)
                        {
                            foreach (var inter in step.Intersections)
                            {
                                polyline.Points.Add(new Position(inter.Location[1], inter.Location[0]));
                            }
                        }
                    }
                    PolyLines.Add(polyline);
                }
            }
        }
    }
}
