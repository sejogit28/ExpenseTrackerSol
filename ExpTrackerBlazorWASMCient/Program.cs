using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using ExpTrackerAppApplicationLogic;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpTrackerBlazorWASMCient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            builder.Services.AddAuthorizationCore();


            //The following needs to be done for Blazor WASM to know about your externally created classes/services
            builder.Services.AddTransient<IAuthRepository, AuthRepository>();
            builder.Services.AddTransient<IIncomesScreenUseCases, IncomesScreenUseCases>();
            builder.Services.AddTransient<IExpensesScreenUseCases, ExpensesScreenUseCases>();
            //"Transient' means that everytime you need it, a new instance will be created

            builder.Services.AddTransient<IIncomesRepository, IncomesRepository>();
            builder.Services.AddTransient<IExpensesRepository, ExpensesRepository>();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            builder.Services.AddSingleton<IWebApiExecuter, WebApiExecuter>(sp => new WebApiExecuter("https://localhost:5001", new HttpClient()));
            /*It is bad practice to keep creating new instances of HttpClient(as the default AddScope Method shows below)
             So when calling a Interface/abstraction that relies on HttpClient it is better to use the Singleton Method
            as opposed to using the Transient method*/

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //Scoped in Blazor WASM is a Singleton

            await builder.Build().RunAsync();
        }
    }
}
