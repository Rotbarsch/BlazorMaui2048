using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Twothousandfortyeight.Game;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.Add2048GameServices();

await builder.Build().RunAsync();
