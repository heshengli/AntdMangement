﻿using AntdMangement.Models;
using System.Net.Http.Json;

namespace AntdMangement.Services
{
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrentUser> GetCurrentUserAsync()
        {
            return await _httpClient.GetFromJsonAsync<CurrentUser>("data/current_user.json");
        }
    }
}
