using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using ExpTrackerAppApplicationLogic;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using ExpenseTrackerRepository.AuthProviders;
using Microsoft.Extensions.Configuration;

namespace ExpTrackerWASMClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
           
            //"Transient' means that everytime you need it, a new instance will be created
            //The following needs to be done for Blazor WASM to know about your externally created classes/services
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddTransient<IIncomesScreenUseCases, IncomesScreenUseCases>();
            builder.Services.AddTransient<IExpensesScreenUseCases, ExpensesScreenUseCases>();
            builder.Services.AddTransient<IAdminScreenUseCases, AdminScreenUseCases>();
            builder.Services.AddTransient<IGroupsScreenUseCases, GroupsScreenUseCases>();

            builder.Services.AddTransient<IAuthRepository, AuthRepository>();
            builder.Services.AddTransient<IIncomesRepository, IncomesRepository>();
            builder.Services.AddTransient<IExpensesRepository, ExpensesRepository>();
            builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddTransient<IGroupsRepository, GroupsRepository>();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            var remoteApiDomain = "https://expense-tracker-api28.herokuapp.com";
            var localApiDomain = "https://localhost:5001";

            var localClientDomain = "https://localhost:44382";
            var remoteClientDomain = "https://loving-turing-d3cc22.netlify.app";

            builder.Services.AddSingleton<IWebApiExecuter, WebApiExecuter>(sp => new WebApiExecuter(localApiDomain, new HttpClient()));

            /*It is bad practice to keep creating new instances of HttpClient(as the default AddScope Method shows below)
             So when calling a Interface/abstraction that relies on HttpClient it is better to use the Singleton Method
            as opposed to using the Transient method*/

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(localClientDomain + "/") });
           
           await builder.Build().RunAsync();
        }
    }
}
