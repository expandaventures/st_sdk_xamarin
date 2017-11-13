using System;
using System.ComponentModel;

namespace SinTrafico
{
    public enum TransportType
    {
        [Description("car")]
        Car,

        [Description("bicycle")]
        Bicycle,

        [Description("pedestrian")]
        Pedestrian
    }
}
