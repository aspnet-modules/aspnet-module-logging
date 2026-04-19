# AspNet.Module.Logging

Logging module that replaces the default ASP.NET logger with [Serilog](https://serilog.net/).

## Installation

```sh
dotnet add package AspNet.Module.Logging
```

## Configuration

Logging can be configured through `appsettings.json` or environment variables.

```json
{
  "Serilog": {
    "MinimumLevel": "Verbose",
    "ServiceName": "Service name for Elastic",
    "Sinks": {
      "Elasticsearch": {
        "Url": "Elastic URL",
        "TemplateVersion": 7
      }
    }
  }
}
```

## Module Registration

```cs
using AspNet.Module.Logging;

var builder = AspNetWebApplication.CreateBuilder(args);
builder.RegisterModule<LoggingModule>();

var app = builder.Build();
await app.RunWithLogging();
```

## Source Code

- Repository: [github.com/aspnet-modules/aspnet-module-logging](https://github.com/aspnet-modules/aspnet-module-logging)
