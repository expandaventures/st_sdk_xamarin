using System;
using System.Collections.Generic;
using System.Linq;
using SinTrafico.Helpers;

namespace SinTrafico
{
    public sealed class RouteRequest : Request
    {
        public RouteRequest(Position start)
        {
            Start = start;
        }

        /// <summary>
        /// Route's initial point
        /// </summary>
        /// <value>Start</value>
        public Position Start { get; }

        /// <summary>
        /// Route's end point
        /// </summary>
        /// <value>End</value>
        public Position End { get; set; }

        /// <summary>
        /// The type of pois that should be found close to the end of the route. Range 0-3
        /// </summary>
        /// <value>PoiSet</value>
        public int? PoiSet { get; set; }

        /// <summary>
        /// Departure time in 24-hour format
        /// </summary>
        /// <value>DepartureTime</value>
        public DateTime? DepartureTime { get; set; }

        /// <summary>
        /// Flag that allows waypoints to be rearranged for the fastest route. If optimize is true, at least 4 points are needed: start and 3 wp[] or start, end, and 2 wp[]
        /// </summary>
        /// <value>Optimize</value>
        public bool? Optimize { get; set; }

        /// <summary>
        /// Waypoints to be included in route.
        /// </summary>
        /// <value>WayPoints</value>
        public List<Position> WayPoints { get; set; }

        /// <summary>
        /// If true, route will be saved to user's profile
        /// </summary>
        /// <value>Save</value>
        public bool? Save { get; set; }

        /// <summary>
        /// If true, toll information will be included if any
        /// </summary>
        /// <value>Tolls</value>
        public bool? Tolls { get; set; }

        /// <summary>
        /// If true, Gas stations information will be included if any
        /// </summary>
        /// <value>GasStations</value>
        public bool? GasStations { get; set; }

        /// <summary>
        /// If true, User pois information will be included if any.
        /// </summary>
        /// <value>The user pois.</value>
        public bool? UserPois { get; set; }

        /// <summary>
        /// Indicate the price that should be used to calculate toll total
        /// </summary>
        /// <value>VehicleType</value>
        public VehicleType? VehicleType { get; set; }

        /// <summary>
        /// If true, parkings information at the end od route will be included if any
        /// </summary>
        /// <value>Parkings</value>
        public bool? Parkings { get; set; }

        /// <summary>
        /// The id of the user that wants the route saved to his/her profile
        /// </summary>
        /// <value>UserId</value>
        public string UserId { get; set; }

        /// <summary>
        /// String indicating the means used for the route
        /// </summary>
        /// <value>Transport</value>
        public TransportType? Transport { get; set; }

        /// <summary>
        /// If true, route will be calculated using live traffic data
        /// </summary>
        /// <value>Live</value>
        public bool? Live { get; set; }

        /// <summary>
        /// Arrival time in 24-hour format
        /// </summary>
        /// <value>ArrivalTime</value>
        public DateTime? ArrivalTime { get; set; }

        /// <summary>
        /// If polyline, route geometry and every step will be returned encoded in Google polyline format, otherwise in GeoJSON format
        /// </summary>
        /// <value>Geometry</value>
        public GeometryType? Geometry { get; set; }

        /// <summary>
        /// If true, weather information will be included in each step of the route
        /// </summary>
        /// <value>Weather</value>
        public bool? Weather { get; set; }

        /// <summary>
        /// If true, alternative routes may be found and returned
        /// </summary>
        /// <value>Alternatives</value>
        public bool? Alternatives { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public double? Distance { get; set; }

        public override string BuildQuery()
        {
            var query = $"?key={ServiceClient.ApiKey}";
            if(Start != null)
            {
                query += $"&start={Start.ToString()}";
            }
            if (End != null)
            {
                query += $"&end={End.ToString()}";
            }
            if (PoiSet.HasValue)
            {
                query += $"&poi_set[]={PoiSet.Value}";
            }
            if (DepartureTime.HasValue)
            {
                query += $"&departure_time={DepartureTime.Value.ToUniversalTime().ToString("HH:mm")}";
            }
            if (Optimize.HasValue)
            {
                query += $"&optimize={Optimize.Value}";
            }
            if (WayPoints != null && WayPoints.Any())
            {
                foreach (var wayPoint in WayPoints)
                {
                    query += $"&wp[]={wayPoint.ToString()}";
                }
            }
            if (Save.HasValue)
            {
                query += $"&save={Save.Value}";
            }
            if (Parkings.HasValue && Parkings.Value)
            {
                query += $"&poi_in[]=0";
            }
            if (Tolls.HasValue && Tolls.Value)
            {
                query += $"&poi_in[]=1";
            }
            if (GasStations.HasValue && GasStations.Value)
            {
                query += $"&poi_in[]=2";
            }
            if (UserPois.HasValue && UserPois.Value)
            {
                query += $"&poi_in[]=4";
            }
            if(VehicleType.HasValue)
            {
                query += $"&vehicle_type={(uint)VehicleType.Value}";
            }
            if (!string.IsNullOrWhiteSpace(UserId))
            {
                query += $"&user_id={UserId}";
            }
            if (Transport.HasValue)
            {
                query += $"&transport={Transport.Value.GetDescription()}";
            }
            if (Live.HasValue)
            {
                query += $"&live={Live.Value}";
            }
            if (ArrivalTime.HasValue)
            {
                query += $"&arrival_time={ArrivalTime.Value.ToUniversalTime().ToString("HH:mm")}";
            }
            if (Geometry.HasValue)
            {
                query += $"&geometry={Geometry.Value.GetDescription()}";
            }
            if (Weather.HasValue)
            {
                query += $"&weather={Weather.Value}";
            }
            if (Alternatives.HasValue)
            {
                query += $"&alternatives={Alternatives.Value}";
            }
            if (Distance.HasValue)
            {
                query += $"&distance={Distance.Value}";
            }
            return query;
        }
    }
}
