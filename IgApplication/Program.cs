using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IgApplication;
using IgApplication.Services;
using IgniteUI.Blazor.Controls;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register IgniteUI Blazor modules
builder.Services.AddIgniteUIBlazor(
    typeof(IgbGridModule),
    typeof(IgbColumnModule),
    typeof(IgbPaginatorModule),
    typeof(IgbCategoryChartModule),
    typeof(IgbInputModule),
    typeof(IgbSelectModule),
    typeof(IgbSelectItemModule),
    typeof(IgbDialogModule),
    typeof(IgbCardModule),
    typeof(IgbNavDrawerModule),
    typeof(IgbNavDrawerItemModule),
    typeof(IgbNavDrawerHeaderItemModule),
    typeof(IgbNavbarModule),
    typeof(IgbButtonModule),
    typeof(IgbBadgeModule),
    typeof(IgbChipModule),
    typeof(IgbIconModule),
    typeof(IgbIconButtonModule)
);

// Register application services
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<TicketService>();

await builder.Build().RunAsync();
