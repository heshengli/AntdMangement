using AntDesign.ProLayout;
using AntdMangement.Services;
using AntdMangement.Store;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net;

namespace AntdMangement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


#if DEBUG
            var baseAddress = builder.Configuration["AppSettings:ApiUrl"];
            builder.Services.AddHttpClient("AntdMangement.ServerAPI", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
#else
            builder.Services.AddScoped(sp => new HttpClient()
            { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),         
            });
#endif




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