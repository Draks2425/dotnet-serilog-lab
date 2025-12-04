using Microsoft.Extensions.Logging;
using SerilogDiDemo;
using System;
using System.Collections.Generic;
using System.Text;
public class MyService : IMyService
{
    // Private field for the logger dependency
    private readonly ILogger<MyService> _logger;
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("--- TASK 5: MyService is working! ---");
        _logger.LogInformation("This message comes from a class created by DI.");
        _logger.LogInformation("12.03 (Implementation 1)");
    }
}