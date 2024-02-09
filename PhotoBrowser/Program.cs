using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoBrowser;
using PhotoBrowser.Models;
using PhotoBrowser.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IClientService<User>, UserService>();
builder.Services.AddTransient<IClientService<Photo>, PhotoService>();
builder.Services.AddTransient<IClientService<Album>, AlbumService>();
builder.Services.AddScoped<IDataService, DataService>();

await builder.Build().RunAsync();
