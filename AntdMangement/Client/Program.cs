using AntDesign.ProLayout;
using AntdMangement.Services;
using AntdMangement.Store;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net;
using System.Net.Http;

namespace AntdMangement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            var baseAddress = builder.Configuration["AppSettings:ApiUrl"];
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

            builder.Services.AddHttpClient("AntdMangement.ServerAPI", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("mode", "no-cors");
                client.DefaultRequestHeaders.Add("cache", "no-store");
                client.DefaultRequestHeaders.Add("credentials", "include");
            });

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("AntdMangement.ServerAPI"));

            builder.Services.AddBlazoredLocalStorageAsSingleton();

            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            builder.Services.AddScoped<IChartService, ChartService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddSingleton<StateService>();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}