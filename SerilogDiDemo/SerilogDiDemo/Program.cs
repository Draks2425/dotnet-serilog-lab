using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog(config => {
    config.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddTransient<MyService>();
//build the host the DI container
using IHost host = builder.Build();

Console.WriteLine("--- TASK 3: Manual Logging ---");

using (var scope = host.Services.CreateScope())
{
    //request the logger from the container
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    //3
    logger.LogInformation("This is info - everything is working.");
    logger.LogWarning("This is warning - attention!");
    logger.LogError("This is error - something exploded (simulated).");
    logger.LogDebug("This is debug - you won't see this because MinimumLevel in appsettings is set to Information.");
}
//Check the bin/Debug/net10.0/logs folder for the file!
Console.WriteLine("\n--- TASK 5: Logging from a DI Class ---");

using (var scope = host.Services.CreateScope())
{
    var myService = scope.ServiceProvider.GetRequiredService<MyService>();

    myService.DoWork();
}
