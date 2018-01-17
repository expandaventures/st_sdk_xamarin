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
            var routeRequest = new RouteRequest(new Position(19.29883, -99.39606));
            routeRequest.End = new Position(19.41360, -99.14660);
            routeRequest.Parkings = true;
            routeRequest.Tolls = true;
            routeRequest.GasStations = true;
            routeRequest.VehicleType = VehicleType.Car;
            routeRequest.Transport = TransportType.Car;
            await myMap.LoadRouteAsync(routeRequest, Color.DeepPink, 10);

            SinTrafico.ServiceClient.SetApiKey("a816b7b3cc5314fd70bf9188f2cf1d7c9972eda55f2151e4d2d1151f4fa64dff");
            var poiRequest = new PoiRequest();
            poiRequest.Query = "Mi";
            poiRequest.SetBounds(19.00001,-99.99999,19.99999, -99.999999);
            poiRequest.Origin = new Position(19.29883, -99.39606);
            await myMap.LoadPoisAsync(poiRequest);
        }
    }
}
