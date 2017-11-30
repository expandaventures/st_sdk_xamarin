using System;
using System.ComponentModel;

namespace SinTrafico
{
    public enum GeometryType
    {
        [Description("polyline")]
        Polyline,

        [Description("geojson")]
        GeoJson,

        [Description("json")]
        Json
    }
}
