global using QualityManagementApp.Client.Services.Contracts;
global using QualityManagementApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QualityManagementApp.Client;
using MatBlazor;
using QualityManagementApp.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ClipboardService>();

builder.Services.AddScoped<ISurveyService, SurveyService>();

builder.Services.AddIgniteUIBlazor();
builder.Services.AddMatBlazor();

await builder.Build().RunAsync();
