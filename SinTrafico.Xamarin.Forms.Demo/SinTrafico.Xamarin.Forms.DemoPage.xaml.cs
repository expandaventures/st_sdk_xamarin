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
            var request = new RouteRequest(new Position(19.43436, -99.18630));
            request.End = new Position(19.41360, -99.14660);
            await myMap.LoadRouteAsync(request, Color.DeepPink, 10);

            myMap.Pins.Add(new SinTraficoPin { PinColor = Color.Black, Address = "19.43436, -99.18630", Label = "START", Position = new global::Xamarin.Forms.Maps.Position(request.Start.Latitude, request.Start.Longitude) });
            myMap.Pins.Add(new SinTraficoPin { PinColor = Color.Pink, Address = "19.43436, -99.18630", Label = "END", Position = new global::Xamarin.Forms.Maps.Position(request.End.Latitude, request.End.Longitude) });
        }
    }
}
