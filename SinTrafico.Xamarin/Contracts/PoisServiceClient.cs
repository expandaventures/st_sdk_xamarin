using System;
using System.Threading.Tasks;
using static SinTrafico.ServiceClient;

namespace SinTrafico
{
    public sealed class PoisServiceClient
    {
        const string SEGMENT = "pois";
        
        public PoisServiceClient()
        {
        }

        public Task<Reponse<PoiResponse>> GetPois(PoiRequest request) => GetResponseDataAsync<PoiResponse>($"{SEGMENT}{request.BuildQuery()}");
    }
}
