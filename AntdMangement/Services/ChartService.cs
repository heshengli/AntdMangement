﻿using AntdMangement.Models;
using System.Net.Http.Json;

namespace AntdMangement.Services
{
    public interface IChartService
    {
        Task<ChartDataItem[]> GetVisitDataAsync();
        Task<ChartDataItem[]> GetVisitData2Async();
        Task<ChartDataItem[]> GetSalesDataAsync();
        Task<RadarDataItem[]> GetRadarDataAsync();
    }

    public class ChartService : IChartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public ChartService(HttpClient httpClient,IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ChartDataItem[]> GetVisitDataAsync()
        {
            return (await GetChartDataAsync()).VisitData;
        }

        public async Task<ChartDataItem[]> GetVisitData2Async()
        {
            return (await GetChartDataAsync()).VisitData2;
        }

        public async Task<ChartDataItem[]> GetSalesDataAsync()
        {
            return (await GetChartDataAsync()).SalesData;
        }

        public async Task<RadarDataItem[]> GetRadarDataAsync()
        {
            return (await GetChartDataAsync()).RadarData;
        }

        private async Task<ChartData> GetChartDataAsync()
        {
            return await _httpClient.GetFromJsonAsync<ChartData>("data/fake_chart_data.json");
        }
    }
}
