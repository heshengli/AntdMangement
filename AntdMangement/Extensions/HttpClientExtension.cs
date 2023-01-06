using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Serialization;

namespace AntdMangement.Extensions
{
    public static class HttpClientExtension
    {
        private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
        };

        /// <summary>
        /// Get<T>请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url, string token = null, Dictionary<string, string> headers = null, JsonSerializerOptions options = null) where T : class, new()
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (headers != null && headers.Keys.Any())
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    options = options ?? DefaultJsonSerializerOptions;
                    result = await response.Content.ReadFromJsonAsync<T>(options);
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                httpClient.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Post<T>请求（json请求体）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url</param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string url, object postData, string token = null, Dictionary<string, string> headers = null, JsonSerializerOptions options = null) where T : class, new()
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (headers != null && headers.Keys.Any())
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var response = await httpClient.PostAsJsonAsync(url, postData);
                if (response.IsSuccessStatusCode)
                {
                    options = options ?? DefaultJsonSerializerOptions;
                    result = await response.Content.ReadFromJsonAsync<T>(options);
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                httpClient.Dispose();
            }
            return result;
        }

        /// <summary>
        /// PostXm<T>请求（xml请求体）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="xmlString">xml请求体</param>
        /// <returns></returns>
        public static async Task<T> PostXmlAsync<T>(this HttpClient httpClient, string url, string xmlString, string contentType = "application/json", string token = null, Dictionary<string, string> headers = null) where T : class, new()
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            try
            {
                HttpContent httpContent = new StringContent(xmlString);
                httpClient.Timeout = new TimeSpan(0, 1, 0);
                if (!string.IsNullOrWhiteSpace(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (headers != null && headers.Keys.Any())
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                var response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    result = XmlDeserialize<T>(data);
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                httpClient.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 反序列化Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xmlString) where T : class, new()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xmlString))
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
