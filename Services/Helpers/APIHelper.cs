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
        public static HttpRequestMessage BuildRequest(HttpMethod verb, string url, string endpoint, Dictionary<string, object> formData, AuthenticationHeaderValue auth = null, Dictionary<string, string?> headers = null)
        {
            HttpRequestMessage request = new HttpRequestMessage() { Method = verb, RequestUri = new Uri($"{url}{endpoint}") };

            var content = new MultipartFormDataContent();
            foreach (var item in formData)
            {
                switch (item.Value)
                {
                    case string str:
                        content.Add(new StringContent(str), item.Key);
                        break;
                    case byte[] bytes:
                        var fileContent = new ByteArrayContent(bytes);
                        fileContent.Headers.ContentType =
                            new MediaTypeHeaderValue("application/octet-stream");
                        content.Add(fileContent, item.Key, "file.bin");
                        break;
                    case Stream stream:
                        var streamContent = new StreamContent(stream);
                        content.Add(streamContent, item.Key, "file.bin");
                        break;
                    case IEnumerable<string> list:
                        foreach (var val in list)
                        {
                            content.Add(new StringContent(val), item.Key);
                        }
                        break;
                    default:
                        content.Add(new StringContent(item.Value.ToString() ?? string.Empty), item.Key);
                        break;
                }
            }
            request.Content = content;
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
