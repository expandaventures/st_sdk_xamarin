using System;
using Xamarin.Forms;
using FormsPosition = Xamarin.Forms.Maps.Position;

namespace SinTrafico.Xamarin.Forms.Extensions
{
    public static class PoiExtensions
    {

        public static SinTraficoPin ToSinTraficoPin(this Poi poi) => new SinTraficoPin { Label = poi.Name, Address = poi.Category, Icon = poi.GetPoiDefaultPin(), Position = new FormsPosition(poi.Latitude, poi.Longitude) };

        public static ImageSource GetPoiDefaultPin(this Poi poi)
        {
            switch(poi.CategoryId)
            {
                case "0":
                    return ServiceClient.ACCIDENT_ICON_URL;
                case "1":
                    return ServiceClient.BADACCIDENT_ICON_URL;
                case "2":
                    return ServiceClient.ROADWORK_ICON_URL;
                case "3":
                    return ServiceClient.FLOOD_ICON_URL;
                case "4":
                    return ServiceClient.PROTEST_ICON_URL;
                case "5":
                    return ServiceClient.STRIKE_ICON_URL;
                case "6":
                    return ServiceClient.EVENT_ICON_URL;
                case "7":
                    return ServiceClient.INCIDENT_ICON_URL;
                case "8":
                    return ServiceClient.PEREGRINATION_ICON_URL;
                case "9":
                    return ServiceClient.BROKENVEHICLE_ICON_URL;
                case "10":
                    return ServiceClient.MARKET_ICON_URL;
                case "11":
                    return ServiceClient.ONOFFCUT_ICON_URL;
                case "12":
                    return ServiceClient.WRONGWAY_ICON_URL;
                case "13":
                    return ServiceClient.OPEN_ICON_URL;
                case "14":
                    return ServiceClient.ALERTS_ICON_URL;
                case "15":
                    return ServiceClient.MOBILIZATION_ICON_URL;
                case "16":
                    return ServiceClient.MXSUBWAY_ICON_URL;
                case "17":
                    return ServiceClient.MXMETROBUS_ICON_URL;
                case "18":
                    return ServiceClient.GDLLIGHTRAIL_ICON_URL;
                case "19":
                    return ServiceClient.GDLMACROBUS_ICON_URL;
                default:
                    return ServiceClient.DEFAULT_ICON_URL;
            }
        }
    }
}
