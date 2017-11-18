using System;
namespace SinTrafico.Xamarin.Android.Helpers
{
    public static class ColorExtensions
    {
        public static string GetHexString(this global::Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            return $"{red:X2}{green:X2}{blue:X2}";
        }
    }
}
