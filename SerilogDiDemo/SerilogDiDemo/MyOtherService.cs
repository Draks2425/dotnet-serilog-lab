using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SerilogDiDemo;

public class MyOtherService : IMyService
{
    private readonly ILogger<MyOtherService> _logger;

    public MyOtherService(ILogger<MyOtherService> logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogWarning("MyOtherService is executing its implementation of DoWork (Implementation 2)");
    }
}
