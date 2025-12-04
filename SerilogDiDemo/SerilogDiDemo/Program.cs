using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SerilogDiDemo;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog(config => {
    config.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddTransient<IMyService, MyOtherService>();

builder.Services.AddTransient<ISomeLogic, SomeLogic>();

using IHost host = builder.Build();

Console.WriteLine("--- SCENARIO A: MyService is injected ---");

using (var scope = host.Services.CreateScope())
{
    var logic = scope.ServiceProvider.GetRequiredService<ISomeLogic>();

    logic.HandleWork();
}
Console.ReadKey();