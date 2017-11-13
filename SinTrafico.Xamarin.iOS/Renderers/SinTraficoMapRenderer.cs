using System;
using SinTrafico.Xamarin.Forms;
using SinTrafico.Xamarin.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;

[assembly: ExportRenderer(typeof(SinTraficoMap), typeof(SinTraficoMapRenderer))]
namespace SinTrafico.Xamarin.iOS
{
    public class SinTraficoMapRenderer : MapRenderer
    {
        public static new void Init()
        {
            var dummy = DateTime.Now;
        }

        public SinTraficoMapRenderer()
        {
        }
    }
}