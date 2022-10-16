global using QualityManagementApp.Client.Services.Contracts;
global using QualityManagementApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QualityManagementApp.Client;
using MudBlazor.Services;
using QualityManagementApp.Shared;
using QualityManagementApp.Client.Services.Catalog;
using QualityManagementApp.Client.Services.Contracts.Catalog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPresetActivityService, PresetActivityService>();

builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyInsightsService, SurveyInsightsService>();

await builder.Build().RunAsync();
