using System;
using SinTrafico.Xamarin.Android;
using SinTrafico.Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(SinTraficoMap), typeof(SinTraficoMapRenderer))]
namespace SinTrafico.Xamarin.Android
{
    public class SinTraficoMapRenderer : MapRenderer
    {
        public static void Init()
        {
            var dummy = DateTime.Now;
        }

        public SinTraficoMapRenderer()
        {
        }
    }
}
