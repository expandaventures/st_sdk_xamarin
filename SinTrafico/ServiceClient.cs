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
        #region PinImageSource

        public const string PIN_BASE_URL = "https://s3.amazonaws.com/sintrafico/images/";
        public const string PARKING_ICON_URL = PIN_BASE_URL + "iconos_estacionamiento.png";
        public const string BOOTH_ICON_URL = PIN_BASE_URL + "iconos_peaje.png";
        public const string GASSTATION_ICON_URL = PIN_BASE_URL + "iconos_gas.png";
        public const string ACCIDENT_ICON_URL = PIN_BASE_URL + "iconos_accidente.png";
        public const string BADACCIDENT_ICON_URL = PIN_BASE_URL + "iconos_accidente.png";
        public const string ROADWORK_ICON_URL = PIN_BASE_URL + "iconos_obras.png";
        public const string FLOOD_ICON_URL = PIN_BASE_URL + "iconos_inundacion.png";
        public const string PROTEST_ICON_URL = PIN_BASE_URL + "iconos_manifestacion.png";
        public const string STRIKE_ICON_URL = PIN_BASE_URL + "iconos_manifestacion.png";
        public const string EVENT_ICON_URL = PIN_BASE_URL + "iconos_evento.png";
        public const string INCIDENT_ICON_URL = PIN_BASE_URL + "iconos_reporte.png";
        public const string PEREGRINATION_ICON_URL = PIN_BASE_URL + "iconos_peregrinacion.png";
        public const string BROKENVEHICLE_ICON_URL = PIN_BASE_URL + "iconos_vehiculo_descompuesto.png";
        public const string MARKET_ICON_URL = PIN_BASE_URL + "iconos_mercado.png";
        public const string ONOFFCUT_ICON_URL = PIN_BASE_URL + "iconos_reporte.png";
        public const string WRONGWAY_ICON_URL = PIN_BASE_URL + "iconos_contraflujo.png";
        public const string OPEN_ICON_URL = PIN_BASE_URL + "iconos_abierto.png";
        public const string ALERTS_ICON_URL = PIN_BASE_URL + "iconos_alerta.png";
        public const string MOBILIZATION_ICON_URL = PIN_BASE_URL + "iconos_movilizacion.png";
        public const string MXSUBWAY_ICON_URL = PIN_BASE_URL + "iconos_alerta.png";
        public const string MXMETROBUS_ICON_URL = PIN_BASE_URL + "iconos_alerta.png";
        public const string GDLLIGHTRAIL_ICON_URL = PIN_BASE_URL + "iconos_alerta.png";
        public const string GDLMACROBUS_ICON_URL = PIN_BASE_URL + "iconos_alerta.png";
        public const string DEFAULT_ICON_URL = PIN_BASE_URL + "iconos_reporte.png";

        #endregion

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

        internal static async Task<Response<T>> GetResponseDataAsync<T>(string segment)
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
            return new Response<T>(resultItem, result.StatusCode, errorMessage);
        }
    }
}
