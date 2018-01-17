using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SinTrafico.Xamarin.Forms
{
    public class SinTraficoPin : SinTrafico.Xamarin.Forms.Models.Pin
    {
        //
        // Static Fields
        //
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(SinTraficoPin), null);
        public static readonly BindableProperty PinColorProperty = BindableProperty.Create(nameof(PinColor), typeof(Color), typeof(SinTraficoPin), Color.Red);

        //
        // Properties
        //
        public ImageSource Icon
        {
            get => (ImageSource)base.GetValue(SinTraficoPin.IconProperty);
            set => base.SetValue(SinTraficoPin.IconProperty, value);
        }

        public Color PinColor
        {
            get => (Color)base.GetValue(SinTraficoPin.PinColorProperty);
            set => base.SetValue(SinTraficoPin.PinColorProperty, value);
        }
    }
}
