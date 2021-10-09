using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManagerClient.Services
{
    public class CpuRequest
    {
        private readonly HttpClient _httpClient;

        public CpuRequest()
        {
            _httpClient = new HttpClient();
        }
        
        public CpuMetricResponce GetAllCpuMetrics(
            //TimeSpan fromParameter, 
            //TimeSpan toParameter
            )
        {
            
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44308/api/metrics/cpu/all");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                Stream responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<CpuMetricResponce>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public CpuMetricResponce GetAllCpuMetrics(
            TimeSpan fromTime, 
            TimeSpan toTime
            )
        {

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44308/api/metrics/cpu/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                Stream responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<CpuMetricResponce>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}