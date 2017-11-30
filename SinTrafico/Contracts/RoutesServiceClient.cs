using System;
using System.Threading.Tasks;
using static SinTrafico.ServiceClient;

namespace SinTrafico
{
    public sealed class RoutesServiceClient
    {
        const string SEGMENT = "route";

        public RoutesServiceClient()
        {
        }

        public Task<Response<RouteResponse>> GetRoutes(RouteRequest request) => GetResponseDataAsync<RouteResponse>($"{SEGMENT}{request.BuildQuery()}");
    }
}
