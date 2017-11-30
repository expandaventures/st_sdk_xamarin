using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using FormsPosition = Xamarin.Forms.Maps.Position;

namespace SinTrafico.Xamarin.Forms.Extensions
{
    public static class SinTraficoMapExtensions
    {
        static readonly List<string> _rateBoothFields = new List<string> {
            "id", "id_plaza_e", "nombre_sal", "nombre_ent", "t_moto", "t_auto", "t_eje_lig",
            "t_autobus2", "t_autobus3", "t_autobus4", "t_camion2", "t_camion3", "t_camion4", "t_camion5", "t_camion6",
            "t_camion7", "t_camion8", "t_camion9", "t_eje_pes", "fecha_act"
        };

        public static async Task<Response<RouteResponse>> LoadRouteAsync(this SinTraficoMap map, RouteRequest request, Color lineColor, double lineWidth = 1, bool loadPois = true)
        {
            var service = new RoutesServiceClient();
            var response = await service.GetRoutes(request);
            if (response.Result != null)
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
                    map.PolyLines.Add(polyline);
                    if (loadPois && route.Pois != null)
                    {
                        var poisData = route.Pois;
                        if (poisData != null)
                        {
                            foreach (var parking in poisData.Parkings)
                            {
                                var parkingPin = new SinTraficoPin();
                                parkingPin.Label = "Tarifas no disponibles";
                                parkingPin.Address = parking.Address;
                                parkingPin.Icon = ServiceClient.PARKING_ICON_URL;
                                parkingPin.Position = new FormsPosition(parking.Geometry.Coordinates[1], parking.Geometry.Coordinates[0]);
                                map.Pins.Add(parkingPin);
                            }
                            foreach (var booth in poisData.Tolls)
                            {
                                var boothPin = new SinTraficoPin();
                                var pinTitle = "";
                                try
                                {
                                    var priceIndex = (int)request.VehicleType + 4;
                                    pinTitle = $"{_rateBoothFields[priceIndex]}: ${booth.Rates[priceIndex]}";
                                }
                                catch
                                {
                                    pinTitle = "Tarifas no disponibles";
                                }
                                boothPin.Label = pinTitle;
                                boothPin.Address = booth.Address;
                                boothPin.Icon = ServiceClient.BOOTH_ICON_URL;
                                boothPin.Position = new FormsPosition(booth.Geometry.Coordinates[1], booth.Geometry.Coordinates[0]);
                                map.Pins.Add(boothPin);
                            }
                            foreach (var gasStation in poisData.GasStations)
                            {
                                var gasStationPin = new SinTraficoPin();
                                gasStationPin.Label = "Tarifas no disponibles";
                                gasStationPin.Address = gasStation.Address;
                                gasStationPin.Icon = ServiceClient.GASSTATION_ICON_URL;
                                gasStationPin.Position = new FormsPosition(gasStation.Geometry.Coordinates[1], gasStation.Geometry.Coordinates[0]);
                                map.Pins.Add(gasStationPin);
                            }
                        }
                    }
                }
            }
            return response;
        }

        public static async Task<Response<PoiResponse>> LoadPoisAsync(this SinTraficoMap map, PoiRequest request)
        {
            var service = new PoisServiceClient();
            var response = await service.GetPois(request);
            if (response.Result != null && response.Result.Pois.Any())
            {
                foreach (var poi in response.Result.Pois)
                {
                    var poiPin = new SinTraficoPin();
                    poiPin.Label = poi.Name;
                    poiPin.Address = poi.Category;
                    poiPin.PinColor = Color.Azure;
                    poiPin.Position = new FormsPosition(poi.Latitude, poi.Longitude);
                    map.Pins.Add(poiPin);
                }
            }
            return response;
        }
    }
}
