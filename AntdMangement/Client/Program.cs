using AntDesign.ProLayout;
using AntdMangement.Extensions;
using AntdMangement.Services;
using AntdMangement.Store;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AntdMangement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            var baseAddress = builder.Configuration["AppSettings:ApiUrl"];
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("AntdMangement.ServerAPI",
                client => client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("AntdMangement.ServerAPI"));

            builder.Services.AddApiAuthorization<RemoteAppState, OidcAccount>(options =>
            {
                options.AuthenticationPaths.LogInPath = "/api/auth";

                options.UserOptions.AuthenticationType = "Bearer";
                options.ProviderOptions.ConfigurationEndpoint = baseAddress;

            }).AddAccountClaimsPrincipalFactory<RemoteAppState, OidcAccount, PreferencesUserFactory>();

            //builder.Services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();

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