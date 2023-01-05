using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace AntdMangement.Extensions
{
    public static class HttpClientExtension
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(this HttpClient httpClient, string url, string contentType = "application/json", string token = null, Dictionary<string, string> headers = null)
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
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
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
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

            return null;
        }
        /// <summary>
        /// Get<T>请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url, string contentType = "application/json", string token = null, Dictionary<string, string> headers = null) where T : class, new()
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
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
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<T>(data);
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
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(this HttpClient httpClient, string url, string postData, string contentType = "application/json", string token = null, Dictionary<string, string> headers = null)
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            try
            {
                HttpContent httpContent = new StringContent(postData);
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
                HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
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
            return null;
        }

        /// <summary>
        /// Post<T>请求（json请求体）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url</param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string url, string postData, string contentType = "application/json", string token = null, Dictionary<string, string> headers = null) where T : class, new()
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            try
            {
                HttpContent httpContent = new StringContent(postData);
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
                HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<T>(data);
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
        public static async Task<T> PostAsync<T, T1>(this IHttpClientFactory HttpClientFactory, string url, T1 postData, string contentType = "application/json", string token = null, Dictionary<string, string> headers = null) where T : class, new()
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            var httpClient = HttpClientFactory.CreateClient("AntdMangement.ServerAPI");

            try
            {
                HttpContent httpContent = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, contentType);
                httpClient.Timeout = new TimeSpan(0, 1, 0);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                if (headers != null && headers.Keys.Any())
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<T>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
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
