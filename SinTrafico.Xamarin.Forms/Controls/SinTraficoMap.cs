using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace SinTrafico.Xamarin.Forms
{
    public class SinTraficoMap : Map
    {
        //
        // Fields
        //
        private readonly ObservableCollection<Polyline> _polyLines = new ObservableCollection<Polyline>();
        private readonly ObservableCollection<SinTraficoPin> _extendedPins = new ObservableCollection<SinTraficoPin>();

        //
        // Properties
        //
        public IList<Polyline> PolyLines
        {
            get
            {
                return _polyLines;
            }
        }

        public IList<SinTraficoPin> ExtendedPins
        {
            get
            {
                return _extendedPins;
            }
        }

        public async Task LoadMapAsync()
        {
        }
    }
}
