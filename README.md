# Serilog DI Demo: .NET Logging and Dependency Injection Lab

This project is a minimal .NET Console Application designed to demonstrate three fundamental concepts in modern .NET development:

1.  **Structured Logging** using the third-party library **Serilog**.
2.  **Dependency Injection (DI)** using the built-in Generic Host Builder.
3.  **Configuration Management** using `appsettings.json`.

This repository was created as part of a programming internship task.

---

## ✨ Key Features Demonstrated

* **NuGet Package Management:** Installing, downgrading, and upgrading essential libraries.
* **Serilog Setup:** Configuring Serilog to read its settings from `appsettings.json`.
* **Multiple Sinks:** Logging output simultaneously to the **Console** and a **File** (`logs/log.txt`).
* **Constructor Injection (DI):** Correctly injecting the `ILogger<T>` interface into the `MyService` class (Task 4).
* **Logging Levels:** Demonstrating `Information`, `Warning`, `Error`, and `Debug` log levels.

---

## ⚙️ Prerequisites

To run this project, you need:

* **[.NET 10 SDK]** (or compatible version, e.g., .NET 8 SDK).
* **Visual Studio 2022** (or VS Code with C# extensions).

---

## 🚀 Getting Started

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/YourGitHubName/dotnet-serilog-lab.git](https://github.com/YourGitHubName/dotnet-serilog-lab.git)
    cd dotnet-serilog-lab/SerilogDiDemo
    ```

2.  **Install Packages:**
    * Open the solution in Visual Studio.
    * Right-click the project -> Manage NuGet Packages -> Install the required Serilog packages (as described in Task 1).

3.  **Run the Application:**
    * Open the solution file (`SerilogDiDemo.sln`).
    * Press **F5** or run from the CLI using `dotnet run`.

---

## 📁 Key Files & Code Snippets

### 1. Program.cs (DI Setup and Manual Logging)

This file sets up the Host (DI container) and performs the logging for Tasks 3 and 5.

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

// Setup Builder - this builds our application host
var builder = Host.CreateApplicationBuilder(args);

// Serilog Configuration - load settings from appsettings.json
builder.Services.AddSerilog(config => {
    config.ReadFrom.Configuration(builder.Configuration);
});

// Register our service for constructor injection (Task 5)
builder.Services.AddTransient<MyService>(); 

// Build the host (the DI container is ready)
using IHost host = builder.Build();

Console.WriteLine("--- TASK 3: Manual Logging ---");

// Create a scope to retrieve the logger manually
using (var scope = host.Services.CreateScope())
{
    // Request the logger from the container
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // Log with different levels (Task 3)
    logger.LogInformation("This is info - everything is working.");
    logger.LogWarning("This is warning - attention!");
    logger.LogError("This is error - something exploded (simulated).");
    logger.LogDebug("This is debug - you won't see this unless minimum level is set to Debug.");
}

// --- TASK 5: Invoking Service with DI ---
Console.WriteLine("\n--- TASK 5: Logging from a DI Class ---");
using (var scope = host.Services.CreateScope())
{
    // Retrieve our service. The DI container automatically injects the ILogger dependency!
    var myService = scope.ServiceProvider.GetRequiredService<MyService>();
    
    myService.DoWork();
}

Console.WriteLine("\nApplication finished. Check the logs/log.txt file for output.");
Console.ReadKey();
