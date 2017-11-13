using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SinTrafico.Xamarin.Forms
{
    public class Polyline : BindableObject
    {
        //
        // Static Fields
        //
        public static readonly BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(Polyline), Color.Red);
        public static readonly BindableProperty LineWidthProperty = BindableProperty.Create(nameof(LineWidth), typeof(double), typeof(Polyline), 1);

        //
        // Fields
        //
        private readonly ObservableCollection<Position> _points = new ObservableCollection<Position>();

        //
        // Properties
        //
        public IList<Position> Points
        {
            get
            {
                return _points;
            }
        }

        public Color LineColor
        {
            get => (Color)base.GetValue(Polyline.LineColorProperty);
            set => base.SetValue(Polyline.LineColorProperty, value);
        }

        public double LineWidth
        {
            get => (double)base.GetValue(Polyline.LineWidthProperty);
            set => base.SetValue(Polyline.LineWidthProperty, value);
        }
    }
}
