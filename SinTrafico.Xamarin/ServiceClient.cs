using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SinTrafico
{
    public static class ServiceClient
    {
        public const string BASE_URL = "http://api.sintrafico.com";
        public const string INVALID_INPUT_ERROR_MESSAGE = "Invalid input";
        public const string UNAUTHORIZED_ERROR_MESSAGE = "Unauthorized access";
        public const string NOTFOUND_ERROR_MESSAGE = "User not found";

        static string _apiKey;
        static int _timeoutMilliseconds = 10000;
        static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BASE_URL),
            Timeout = TimeSpan.FromMilliseconds(TimeoutMilliseconds)
        };

        public static int TimeoutMilliseconds
        {
            get
            {
                return _timeoutMilliseconds;
            }
            set
            {
                _timeoutMilliseconds = value;
                _httpClient.Timeout = TimeSpan.FromMilliseconds(_timeoutMilliseconds);
            }
        }

        public static string ApiKey
        {
            get
            {
                return _apiKey;
            }
        }

        public static void SetApiKey(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static Task<HttpResponseMessage> GetDataAsync(string endpoint) => _httpClient.GetAsync(endpoint);

        internal static Task<HttpResponseMessage> PostDataAsync(string data, string endpoint) => _httpClient.PostAsync(endpoint, new StringContent(data, Encoding.UTF8, "application/json"));

        internal static Task<HttpResponseMessage> PutDataAsync(string data, string endpoint) => _httpClient.PutAsync(endpoint, new StringContent(data, Encoding.UTF8, "application/json"));

        internal static Task<HttpResponseMessage> DeleteDataAsync(string endpoint) => _httpClient.DeleteAsync(endpoint);

        internal static string SerializeObject(object obj) => JsonConvert.SerializeObject(obj);

        internal static T DeSerializeObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        internal static async Task<Reponse<T>> GetResponseDataAsync<T>(string segment)
        {
            var result = await GetDataAsync(segment);
            T resultItem = default(T);
            string errorMessage = null;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var responseMessage = await result.Content.ReadAsStringAsync();
                resultItem = DeSerializeObject<T>(responseMessage);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                errorMessage = INVALID_INPUT_ERROR_MESSAGE;
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                errorMessage = UNAUTHORIZED_ERROR_MESSAGE;
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                errorMessage = NOTFOUND_ERROR_MESSAGE;
            }
            return new Reponse<T>(resultItem, result.StatusCode, errorMessage);
        }
    }
}
