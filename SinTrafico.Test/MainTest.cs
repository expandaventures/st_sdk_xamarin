using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SinTrafico.Test
{
    public class MainTest
    {
        const string DEMO_API_KEY = "b5b5f6ebe6fc3e233f958afd81ef1c664c495d7637267c0f2902ef85c8e3ad7d";

        [Fact]
        public void VerifyEmptyApiKey()
        {
            ServiceClient.SetApiToken(null);
            Assert.True(string.IsNullOrWhiteSpace(ServiceClient.ApiKey));
        }

        [Fact]
        public void VerifyApiKey()
        {
            ServiceClient.SetApiToken(DEMO_API_KEY);
            Assert.False(string.IsNullOrWhiteSpace(ServiceClient.ApiKey));
        }

        [Fact]
        public async Task RoutesRequestWithoutApiKey()
        {
            ServiceClient.SetApiToken(null);             var request = new RouteRequest(new Position(19.385994, -99.192323));             request.End = new Position(19.458232, -99.113169);
            var service = new RoutesServiceClient();             var result = await service.GetRoutes(request);
            Assert.True(result.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task RoutesRequestWithApiKey()
        {
            ServiceClient.SetApiToken(DEMO_API_KEY);
            var request = new RouteRequest(new Position(19.385994, -99.192323));
            request.End = new Position(19.458232, -99.113169);
            var service = new RoutesServiceClient();
            var result = await service.GetRoutes(request);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task PoisRequestWithoutApiKey()
        {
            ServiceClient.SetApiToken(null);
            var request = new PoiRequest();
            request.SetBounds(19.00001, -99.99999, 19.99999, -99.000001);
            request.Query = "MI";
            request.Category = 3;
            request.City = 1;
            var service = new PoisServiceClient();
            var result = await service.GetPois(request);
            Assert.True(result.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PoisRequestWithApiKey()
        {
            ServiceClient.SetApiToken("a816b7b3cc5314fd70bf9188f2cf1d7c9972eda55f2151e4d2d1151f4fa64dff");
            var request = new PoiRequest();             request.SetBounds(19.00001, -99.99999, 19.99999, -99.000001);             request.Query = "MI";             request.Category = 3;             request.City = 1;
            var service = new PoisServiceClient();             var result = await service.GetPois(request);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
        }
    }
}
