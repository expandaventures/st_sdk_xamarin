using System.Linq;
using System.Threading.Tasks;
using SinTrafico.Xamarin.Forms.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SinTrafico.Xamarin.Forms.Demo
{
    public partial class SinTrafico_Xamarin_Forms_DemoPage : ContentPage
    {
        public SinTrafico_Xamarin_Forms_DemoPage()
        {
            InitializeComponent();
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new global::Xamarin.Forms.Maps.Position(19.43436, -99.18630), Distance.FromKilometers(5)));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            SinTrafico.ServiceClient.SetApiKey("03ba35e5261a92e0948b1620290e6408ea183e3c4c11affcd02e16fb15fd312c");

            var routeRequest = new RouteRequest(new Position(25.735432, -100.403001));
            routeRequest.End = new Position(25.723423, -100.394087);
            //routeRequest.Parkings = true;
            //routeRequest.Tolls = true;
            //routeRequest.GasStations = true;
            routeRequest.UserPois = true;
            routeRequest.Distance = 100;
            //routeRequest.VehicleType = VehicleType.Car;
            //routeRequest.Transport = TransportType.Car;
            var routeResponse = await myMap.LoadRouteAsync(routeRequest, Color.DeepPink, 10);

            SinTrafico.ServiceClient.SetApiKey("a816b7b3cc5314fd70bf9188f2cf1d7c9972eda55f2151e4d2d1151f4fa64dff");
            var poiRequest = new PoiRequest();
            poiRequest.Query = "Mi";
            poiRequest.SetBounds(19.00001,-99.99999,19.99999, -99.999999);
            poiRequest.Origin = new Position(19.29883, -99.39606);
            poiRequest.Distance = 1;
            var response = await myMap.LoadPoisAsync(poiRequest);
            if(response != null && response.Result != null && response.Result.Pois != null && response.Result.Pois.Any())
            {
                foreach(var poi in response.Result.Pois)
                {
                    var disntace = poi.Distance;
                }
            }
        }
    }
}
