using System;

namespace SinTrafico
{
    public class PoiRequest : Request
    {
        public PoiRequest()
        {
        }

        /// <summary>
        /// Query for the POIs' name
        /// </summary>
        /// <value>Query</value>
        public string Query { get; set; }

        /// <summary>
        /// Categories of POIs to fetch
        /// </summary>
        /// <value>Category</value>
        public int? Category { get; set; }

        /// <summary>
        /// Define a rectangle to limit reports inside.
        /// </summary>
        /// <value>MinBounds</value>
        public Position MinBounds { get; private set; }

        /// <summary>
        /// Define a rectangle to limit reports inside.
        /// </summary>
        /// <value>MaxBounds</value>
        public Position MaxBounds { get; private set; }

        /// <summary>
        /// Limit search to POIs in given city
        /// </summary>
        /// <value>The city.</value>
        public int? City { get; set; }

        /// <summary>
        /// Define a rectangle to limit reports inside.
        /// </summary>
        /// <param name="minLat">Minimum lat.</param>
        /// <param name="minLng">Minimum lng.</param>
        /// <param name="maxLat">Max lat.</param>
        /// <param name="maxLng">Max lng.</param>
        public void SetBounds(double minLat, double minLng, double maxLat, double maxLng)
        {
            MinBounds = new Position(minLat, minLng);
            MaxBounds = new Position(maxLat, maxLng);
        }

        public override string BuildQuery()
        {
            var query = $"?key={ServiceClient.ApiKey}";
            if (!string.IsNullOrWhiteSpace(Query))
            {
                query += $"&q={Query}";
            }
            if (Category.HasValue)
            {
                query += $"&category={Category.Value}";
            }
            if (MinBounds != null)
            {
                query += $"&bounds{MinBounds.Longitude},{MinBounds.Latitude},{MaxBounds.Longitude},{MaxBounds.Latitude}";
            }
            if (City.HasValue)
            {
                query += $"&city={City.Value}";
            }
            return query;
        }
    }
}
