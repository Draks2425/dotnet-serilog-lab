using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
namespace SerilogDiDemo;

public class SomeLogic : ISomeLogic
{
    private readonly ILogger<SomeLogic> _logger;
    private readonly IMyService _myService;

    public SomeLogic(ILogger<SomeLogic> logger, IMyService myService)
    {
        _logger = logger;
        _myService = myService;
        _logger.LogInformation("instance created");
    }

    public void HandleWork()
    {
        _logger.LogInformation("Starting work Delegating task to injected service");

        _myService.DoWork();

        _logger.LogInformation("Work finished");
    }
}
