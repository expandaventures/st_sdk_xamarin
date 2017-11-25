using System.Threading.Tasks;
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
            //var request = new RouteRequest(new Position(19.29883, -99.39606));
            //request.End = new Position(19.41360, -99.14660);
            //request.Parkings = true;
            //request.Tolls = true;
            //request.GasStations = true;
            //request.VehicleType = VehicleType.Car;
            //request.Transport = TransportType.Car;
            //request.Geometry = GeometryType.GeoJson;
            //await myMap.LoadRouteAsync(request, Color.DeepPink, 10);
            SinTrafico.ServiceClient.SetApiKey("a816b7b3cc5314fd70bf9188f2cf1d7c9972eda55f2151e4d2d1151f4fa64dff");
            var request = new PoiRequest();
            request.Query = "Mi";
            request.SetBounds(19.00001,-99.99999,19.99999, -99.999999);
            await myMap.LoadPoisAsync(request);
        }
    }
}
