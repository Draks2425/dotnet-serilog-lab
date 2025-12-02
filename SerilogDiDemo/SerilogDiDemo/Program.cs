using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

// 1. Setup Builder - this builds our application
var builder = Host.CreateApplicationBuilder(args);

// 2. Serilog Configuration - load settings from appsettings.json
builder.Services.AddSerilog(config => {
    config.ReadFrom.Configuration(builder.Configuration);
});

// 3. Build the host (the DI container is now ready)
using IHost host = builder.Build();

Console.WriteLine("--- TASK 3: Manual Logging ---");

// 4. Create a "Scope" to retrieve the logger manually
using (var scope = host.Services.CreateScope())
{
    // Request the logger from the container
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // 5. Log various levels (Task 3)
    logger.LogInformation("This is info - everything is working.");
    logger.LogWarning("This is warning - attention!");
    logger.LogError("This is error - something exploded (simulated).");
    logger.LogDebug("This is debug - you won't see this because MinimumLevel in appsettings is set to Information.");
}


//Check the bin/Debug/net10.0/logs folder for the file!