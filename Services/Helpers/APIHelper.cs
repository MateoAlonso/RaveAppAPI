using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace RaveAppAPI.Services.Helpers
{
    public class ApiHelper
    {
        public static HttpRequestMessage BuildRequest(HttpMethod verb, string url, string endpoint, object payload = null, AuthenticationHeaderValue auth = null, Dictionary<string, string?> headers = null)
        {
            HttpRequestMessage request = new HttpRequestMessage() { Method = verb, RequestUri = new Uri($"{url}{endpoint}") };
            if (payload != null)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                request.Content = content;
            }
            request.Headers.Authorization = auth;
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            return request;
        }
        public static T MapResponse<T>(HttpResponseMessage response) where T : class
        {
            string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            T result = null;
            if (!string.IsNullOrEmpty(content))
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                result = JsonConvert.DeserializeObject<T>(content, settings);
            }
            return result;
        }
    }
}
