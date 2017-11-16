using Xamarin.Forms;

namespace SinTrafico.Xamarin.Forms.Demo
{
    public partial class App : Application
    {
        public App()
        {
            SinTrafico.ServiceClient.SetApiKey("b5b5f6ebe6fc3e233f958afd81ef1c664c495d7637267c0f2902ef85c8e3ad7d");

            InitializeComponent();

            MainPage = new SinTrafico_Xamarin_Forms_DemoPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
