global using QualityManagementApp.Client.Services.Contracts;
global using QualityManagementApp.Client.Services;
global using QualityManagementApp.Client.Services.Catalog;
global using QualityManagementApp.Client.Services.Contracts.Catalog;
global using static QualityManagementApp.Shared.Model;
global using Microsoft.AspNetCore.Components;
global using System.Net.Http.Json;
global using MudBlazor;

using MudBlazor.Services;
var builder = Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<QualityManagementApp.Client.App>("#app");
builder.RootComponents.Add<Microsoft.AspNetCore.Components.Web.HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices(
    config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 10000;
        config.SnackbarConfiguration.HideTransitionDuration = 500;
        config.SnackbarConfiguration.ShowTransitionDuration = 500;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    });

builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPresetActivityService, PresetActivityService>();
builder.Services.AddScoped<ISurveyCategory, SurveyCategoryService>();

builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IPlanningService, PlanningService>();

await builder.Build().RunAsync();
