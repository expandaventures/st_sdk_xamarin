using System;
namespace SinTrafico
{
    public sealed class Position
    {
        public Position(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public double Latitude { get; }

        public double Longitude { get; }

        public override string ToString() => $"{Latitude},{Longitude}";
    }
}
