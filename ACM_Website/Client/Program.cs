using ACM_Website.Client.Services.AuthService;
using ACM_Website.Client.Services.CustomerService;
using ACM_Website.Client.Services.ExecutorService;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;
using ACM_Website.Client.Services.SearchService;
using ACM_Website.Client.Services.ModeratorService;
using ACM_Website.Client.Services.ChatService;
using ACM_Website.Client.Services.FileService;

namespace ACM_Website.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IExecutorService, ExecutorService>();
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<IModeratorService, ModeratorService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
