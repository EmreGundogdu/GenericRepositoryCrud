using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FormulaOne.App;
using FormulaOne.App.Services;
using FormulaOne.App.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped(c => new HttpClient()
{
BaseAddress = new Uri("http://localhost:5228")
});
await builder.Build().RunAsync();