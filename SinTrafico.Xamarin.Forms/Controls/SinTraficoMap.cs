using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SinTrafico.Xamarin.Forms
{
    public class SinTraficoMap : Map
    {
        //
        // Fields
        //
        private readonly ObservableCollection<Polyline> _polyLines = new ObservableCollection<Polyline>();

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
    }
}
