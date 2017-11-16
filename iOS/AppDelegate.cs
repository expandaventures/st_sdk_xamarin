using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using SinTrafico.Xamarin.iOS;
using UIKit;

namespace SinTrafico.Xamarin.Forms.Demo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            SinTraficoMapRenderer.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
